using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AdFullScreenInterface : AdInterface
    {
        private AdWaterfall[] m_AdWaterfalls;

        protected override void BeforeAdHandlerInit()
        {
            if (m_AdWaterfalls == null)
            {
                m_AdWaterfalls = new AdWaterfall[AdsMgr.S.platformCount];
            }
        }

        protected AdWaterfall GetAdWaterfall(IAdAdapter adapter)
        {
            if (m_AdWaterfalls == null || adapter == null)
            {
                return null;
            }

            if (adapter.platformIndex >= m_AdWaterfalls.Length)
            {
                Log.e("AD-Invalid AdWaterfall Index");
                return null;
            }

            AdWaterfall waterfall = m_AdWaterfalls[adapter.platformIndex];

            if (waterfall == null)
            {
                waterfall = new AdWaterfall(this, adapter);
                m_AdWaterfalls[adapter.platformIndex] = waterfall;
            }

            return waterfall;
        }

        protected override void ProcessNewAdHandler(AdHandler handler)
        {
            AdWaterfall waterfall = GetAdWaterfall(handler.adAdapter);

            if (waterfall == null)
            {
                Log.e("AD-Not Find Waterfall For handler");
                return;
            }

            waterfall.AddAdHandler(handler);
        }

        protected override void AfterAdHandlerInit()
        {
            for (int i = 0; i < m_AdWaterfalls.Length; ++i)
            {
                if (m_AdWaterfalls[i] == null)
                {
                    continue;
                }
                m_AdWaterfalls[i].Rebuild();
            }
        }

        public override bool ShowAd(string rewardID)
        {
            if (m_IsShowing)
            {
                return false;
            }

            m_RewardID = rewardID;
            m_HasReward = false;

            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                if (m_AdHandler[i].isAdReady)
                {
                    if (m_AdHandler[i].ShowAd())
                    {
                        m_IsShowing = true;
                        CheckAdState();
                        return true;
                    }
                }
            }

            return false;
        }

        public int GetAlreadyLoadCount()
        {
            int loadedCount = 0;
            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                switch (m_AdHandler[i].adState)
                {
                    case AdState.Loaded:
                        ++loadedCount;
                        break;
                    default:
                        break;
                }
            }

            return loadedCount;
        }

        public int CalcualteHandlerSortInLoadedAd(int ecpm)
        {
            int index = 0;
            for (int i = 0; i < m_AdHandler.Count; ++i)
            {
                if (ecpm > m_AdHandler[i].ecpm)
                {
                    return index;
                }

                switch (m_AdHandler[i].adState)
                {
                    case AdState.Loaded:
                        ++index;
                        break;
                    default:
                        break;
                }
            }

            return index;
        }

        public override void PreLoadAd()
        {
            if (m_AdWaterfalls == null || m_AdWaterfalls.Length == 0)
            {
                return;
            }

            for (int i = 0; i < m_AdWaterfalls.Length; ++i)
            {
                if (m_AdWaterfalls[i] == null)
                {
                    continue;
                }
                m_AdWaterfalls[i].PreLoadAd();
            }
        }

        public override void OnAdLoad(TDAdConfig config)
        {
            base.OnAdLoad(config);

            CheckAdState();
        }
    }
}
