using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AdHandler
    {
        protected class PlatformAdHandlerState
        {
            private string m_Key;
            private bool m_IsEnable = true;          

            public bool isEnable
            {
                get { return m_IsEnable; }
            }

            public PlatformAdHandlerState(string key)
            {
                m_Key = key;
                m_IsEnable = true;
            }

            public void Pause()
            {
                if (!m_IsEnable)
                {
                    return;
                }

                m_IsEnable = false;

                Timer.S.Post2Really(OnResumeTick, 35, 1);
            }

            protected void OnResumeTick(int count)
            {
                m_IsEnable = true;
            }

            private static Dictionary<string, PlatformAdHandlerState> m_StateMap;

            public static PlatformAdHandlerState GetAdHandlerState(TDAdConfig config)
            {
                string key = string.Format("{0}-{1}", config.adPlatform, config.adType);

                if (m_StateMap == null)
                {
                    m_StateMap = new Dictionary<string, PlatformAdHandlerState>();
                }

                PlatformAdHandlerState result = null;

                if (!m_StateMap.TryGetValue(key, out result))
                {
                    result = new PlatformAdHandlerState(key);
                    m_StateMap.Add(key, result);
                }

                return result;
            }
        }

        protected AdState m_AdState = AdState.NONE;
        protected IAdAdapter m_Adapter;
        protected TDAdConfig m_Config;
        protected int m_ResetTimer;
        protected AdInterface m_AdInterface;
        protected int m_FailedWaitDuration = 10;
        protected PlatformAdHandlerState m_PlatformAdHandlerState;
        protected Dictionary<string,string> m_AfDataDic = new Dictionary<string, string>();

        public virtual bool isAdHandleEnable
        {
            get
            {
                return true;
            }
        }

        public virtual bool isAdReady
        {
            get
            {
                return false;
            }
        }

        public virtual bool isInstanceAd
        {
            get { return false; }
        }

        public bool isAdPlatformEnable
        {
            get { return m_PlatformAdHandlerState.isEnable; }
        }

        public TDAdConfig adConfig
        {
            get { return m_Config; }
            set { m_Config = value; }
        }

        public IAdAdapter adAdapter
        {
            get { return m_Adapter; }
        }

        public AdState adState
        {
            get { return m_AdState; }
        }

        public int ecpm
        {
            get
            {
                return m_Config.ecpm + m_Adapter.adPlatformScore;
            }
        }

        protected virtual int failedWaitDuration
        {
            get { return 10; }
        }

        protected virtual int failedWaitAddOffset
        {
            get { return 3; }
        }

        public void SetAdStateFailed()
        {
            m_AdState = AdState.Failed;

            if (m_ResetTimer > 0)
            {
                Timer.S.Cancel(m_ResetTimer);
                m_ResetTimer = -1;
            }

            m_ResetTimer = Timer.S.Post2Really(ResetAdState, m_FailedWaitDuration);
        }

        public void BindAdapter(IAdAdapter adapter)
        {
            m_Adapter = adapter;
        }

        public virtual void SetAdConfig(TDAdConfig config)
        {
            m_Config = config;
            m_PlatformAdHandlerState = PlatformAdHandlerState.GetAdHandlerState(config);
            m_FailedWaitDuration = AdInterface.AD_FAILED_WAIT_DURATION;

            m_AfDataDic.Add(DataAnalysisDefine.AF_PID,m_Config.unitID.ToString());
            m_AfDataDic.Add(DataAnalysisDefine.AF_SDK_NAME,DataAnalysisDefine.PLATFORMLIST.IndexOf(m_Config.adPlatform).ToString());
            m_AfDataDic.Add(DataAnalysisDefine.AF_SDK_ECPM, m_Config.ecpm == 0 ? "1" : m_Config.ecpm.ToString());
            m_AfDataDic.Add(DataAnalysisDefine.AF_AD_CLICK_TYPE, m_Config.adType.ToString());
            
        }

        public void SetAdInterface(AdInterface adInterface)
        {
            m_AdInterface = adInterface;
        }

        public virtual bool ShowAd()
        {
            return false;
        }

        public virtual bool PreLoadAd()
        {
            return false;
        }

        //Banner Only?
        public virtual void RefreshAd()
        {

        }

        public virtual void HideAd()
        {

        }

        public virtual void SyncAdPosition()
        {

        }

        protected virtual void DoCleanAd()
        {

        }

        protected void HandleOnAdLoaded()
        {
            ThreadMgr.S.mainThread.PostAction(ProcessAdLoadedAction);
        }

        protected void ProcessAdLoadedAction()
        {
            m_FailedWaitDuration = failedWaitDuration;
            m_AdState = AdState.Loaded;
            m_AdInterface.OnAdLoad(m_Config);
            DataAnalysisMgr.S.CustomValueEvent(DataAnalysisDefine.AF_AD_FILL, m_Config.ecpm / 1000f, null,
                m_AfDataDic);
        }

        protected void ProcessAdFailedLoadAction()
        {

            if (m_ResetTimer > 0)
            {
                Timer.S.Cancel(m_ResetTimer);
                m_ResetTimer = -1;
            }

            m_ResetTimer = Timer.S.Post2Really(ResetAdState, m_FailedWaitDuration);

            //m_FailedWaitDuration += failedWaitAddOffset;

            m_AdState = AdState.Failed;
            DoCleanAd();

            m_AdInterface.OnAdLoadFailed();
        }

        protected void HandleOnAdFailedToLoad(string msg)
        {
            try
            {
                if (CheckIsSeriousFailedError(msg))
                {
                    m_PlatformAdHandlerState.Pause();
                }

                Log.w(string.Format("AD-AdLoadFailed:{0}-", m_Config.id) + msg);
            }
            catch(Exception e)
            {
                Log.i("AD-LoadFailed");
                Log.e(e);
            }

            ThreadMgr.S.mainThread.PostAction(ProcessAdFailedLoadAction);
        }

        protected bool CheckIsSeriousFailedError(string msg)
        {
            if (msg == null)
            {
                return false;
            }

            msg = msg.ToLower();

            if (msg.Contains("fill") || msg.Contains("net"))
            {
                return false;
            }

            return true;
        }

        //统计展示
        protected void HandleOnAdOpened()
        {
            //Log.i("AD-HandleOnAdOpened:" + m_Config.id);
            m_AdInterface.OnAdOpen();
            DataAnalysisMgr.S.CustomValueEvent(DataAnalysisDefine.AF_AD_IMP, m_Config.ecpm / 1000f, null, m_AfDataDic);            
        }

        //恢复操作
        protected void HandleOnAdClosed()
        {
            //Log.i("AD-HandleOnAdClose:" + m_Config.id);
            ThreadMgr.S.mainThread.PostAction(ProcessAdClosedAction);
        }

        public virtual bool isFrquencyControl
        {
            get { return false; }
        }

        protected void ProcessAdClosedAction()
        {
            if (isFrquencyControl)
            {
                if (m_ResetTimer > 0)
                {
                    Timer.S.Cancel(m_ResetTimer);
                    m_ResetTimer = -1;
                }

                m_ResetTimer = Timer.S.Post2Really(ResetAdState, m_FailedWaitDuration);

                m_AdState = AdState.Failed;
            }
            else
            {
                m_AdState = AdState.NONE;
            }
            DataAnalysisMgr.S.CustomValueEvent(DataAnalysisDefine.AF_AD_CLOSE, m_Config.ecpm / 1000f, null,
                m_AfDataDic);
            m_AdInterface.OnAdClose(m_Config);
            DoCleanAd();
        }

        protected virtual void HandleOnAdClick()
        {
            m_AdInterface.OnAdClick();
            DataAnalysisMgr.S.CustomValueEvent(DataAnalysisDefine.AF_AD_CLICK, m_Config.ecpm / 1000f, null,
                m_AfDataDic);
        }

        protected void HandleOnAdLeftApplication()
        {
            //Log.i(ToString() + ":HandleOnAdLeftApplication");
        }

        protected void HandleOnAdRewarded()
        {
            m_AdInterface.OnAdReward();
            DataAnalysisMgr.S.CustomValueEvent(DataAnalysisDefine.AF_AD_REWARD, m_Config.ecpm / 1000f, null,
                m_AfDataDic);
            //
        }

        protected void HandleOnOffwallBack(string id, double count)
        {
            m_AdInterface.OnOffwallClose(id, count);
        }

        protected void ResetAdState(int count)
        {
            m_ResetTimer = -1;
            if (m_AdState == AdState.Failed)
            {
                m_AdState = AdState.NONE;
            }

            m_AdInterface.PreLoadAd();
        }
    }
}
