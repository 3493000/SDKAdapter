using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


namespace Hunter.Plugin
{
    public class RateHandler_iOS : RateHandler
    {
#if UNITY_IOS
        #region Import Native implementations

        [DllImport("__Internal")]
        private static extern void Hunter_Rate_RequestReview();

        [DllImport("__Internal")]
        private static extern bool Hunter_Rate_IsInSandbox();

        #endregion

#endif
        #region Features

        public override void RequestReview()
        {
    #if UNITY_IOS
            Hunter_Rate_RequestReview();
#endif
        }

        public override void OpenRatingPage()
        {
#if UNITY_IOS
            string iOS_7 = ""
                + "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews"
                + "?id={0}" // App ID
                + "&type=Purple+Software"
                + "&mt=8";

            // string iOS_7 = "itms-apps://itunes.apple.com/app/id<APP_ID>";

            string iOS_8 = ""
                + "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews"
                + "?id={0}" // App ID
                + "&type=Purple+Software"
                + "&mt=8"
                + "&onlyLatestVersion=true"
                + "&pageNumber=0"
                + "&sortOrdering=1";

            string iOS_11 = ""
                + "itms-apps://itunes.apple.com/us/app/id{0}" // App ID
                + "?action=write-review"
                + "&mt=8";

            string versionString = UnityEngine.iOS.Device.systemVersion;
            string[] versionTokens = versionString.Split("."[0]);
            int version = int.Parse(versionTokens[0]);

            string URL;
            if (version <= 7)
            {
                URL = iOS_7;
            }
            else if (version <= 8)
            {
                URL = iOS_8;
            }
            else
            {
                URL = iOS_11;
            }

            URL = string.Format(URL, RateMgr.S.iOS_App_ID());
            Application.OpenURL(URL);
#endif
        }

        public override bool IsInSandbox()
        {
#if UNITY_IOS
            return Hunter_Rate_IsInSandbox();
#endif
            return false;
        }

#endregion


    }
}
