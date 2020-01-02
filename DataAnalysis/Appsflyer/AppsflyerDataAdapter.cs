using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Hunter;

namespace Hunter
{
    public class AppsflyerDataAdapter : IDataAnalysisAdapter
    {
        public string Platform
        {
            get
            {
                return "appsflyer";
            }
        }

        public bool InitWithConfig(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            var name = AppsFlyerTrackerCallbacks.S.name;
            AppsflyerConfig appsflyerConfig = adapterConfig as AppsflyerConfig;
            AppsFlyer.setAppsFlyerKey(appsflyerConfig.appKey);
            if (appsflyerConfig.isDebugMode)
            {
                AppsFlyer.setIsDebug(true);
            }

#if UNITY_IOS
   /* Mandatory - set your apple app ID
      NOTE: You should enter the number only and not the "ID" prefix */
            AppsFlyer.setAppID(config.iosAppID);
            AppsFlyer.trackAppLaunch();
            AppsFlyer.getConversionData();
#elif UNITY_ANDROID
            /* Mandatory - set your Android package name */
            AppsFlyer.setAppID(Application.identifier);
            /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
            //AppsFlyer.init(appsflyerConfig.appKey, "AppsFlyerTrackerCallbacks");
            AppsFlyer.init(appsflyerConfig.appKey);
            //AppsFlyer.setCustomerUserID(AppsFlyer.getAppsFlyerId());            
            AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks");            
#endif

            return true;
        }

        public void CustomEvent(string eventID, string label = null, Dictionary<string,string> dic = null)
        {
            Dictionary<string, string> eventValue = new Dictionary<string, string>();
            if (label != null)
            {
                eventValue.Add("description", label);
            }

            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    eventValue.Add(key, dic[key]);
                }
            }

            AppsFlyer.trackRichEvent(eventID, eventValue);
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (DataAnalysisDefine.AF_AD_IMP.Equals(eventID))
            {
                param.Add(DataAnalysisDefine.AF_SDK_VALUE, value.ToString());
            }
            else
            {
                param.Add("revenue", value.ToString());
            }
            if (!string.IsNullOrEmpty(label))
            {
                param.Add("description", label);
            }
            if (dic != null) 
            {
                foreach (var key in dic.Keys)
                {
                    param.Add(key, dic[key]);
                }
            }

            AppsFlyer.trackRichEvent(eventID, param);
        }

        public void CustomEventDuration(string eventID, long duration)
        {

        }

        public void CustomEventMapSend(string eventID)
        {

        }

        public void CustomEventMapValue(string key, string value)
        {

        }

        public int GetPriorityScore()
        {
            return 0;
        }

        public void LevelBegin(string levelID)
        {
            
        }

        public void LevelComplate(string levelID)
        {
            
        }

        public void LevelFailed(string levelID, string reason)
        {
            
        }

        public void OnApplicationQuit()
        {
            
        }

        public void Pay(double cash, double coin)
        {
            Dictionary<string, string> eventValue = new Dictionary<string, string>();
            eventValue.Add("af_revenue", cash.ToString());
            eventValue.Add("af_content_id", coin.ToString());
            eventValue.Add("af_currency", "USD");

            AppsFlyer.trackRichEvent("af_purchase", eventValue);
        }

        public void SetUserLevel(int level)
        {
            
        }

        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
            if (dic != null)
            {
                AppsFlyer.trackRichEvent(eventId,dic);
            }
        }

        public void CheckRemoteConfig()
        {
           
        }
    }
}
