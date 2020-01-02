using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    [Serializable]
    public class AdmobAdsConfig : SDKAdapterConfig
    {
        public override string adapterClassName
        {
            get
            {
                return "Hunter.AdmobAdsAdapter";
            }
        }

        public string appIDAndroid;
        public string appIDIos;
        public string mopubId;
    }
}
