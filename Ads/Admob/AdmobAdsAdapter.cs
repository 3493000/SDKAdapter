using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.IronSource;
using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.AppLovin;
using GoogleMobileAds.Api.Mediation.Vungle;
using GoogleMobileAds.Api.Mediation.Tapjoy;
using GoogleMobileAds.Api.Mediation.Chartboost;
using GoogleMobileAds.Api.Mediation.MoPub;
using GoogleMobileAds.Api.Mediation.AdColony;
using GoogleMobileAds.Api.Mediation.InMobi;


namespace Hunter
{
    public class AdmobAdsAdapter : AbstractAdsAdapter
    {
        private static AdmobAdsConfig m_Config;
        public static bool IsStartProcessFinish = false; 
        string appId;
        protected override bool DoAdapterInit(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            //AudienceNetwork.AdSettings.AddTestDevice("6275f42bceb568babc15c283a4b9de8b");
            
            m_Config = adapterConfig as AdmobAdsConfig;


#if UNITY_ANDROID
             appId = m_Config.appIDAndroid;
#elif UNITY_IPHONE
             appId = m_Config.appIDIos;
#else
             appId = "unexpected_platform";
#endif
            //MobileAds.Initialize(appId.Trim());
            MobileAds.Initialize(
                 initStatus => {
                     Debug.Log("Admob ADS Initialize Call Back Success!");
                     if (IsStartProcessFinish)
                     {
                         AdsMgr.S.PreloadAllAd();
                     }
                 });// 需要等到回调发生后再加载广告，因为这可确保初始化所有的中介适配器。 
            AppLovin.Initialize();
            MoPub.Initialize(SDKConfig.S.adsConfig.admobConfig.mopubId);//ToDo配置在sdkconfig里
            return true;
        }

        public override string adPlatform
        {
            get
            {
                return "admob";
            }
        }

        public override int adPlatformScore
        {
            get
            {
                return 3;
            }
        }

        public override AdHandler CreateBannerHandler()
        {
            AdHandler handler = new AdmobBannerHandler();
            return handler;
        }

        public override AdHandler CreateInterstitialHandler()
        {
            AdHandler handler = new AdmobInterstitialHandler();
            return handler;
        }

        public override AdHandler CreateRewardVideoHandler()
        {
            AdHandler handler = new AdmobRewardedVideoHandler();
            return handler;
        }

        public static AdRequest BuildRequest(TDAdConfig data)
        {
            var builder = new AdRequest.Builder();
            if (m_Config.isDebugMode)
            {
                builder.AddTestDevice("54A21F94407E31BD8A20879613096F8B");
            }

            if (!string.IsNullOrEmpty(data.keyword))
            {
                builder.AddKeyword(data.keyword);
            }
            builder.SetGender((Gender)data.gender);
            if (data.isBirthDayConfiged)
            {
                builder.SetBirthday(data.GetBirthDayTime());
            }

            if (data.forFamilies)
            {
                builder.AddExtra("is_designed_for_families", "true");
            }

            if (data.forChild)
            {
                builder.TagForChildDirectedTreatment(true);
            }

            return builder.Build();
        }
    }
}
