using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    [Serializable]
    public class DataAnalysisConfig
    {
        public bool isEnable = true;
       // public AppsflyerConfig appsflyerConfig;
        //public DataeyeConfig dataeyeConfig;
        //public GameAnalysisConfig gameAnalysisConfig;
        public UmengConfig umengConfig;
        public FacebookDataConfig facebookConfig;
    }

    //[Serializable]
    //public class DataeyeConfig : SDKAdapterConfig
    //{
    //    public string appID;

    //    public override string adapterClassName
    //    {
    //        get
    //        {
    //            return "Hunter.DataeyeAdapter";
    //        }
    //    }
    //}

    //[Serializable]
    //public class GameAnalysisConfig : SDKAdapterConfig
    //{
    //    public override string adapterClassName
    //    {
    //        get
    //        {
    //            return "Hunter.GameAnalysisAdapter";
    //        }
    //    }
    //}


    [Serializable]
    public class AppsflyerConfig : SDKAdapterConfig
    {
        public string appKey;

        public override string adapterClassName
        {
            get
            {
                return "Hunter.AppsflyerDataAdapter";
            }
        }
    }

    [Serializable]
    public class UmengConfig: SDKAdapterConfig
    {
        public string iosAppKey;
        public string androidAppKey;

        public string appChannelId;

        public override string adapterClassName
        {
            get
            {
                return "Hunter.UmengAdapter";
            }
        }
    }

    [Serializable]
    public class FacebookDataConfig : SDKAdapterConfig
    {
        public override string adapterClassName
        {
            get
            {
                return "Hunter.FacebookDataAdapter";
            }
        }
    }


}
