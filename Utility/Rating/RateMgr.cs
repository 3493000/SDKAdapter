using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hunter;
using Hunter.Plugin;


namespace Hunter
{
    public class RateMgr : TMonoSingleton<RateMgr>
    {
        RateHandler m_Handler;

        public override void OnSingletonInit()
        {
            if (m_Handler == null)
            {
#if UNITY_IOS && !UNITY_EDITOR
                m_Handler = new RateHandler_iOS();
#elif UNITY_ANDROID && !UNITY_EDITOR
			    m_Handler = new RateHandler_Android();
#else
                m_Handler = new RateHandler_Editor();
#endif
            }
        }

        int m_RateCount;


        #region Features

        public string iOS_App_ID()
        {
            return SDKConfig.S.iosAppID;
        }

        public string Android_App_ID()
        {
            return SDKConfig.S.bundleIDAndroid;
        }

        public bool RequestReviewCount(int frequency)
        {
            bool now = (++m_RateCount % frequency == 0);
            if (now)
            {
                RequestReview();
            }
            return now;
        }

        public void RequestReview()
        {
            m_Handler.RequestReview();
        }

        public void OpenRatingPage()
        {
            m_Handler.OpenRatingPage();
        }

        public bool IsInSandbox()
        {
            return m_Handler.IsInSandbox();
        }

        #endregion


    }
}
