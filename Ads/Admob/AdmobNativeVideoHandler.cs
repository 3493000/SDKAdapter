using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;
using GoogleMobileAds.Api;

namespace Hunter
{
    public class AdmobNativeVideoHandler : MonoBehaviour, ISingleton
    {
        private static AdmobNativeVideoHandler s_Instance;
        private GameObject icon;
        //private GameObject headline;

        private bool IconUnifiedNativeAdLoaded;
       // private bool HeadlineUnifiedNativeAdLoaded;

        private UnifiedNativeAd IconNativeAd;
        //private UnifiedNativeAd HeadlineNativeAd;

        private int IconRequestId = -1;

        public static AdmobNativeVideoHandler S
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = MonoSingleton.CreateMonoSingleton<AdmobNativeVideoHandler>();
                }
                return s_Instance;
            }
        }

        void ISingleton.OnSingletonInit()
        {

        }

        public void Init()
        {
            IconNativeADInit();
            EventSystem.S.Register(EventID.OnRefreshIconNativeAD, RefreshIconAD);

            // HeadlineNativeADInit();
            // EventSystem.S.Register(EventID.OnRefreshHeadLineNativeAD, RefreshHeadLineAD);
        }

        public bool IsIconNativeAdReady
        {
            get { return IconUnifiedNativeAdLoaded; }
        }
      
        public GameObject ShowNativeIconAD()
        {
            return icon;
        }
       
        public void RefreshIconAD(int key, params object[] param)
        {
            IconUnifiedNativeAdLoaded = false;
            RequestIconNativeAd();
        }
       
        private void IconNativeADInit()
        {
            RequestIconNativeAd();
        }
       
        private void RequestIconNativeAd()
        {
            AdLoader adLoader = new AdLoader.Builder("ca-app-pub-3940256099942544/2247696110")  //ca-app-pub-3940256099942544/2247696110 test ad
                .ForUnifiedNativeAd()
                .Build();
            adLoader.OnUnifiedNativeAdLoaded += this.HandleIconUnifiedNativeAdLoaded;
            adLoader.OnAdFailedToLoad += HandleIconNativeAdFailedToLoad;
            adLoader.LoadAd(new AdRequest.Builder().Build());
        }
       
        private void HandleIconUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
        {
            Log.i("Unified icon native ad loaded.");
            this.IconNativeAd = args.nativeAd;
            this.IconUnifiedNativeAdLoaded = true;

            // Get Texture2D for icon asset of native ad.
            Texture2D iconTexture = this.IconNativeAd.GetIconTexture();

            icon = GameObject.CreatePrimitive(PrimitiveType.Quad);
            icon.transform.position = new Vector3(10000, 10000, 10000);
            icon.transform.localScale = new Vector3(1, 1, 1);
            icon.GetComponent<Renderer>().material.mainTexture = iconTexture;

            // Register GameObject that will display icon asset of native ad.
            if (!this.IconNativeAd.RegisterIconImageGameObject(icon))
            {
                // Handle failure to register ad asset.
                IconUnifiedNativeAdLoaded = false;
                if (IconRequestId == -1)
                {
                    IconRequestId = Timer.S.Post2Really(v => { RequestIconNativeAd(); IconRequestId = -1; }, 3, 1);
                }
            }           
        }

        private void HandleIconNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Log.i("Icon Native ad failed to load: " + args.Message);

            if (IconRequestId == -1)
            {
                IconRequestId = Timer.S.Post2Really(v => { RequestIconNativeAd(); IconRequestId = -1; }, 3, 1);
            }
        }

        //headline类型广告 可以用
        //public bool IsHeadlineNativeAdReady
        //{
        //    get { return HeadlineUnifiedNativeAdLoaded; }
        //}
        //public GameObject ShowNativeHeadLineAD()
        //{
        //    return headline;
        //}
        //public void RefreshHeadLineAD(int key, params object[] param)
        //{
        //    HeadlineUnifiedNativeAdLoaded = false;
        //    RequestHeadLineNativeAd();
        //}
        //private void HeadlineNativeADInit()
        //{
        //    RequestHeadLineNativeAd();
        //}
        //private void RequestHeadLineNativeAd()
        //{
        //    AdLoader adLoader = new AdLoader.Builder("ca-app-pub-3940256099942544/2247696110")
        //        .ForUnifiedNativeAd()
        //        .Build();
        //    adLoader.OnUnifiedNativeAdLoaded += this.HandleHeadlineUnifiedNativeAdLoaded;
        //    adLoader.OnAdFailedToLoad += HandleHeadlineNativeAdFailedToLoad;
        //    adLoader.LoadAd(new AdRequest.Builder().Build());
        //}
        //private void HandleHeadlineUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
        //{
        //    Debug.LogError("Unified native ad loaded.");
        //    this.HeadlineNativeAd = args.nativeAd;
        //    this.HeadlineUnifiedNativeAdLoaded = true;
                    
        //    // Create GameObject that will display the headline ad asset.
        //    headline = new GameObject();
        //    headline.AddComponent<TextMesh>();
        //    headline.GetComponent<TextMesh>().characterSize = 0.5f;
        //    headline.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        //    headline.GetComponent<TextMesh>().color = Color.black;

        //    // Get string of the headline asset.
        //    string headlineText = this.HeadlineNativeAd.GetHeadlineText();
        //    headline.GetComponent<TextMesh>().text = headlineText;

        //    // Add box collider to the GameObject which will automatically scale.
        //    headline.AddComponent<BoxCollider>();

        //    //if (!this.HeadlineNativeAd.RegisterIconImageGameObject(headline))
        //    //{
        //    //    // Handle failure to register ad asset.
        //    //    Timer.S.Post2Really(v => { RequestHeadLineNativeAd(); }, 3, 1);
        //    //    return;
        //    //}
        //}
        //private void HandleHeadlineNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        //{
        //    Debug.LogError("Native ad failed to load: " + args.Message);

        //    Timer.S.Post2Really(v => { RequestHeadLineNativeAd(); }, 3, 1);
        //}     
    }
}
