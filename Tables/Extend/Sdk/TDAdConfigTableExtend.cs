using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Hunter
{
    public partial class TDAdConfigTable
    {
        static void CompleteRowAdd(TDAdConfig tdData)
        {

        }

        public static List<TDAdConfig> GetAdDataByPlatform(string platform)
        {
            platform = platform.Trim().ToLower();

            List<TDAdConfig> result = new List<TDAdConfig>();

            for (int i = 0; i < m_DataList.Count; ++i)
            {
                if (m_DataList[i].adPlatform.Trim().ToLower() == platform)
                {
                    result.Add(m_DataList[i]);
                }
            }

            return result;
        }

        public static List<TDAdConfig> GetAdDataByInterface(string interfaceName)
        {
            List<TDAdConfig> result = new List<TDAdConfig>();

            for (int i = 0; i < m_DataList.Count; ++i)
            {
                if (m_DataList[i].adInterface == interfaceName)
                {
                    result.Add(m_DataList[i]);
                }
            }

            return result;
        }
        public static List<TDAdConfig> GetAdDataByPlatformAndType(string platform, int type)
        {
            List<TDAdConfig> result = new List<TDAdConfig>();

            var results = GetAdDataByPlatform(platform);

            for (int i = 0; i < results.Count; i++)
            {
                if (m_DataList[i].adType == type)
                {
                    result.Add(m_DataList[i]);
                }
            }
            return result;
        }

        //        public static void RefreshDataList(List<AdsUnitConfig> configList)
        //        {
        //            m_DataList.Clear();
        //            m_DataCache.Clear();

        //            for (int i = 0; i < configList.Count; i++)
        //            {
        //                var item = new TDAdConfig();

        //                item.id = configList[i].Id;
        //                item.adType = configList[i].AdType;
        //                item.adInterface = configList[i].AdInterface;
        //                item.adPlatform = configList[i].AdPlatform;
        //                item.ecpm = configList[i].Ecpm;
        //#if UNITY_ANDROID
        //                item.unitID = configList[i].UnitIDAndroid;               
        //#elif UNITY_IPHONE
        //                item.unitID = configList[i].UnitIDIos;
        //#endif
        //                item.Reset();
        //                m_DataList.Add(item);
        //                m_DataCache.Add(item.id, item);
        //            } 
        //            //AdsMgr.S.InitAllAdData();
        //        }
        //    }
    }
}