using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;
using Facebook.Unity;

namespace Hunter
{
    public class FacebookDataAdapter : IDataAnalysisAdapter
    {
        public string Platform
        {
            get
            {
                return "facebook";
            }
        }

        public bool InitWithConfig(SDKConfig config, SDKAdapterConfig adapterConfig)
        {
            if (!FB.IsInitialized)
            {
                FB.Init();
            }        
            return true;
        }

        public void CustomEvent(string eventID, string label = null, Dictionary<string, string> dic = null)
        {
            if (!FB.IsInitialized)
            {
                return;
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(label))
            {
                param = new Dictionary<string, object>()
                {
                    { AppEventParameterName.Description, label }
                };
            }
            if (dic != null)
            {
                foreach (var key in dic.Keys)
                {
                    param.Add(key, dic[key]);
                }
            }

            try
            {
                FB.LogAppEvent(eventID, 1, param);
            }
            catch (Exception e)
            {
                return;
            }
            
        }

        public void CustomValueEvent(string eventID, float value, string label = null,
            Dictionary<string, string> dic = null)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(label))
            {
                param.Add(AppEventParameterName.Description, label);
            }

            if (dic != null) 
            {
                foreach (var key in dic.Keys)
                {
                    param.Add(key, dic[key]);
                }
            }

            FB.LogAppEvent(eventID, value, param);
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
            FB.LogAppEvent(
                DataAnalysisDefine.EVENTID_STARTLEVEL,
                1,
                new Dictionary<string, object>()
                {
                    { AppEventParameterName.Level, levelID }
                });
        }

        public void LevelComplate(string levelID)
        {
            FB.LogAppEvent(
                DataAnalysisDefine.EVENTID_OVERLEVEL,
                1,
                new Dictionary<string, object>()
                {
                    { AppEventParameterName.Level, levelID }
                });
        }

        public void LevelFailed(string levelID, string reason)
        {
            FB.LogAppEvent(
                DataAnalysisDefine.EVENTID_DYING,
                1,
                new Dictionary<string, object>()
                {
                    { AppEventParameterName.Level, levelID },
                    { AppEventParameterName.Description, reason }
                });
        }

        public void OnApplicationQuit()
        {
            //FB.LogAppEvent(DataAnalysisDefine.QUIT_APP);
        }

        public void Pay(double cash, double coin)
        {
            FB.LogPurchase(
                (float)cash, 
                "USD", 
                new Dictionary<string, object>()
                {
                    { AppEventParameterName.Description, coin.ToString() }
                });
        }

        public void SetUserLevel(int level)
        {
            
        }

        public void CustomEventDic(string eventId, Dictionary<string, string> dic)
        {
        }

        public void CheckRemoteConfig()
        {
            
        }
    }
}
