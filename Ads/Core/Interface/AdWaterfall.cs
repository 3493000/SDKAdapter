using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AdWaterfall
    {
        private int m_ReloadTimer = -1;
        private List<AdHandler> m_AdHandler;
        private IAdAdapter m_AdAdapter;
        private AdFullScreenInterface m_AdInterface;
        private int m_LastLoadingIndex = -1;

        public IAdAdapter adAdapter
        {
            get { return m_AdAdapter; }
        }

        public AdWaterfall(AdFullScreenInterface adInterface, IAdAdapter adapter)
        {
            m_AdInterface = adInterface;
            m_AdAdapter = adapter;
            m_AdHandler = new List<AdHandler>();
        }

        public void AddAdHandler(AdHandler handler)
        {
            m_AdHandler.Add(handler);
        }

        public void Rebuild()
        {
            m_AdHandler.Sort(AdHandlerSorter);
        }

        protected int AdHandlerSorter(AdHandler a, AdHandler b)
        {
            return b.ecpm - a.ecpm;
        }

        protected void DoPreLoadWork()
        {
            if (m_AdHandler == null || m_AdHandler.Count == 0)
            {
                return;
            }

            int totalLoadedCount = m_AdInterface.GetAlreadyLoadCount();
            if (totalLoadedCount >= AdInterface.MAX_LOADED_AD_COUNT)
            {
                return;
            }

            m_AdInterface.CheckAdState();

            int loadedCount = 0;

            int i = 0;

            /*
            if (totalLoadedCount == 0 && m_LastLoadingIndex != (m_AdHandler.Count - 1))
            {
                int index = m_AdHandler.Count - 1;
                AdHandler handler = m_AdHandler[index];
                if (handler.adState == AdState.NONE && !handler.isInstanceAd)
                {
                    i = index;
                }
            }
            */

            for (; i < m_AdHandler.Count; ++i)
            {
                /*
                if (!m_AdHandler[i].isAdPlatformEnable)
                {
                    continue;
                }
                */

                if (!m_AdHandler[i].isAdHandleEnable)
                {
                    continue;
                }

                switch (m_AdHandler[i].adState)
                {
                    case AdState.Loaded:
                        ++loadedCount;
                        if (loadedCount >= 1)
                        {
                            return;
                        }
                        break;
                    case AdState.Loading:
                        break;
                    case AdState.NONE:
                        {
                            AdHandler handler = m_AdHandler[i];
                            int sort = m_AdInterface.CalcualteHandlerSortInLoadedAd(handler.ecpm);
                            if (sort >= 2)
                            {
                                return;
                            }
                            m_LastLoadingIndex = i;
                            m_AdHandler[i].PreLoadAd();

                            return;
                        }
                    default:
                        break;
                }
            }
        }

        public void PreLoadAd()
        {
            if (m_ReloadTimer <= 0)
            {
                m_ReloadTimer = Timer.S.Post2Really(OnLoadTimeTick, AdInterface.FULLSCREEN_AD_LOAD_OFFSET, -1);
            }
        }

        protected void OnLoadTimeTick(int count)
        {
            DoPreLoadWork();
        }
    }
}
