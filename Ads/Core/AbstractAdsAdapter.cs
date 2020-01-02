using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AbstractAdsAdapter : AbstractSDKAdapter, IAdAdapter
    {
        public virtual string adPlatform
        {
            get
            {
                return null;
            }
        }

        public virtual int adPlatformScore
        {
            get { return 0; }
        }

        public int platformIndex { get; set; }

        public virtual AdHandler CreateBannerHandler()
        {
            return null;
        }

        public virtual AdHandler CreateInterstitialHandler()
        {
            return null;
        }

        public virtual AdHandler CreateNativeAdHandler()
        {
            return null;
        }

        public virtual AdHandler CreateRewardVideoHandler()
        {
            return null;
        }

        public virtual void InitWithData()
        {

        }

        public virtual AdHandler CreateAppWallHandler()
        {
            return null;
        }
    }
}
