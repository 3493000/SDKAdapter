﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public interface IAdInterfaceEventListener
    {
        void OnAdLoadEvent();
        void OnAdLoadFailedEvent();
        void OnAdRewardEvent();
        void OnAdCloseEvent();
        void OnOffwallCloseEvent(string rewardId, double currencyCount);
        string adPlacementID
        {
            get;
        }
    }

    public class AdInterface
    {
        public Action<bool> On_AdStateUpdateEvent;
        public static int MAX_LOADING_AD_COUNT = 2;
        public static int MAX_LOADED_AD_COUNT = 12;
        public static float FULLSCREEN_AD_LOAD_OFFSET = 3;

        public static int AD_FAILED_WAIT_DURATION = 50;
        public static int AD_FAILED_ADD_OFFSET = 5;

        protected int m_AdType;
        protected List<AdHandler> m_AdHandler;
        protected bool m_IsShowing = false;
        protected string m_RewardID;
        protected AdSize m_AdSize;
        protected AdPosition m_AdPosition;
        protected Vector2Int m_AdCustomGrid;
        protected AdEventLogger m_AdLogger;
        protected string m_AdInterfaceName;
        protected bool m_HasReward;
        protected IAdInterfaceEventListener m_EventListener;
        protected bool m_PreAdState = false;


        public AdHandler GetAdHandlerByPlatform(string adPlatform)
        {
            if (m_AdHandler == null || m_AdHandler.Count == 0)
            {
                return null;
            }

            for (int i = m_AdHandler.Count; i >= 0; --i)
            {
                if (m_AdHandler[i].adAdapter.adPlatform == adPlatform)
                {
                    return m_AdHandler[i];
                }
            }

            return null;
        }

        public List<AdHandler> GetHandlerList()
        {
            return m_AdHandler;
        }


        public IAdInterfaceEventListener adEventListener
        {
            get { return m_EventListener; }
            set
            {
                m_EventListener = value;

                if (m_EventListener != null)
                {
                    m_AdLogger.adPlacement = m_EventListener.adPlacementID;
                }
                else
                {
                    m_AdLogger.adPlacement = null;
                }
            }
        }

        public bool isAdReady
        {
            get
            {
                if (adType == AdType.Interstitial && !AdsAnalysisMgr.S.IsInterAvailable())
                {
                    return false;
                }

                for (int i = 0; i < m_AdHandler.Count; ++i)
                {
                    if (m_AdHandler[i].isAdReady)
                    {
                        DataAnalysisMgr.S.CustomEventWithAppsflyer(DataAnalysisDefine.AD_GET_STATE_KEY, "True");
                        return true;
                    }
                }
                DataAnalysisMgr.S.CustomEventWithAppsflyer(DataAnalysisDefine.AD_GET_STATE_KEY, "False");
                return false;
            }
        }

        public bool isAdStateReady
        {
            get
            {
                if (adType == AdType.Interstitial && !AdsAnalysisMgr.S.IsInterAvailable())
                {
                    return false;
                }

                for (int i = 0; i < m_AdHandler.Count; ++i)
                {
                    if (m_AdHandler[i].isAdReady)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool CheckIsAdReady(string platform)
        {
            platform = platform.ToLower();

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                if (m_AdHandler[i].isAdReady && m_AdHandler[i].adConfig.adPlatform == platform)
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckAdState()
        {
            bool state = isAdStateReady;
            if (state != m_PreAdState)
            {
                m_PreAdState = state;
                if (On_AdStateUpdateEvent != null)
                {
                    On_AdStateUpdateEvent(state);
                }
            }
        }

        public string adInterfaceName
        {
            get { return m_AdInterfaceName; }
        }

        public string rewardID
        {
            get { return m_RewardID; }
        }

        public int adType
        {
            get { return m_AdType; }
        }

        public AdSize adSize
        {
            get { return m_AdSize; }
            set
            {
                m_AdSize = value;
            }
        }

        public AdPosition adPosition
        {
            get { return m_AdPosition; }
            set { m_AdPosition = value; }
        }

        public Vector2Int adCustomGrid
        {
            get
            {
                return m_AdCustomGrid;
            }

            set
            {
                m_AdCustomGrid = value;
            }
        }

        protected virtual bool isLogEnable
        {
            get { return true; }
        }

        public void InitAdInterface(string interfaceName, int adType)
        {
            m_AdType = adType;
            m_AdInterfaceName = interfaceName;

            m_AdLogger = new AdEventLogger(m_AdType);
            m_AdLogger.adInterface = m_AdInterfaceName;
            m_AdLogger.isEnable = isLogEnable;

            m_AdLogger.Log(ADLabelDefine.INIT);
            m_AdLogger.LogIPUInit();
            m_AdHandler = new List<AdHandler>();
            InitAdHandlerList();
        }

        public void RebuildAdHandlerList()
        {
            InitAdHandlerList();
        }

        public virtual bool ShowAd(string rewardID)
        {
            return false;
        }

        public virtual void PreLoadAd()
        {
        }

        public virtual void SyncAdPosition()
        {

        }

        public virtual void HideAd()
        {

        }

        protected void InitAdHandlerList()
        {
            BeforeAdHandlerInit();

            var adDataList = TDAdConfigTable.GetAdDataByInterface(m_AdInterfaceName);

            if (adDataList.Count <= 0)
            {
                Log.w("Not Find AdConfig For Interface:" + m_AdInterfaceName);
                return;
            }

            for (int i = 0; i < adDataList.Count; ++i)
            {
                var handler = AdsMgr.S.CreateAdHandler(adDataList[i]);
                if (handler != null)
                {
                    m_AdHandler.Add(handler);
                    ProcessNewAdHandler(handler);
                }
            }

            AfterAdHandlerInit();

            m_AdHandler.Sort(AdHandlerSorter);

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                m_AdHandler[i].SetAdInterface(this);
            }
        }

        protected virtual void BeforeAdHandlerInit()
        {

        }

        protected virtual void AfterAdHandlerInit()
        {

        }

        protected virtual void ProcessNewAdHandler(AdHandler handler)
        {

        }

        protected int AdHandlerSorter(AdHandler a, AdHandler b)
        {
            return b.ecpm - a.ecpm;
        }

        ///////////////////////////////////
        #region Handler 事件
        public virtual void OnAdLoad(TDAdConfig config)
        {
            //++AdAnalysisMgr.S.interstitialLoadCount;
            m_AdLogger.Log(ADLabelDefine.LOADED_SUCCESS);
            m_AdLogger.LogFill(config);
            if (m_EventListener != null)
            {
                m_EventListener.OnAdLoadEvent();
            }
        }

        public virtual void OnAdLoadFailed()
        {
            m_AdLogger.Log(ADLabelDefine.LOADED_FAILED);
            if (m_EventListener != null)
            {
                m_EventListener.OnAdLoadFailedEvent();
            }

            PreLoadAd();
        }

        public void OnAdOpen()
        {
            m_AdLogger.Log(ADLabelDefine.IMPRESSION);
        }

        public void OnAdClick()
        {

        }

        public void OnAdClose(TDAdConfig config)
        {
            m_IsShowing = false;
            switch (m_AdType)
            {
                case AdType.Interstitial:
                    m_AdLogger.LogIPU(config);
                    break;
                case AdType.RewardedVideo:
                    {
                        if (m_HasReward)
                        {
                            m_AdLogger.Log(ADLabelDefine.REWARD);
                            m_AdLogger.LogIPU(config);
                        }
                        else
                        {
                            m_AdLogger.Log(ADLabelDefine.ABANDON);
                        }
                    }
                    break;
            }

            if (m_EventListener != null)
            {
                m_EventListener.OnAdCloseEvent();
            }

            PreLoadAd();
        }

        public void OnAdReward()
        {
            m_HasReward = true;
            if (m_EventListener != null)
            {
                m_EventListener.OnAdRewardEvent();
            }
        }

        public void OnAdRequest(TDAdConfig config)
        {
            m_AdLogger.LogRequest(config);
        }

        public void OnOffwallClose(string rewardid, double currecyCount)
        {
            if (m_EventListener != null)
            {
                m_EventListener.OnOffwallCloseEvent(rewardid, currecyCount);
            }
        }

        #endregion
    }
}
