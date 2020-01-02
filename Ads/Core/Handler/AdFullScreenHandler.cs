using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AdFullScreenHandler : AdHandler
    {

        protected override int failedWaitDuration
        {
            get { return AdInterface.AD_FAILED_WAIT_DURATION; }
        }

        protected override int failedWaitAddOffset
        {
            get { return AdInterface.AD_FAILED_ADD_OFFSET; }
        }

        public override bool ShowAd()
        {
            if (!isAdReady)
            {
                return false;
            }

            if (m_AdState != AdState.Loaded)
            {
                return false;
            }

            if (DoShowAd())
            {
                //Log.i("AD-ShowAD:" + m_Config.id);
                DataAnalysisMgr.S.CustomValueEvent("AD-Value", m_Config.ecpm / 1000f);
                DataAnalysisMgr.S.CustomValueEvent(DataAnalysisDefine.AF_AD_SHOW, m_Config.ecpm / 1000f, null, m_AfDataDic);
                m_AdState = AdState.Showing;
            }

            return m_AdState == AdState.Showing;
        }

        public override bool PreLoadAd()
        {
            if (m_AdState != AdState.NONE)
            {
                return false;
            }

            if (string.IsNullOrEmpty(m_Config.unitID))
            {
                return false;
            }

            if (DoPreLoadAd())
            {
                m_AdState = AdState.Loading;
                m_AdInterface.OnAdRequest(m_Config);
               // Log.i(string.Format("AD-PreLoadAd:{0} - Time:{1}", m_Config.id, Time.realtimeSinceStartup));
                DataAnalysisMgr.S.CustomValueEvent("ADRequest", m_Config.ecpm / 1000f, null, m_AfDataDic);
            }
            else
            {
               // Log.i(string.Format("AD-PreLoadAd:{0} - Failed", m_Config.id));
            }

            return m_AdState == AdState.Loading;
        }

        protected virtual bool DoPreLoadAd()
        {
            return false;
        }

        protected virtual bool DoShowAd()
        {
            return false;
        }
    }
}
