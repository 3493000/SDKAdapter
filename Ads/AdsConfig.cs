using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    [Serializable]
    public class AdsConfig
    {
        public bool isEnable = true;
        public float interstitialAdCoolingTime = 30;
        public float rewardedVideoAdCoolingTime = 30;

        //public WebeyeAdsConfig webeyeAdsConfig;
          public AdmobAdsConfig admobConfig;
        //public FacebookAdsConfig facebookConfig;
        //public MoPubAdsConfig moPubAdsConfig;
        //public VungleAdsConfig vungleAdsConfig;
        //public ApplovinAdsConfig applovinAdsConfig;
        //public UnityAdsConfig unityConfig;
        //public IronSourceAdsConfig ironSourceConfig;
        ////public PolyAdsConfig polyAdsConfig;        
        //public AdColonyAdsConfig adColonyAdsConfig;
        //public ChartBoostAdsConfig chartAdsConfig;
        ////public OutAdsConfig outAdsConfig;
        //public FyberAdsConfig fyberAdsConfig;

        //public MintegralAdsConfig mintegralAdsConfig;

        //public MinteAdsConfig mintegralConfig;
    }
}
