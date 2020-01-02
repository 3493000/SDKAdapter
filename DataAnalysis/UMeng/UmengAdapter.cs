using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hunter;
using Umeng;

namespace Hunter
{
    public class UmengAdapter : AbstractSDKAdapter,IDataAnalysisAdapter
    {
        public string Platform
        {
            get
            {
                return "umeng";
            }
        }

        public void OnApplicationQuit()
        {
            
        }

        public void LevelBegin(string levelID)
        {
            GA.StartLevel(levelID);
        }

        public void LevelComplate(string levelID)
        {
            GA.FinishLevel(levelID);
        }

        public void LevelFailed(string levelID, string reason)
        {
           GA.FailLevel(levelID);
        }

        public void CustomEvent(string eventID, string label = null, Dictionary<string ,string> dic = null)
        {
            GA.Event(eventID, label);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {

        }

        public void Pay(double cash, double coin)
        {
#if UNITY_IPHONE
            GA.Pay(cash, GA.PaySource.AppStore, coin);
#elif UNITY_ANDROID
            GA.Pay(cash,GA.PaySource.Paypal,coin);
#elif UNITY_EDITOR
           
#endif
        }

        public void CustomEventDuration(string eventID, long duration)
        {
           
        }

        public void CustomEventMapValue(string key, string value)
        {
           
        }

        public void CustomEventMapSend(string eventID)
        {
           
        }

        protected override bool DoAdapterInit(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            UmengConfig umengConfg = adapterConfig as UmengConfig;
           
#if UNITY_IOS
            GA.StartWithAppKeyAndChannelId(umengConfg.iosAppKey.Trim(), umengConfg.appChannelId);
#else
            GA.StartWithAppKeyAndChannelId(umengConfg.androidAppKey.Trim(), umengConfg.appChannelId);
#endif
            GA.ProfileSignIn(AppsFlyer.getAppsFlyerId());
            Debug.Log(GA.GetTestDeviceInfo()); 
            return true;
        }

        public void SetUserLevel(int level)
        {
            GA.SetUserLevel(level);
        }
        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
            GA.Event(eventId, dic);
        }

        public void CheckRemoteConfig()
        {
            
        }
    }
}
