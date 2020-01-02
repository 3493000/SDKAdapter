using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AdBannerHandler : AdHandler
    {
        protected bool m_IsShowing = false;

        protected override int failedWaitAddOffset
        {
            get
            {
                return 10;
            }
        }

        protected override int failedWaitDuration
        {
            get
            {
                return 30;
            }
        }

        public override bool ShowAd()
        {
            if (m_IsShowing)
            {
                return false;
            }

            if (m_AdState == AdState.Failed)
            {
                return false;
            }

            if (DoShowAd())
            {
                DataAnalysisMgr.S.CustomValueEvent("AD-Value", m_Config.ecpm / 1000f);
                DataAnalysisMgr.S.CustomValueEvent("ADShow", m_Config.ecpm / 1000f, null, m_AfDataDic);
                //Log.i("AD->ShowBanner:" + m_Config.id);
                m_IsShowing = true;
            }

            return m_IsShowing;
        }

        //Banner Only?
        public override void RefreshAd()
        {
            if (!m_IsShowing)
            {
                return;
            }
            //Log.i("AD->RefreshBanner:" + m_Config.id);
            DoRefreshAd();
        }

        public override void HideAd()
        {
            if (!m_IsShowing)
            {
                return;
            }

            m_IsShowing = false;

            DoHideAd();
        }

        public override bool PreLoadAd()
        {
            return DoPreLoadAd();
        }

        protected virtual bool DoRefreshAd()
        {
            return false;
        }

        protected virtual bool DoShowAd()
        {
            return false;
        }

        protected virtual void DoHideAd()
        {

        }

        protected virtual bool DoPreLoadAd()
        {
            return false;
        }
    }
}
