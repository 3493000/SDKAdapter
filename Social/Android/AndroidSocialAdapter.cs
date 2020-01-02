using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AndroidSocialAdapter : AbstractSDKAdapter, ISocialAdapter
    {
        protected AndroidJavaObject m_ActivityObject;
        protected AndroidJavaClass m_ShareClass;
        protected AndroidJavaClass m_recommendClass;


        public AndroidJavaObject activityObject
        {
            get
            {
                if (m_ActivityObject == null)
                {
                    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    m_ActivityObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
                }

                return m_ActivityObject;
            }
        }

        public AndroidJavaClass shareClass
        {
            get
            {
                if (m_ShareClass == null)
                {
                    m_ShareClass = new AndroidJavaClass("com.vega.share.AndroidShare");
                }

                return m_ShareClass;
            }
        }

        public AndroidJavaClass recommendClass
        {
            get
            {
                if (m_recommendClass == null)
                {
                    m_recommendClass = new AndroidJavaClass("com.vega.share.MarketHelper");
                }
                return m_recommendClass;
            }
        }

        public bool supportShare2Social
        {
            get
            {
                return true;
            }
        }

        protected override bool DoAdapterInit(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            return true;
        }

        public void OpenMarketRatePage()
        {
            Application.OpenURL(GetMarketDetailPageURL());
        }

        public void OpenMarketDownloadPage(string identifyer)
        {
            Application.OpenURL(string.Format("market://details?id={0}", identifyer));
        }

        public string GetMarketDetailPageURL()
        {
            return string.Format("market://details?id={0}", Application.identifier);
        }

        public void ReportAchievementsUI(string achievementID, double progress)
        {

        }

        public void ReportScore(string leaderboard, long score)
        {

        }

        public void ShareTextWithURL(string title, string msg, string url)
        {
#if UNITY_ANDROID
            ShareTextInAnroid(title, msg, url);
#endif
        }

        public void ShareTextWithURLByPackage(string title, string msg, string url, string package)
        {
#if UNITY_ANDROID
            ShareTextInAnroidByPackage(title, msg, url, package);
#endif
        }

        public void ShowAchievmentsUI()
        {

        }

        public void ShowLeaderboardUI()
        {

        }

        public void ShareImage(string title, string path)
        {
#if !UNITY_EDITOR
            var share = shareClass;
            if (share == null)
            {
                return;
            }
            share.CallStatic("sharePicture", activityObject, title, path);
#endif
        }

        public void RecommandApp(string AppId)
        {
            var recommand = recommendClass;
            if (recommand == null)
            {
                return;
            }
            recommand.CallStatic("openMarket", activityObject, AppId);
        }

        public void NotificationMessage(string title, string message, System.DateTime newDate)
        {

        }

        public void CleanNotification()
        {

        }

#if UNITY_ANDROID
        public void ShareTextInAnroid(string title, string msg, string url)
        {
            //Create intent for action send
            AndroidJavaClass intentClass =
                new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject =
                new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>
                ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>
                ("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), title);
            intentObject.Call<AndroidJavaObject>
                ("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), msg + "\n" + url);

            //call createChooser method of activity class
            AndroidJavaObject chooser =
                intentClass.CallStatic<AndroidJavaObject>
                ("createChooser", intentObject, title);
            activityObject.Call("startActivity", chooser);
        }

        //public void 
#endif

#if UNITY_ANDROID
        public void ShareTextInAnroidByPackage(string title, string msg, string url, string package)
        {
            shareClass.CallStatic("shareText", activityObject, title, msg + "\n" + url, package);
        }
#endif

    }
}
