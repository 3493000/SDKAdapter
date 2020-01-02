using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter
{

    public class ConfigMgr : TSingleton<ConfigMgr>
    {
        private const string STARTTIMEKEY = "key_firstopen_second";
        private const string APPSTATEKEY = "key_app_state";
        private bool m_IsBigBoomOpen = false;
        private long m_FirstOpenTimeTicks;
        public override void OnSingletonInit()
        {
            string timeValue = PlayerPrefs.GetString(STARTTIMEKEY, "");

            if (string.IsNullOrEmpty(timeValue))
            {
                m_FirstOpenTimeTicks = Helper.GetCurrentTimeSecond();
                PlayerPrefs.SetString(STARTTIMEKEY, m_FirstOpenTimeTicks.ToString());
            }
            else
            {
                m_FirstOpenTimeTicks = long.Parse(timeValue);
            }

        }

        public bool isBigBoomOpen
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                string attr = PlayerPrefs.GetString("AppsFlyerAttr", "");
                return attr.Trim().ToLower().Equals("non-organic");
#endif
            }
        }

        public bool GetBigBoomStateByTime(long ticks)
        {


            return true;
        }

        public bool isAppOnline
        {
            get
            {
#if UNITY_EDITOR                               
                return true;
#else
                if(TDRemoteConfigTable.GetData(APPSTATEKEY) != null)
                {
                    return TDRemoteConfigTable.GetData(APPSTATEKEY).value.Trim().ToLower().Equals("online") ;
                }
                else
                {
                    return true;
                }
                
#endif
            }
        }

    }
}