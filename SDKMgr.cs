using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

namespace Hunter
{
    [TMonoSingletonAttribute("[SDK]/SDKMgr")]
    public class SDKMgr : TMonoSingleton<SDKMgr>
    {

        public void Init()
        {
            Log.i("Init[SDKMgr]");
            BuglyMgr.S.Init();
            DataAnalysisMgr.S.Init();
            AdsMgr.S.Init();
            SocialMgr.S.Init();
            FacebookSocialAdapter.S.Init();

        }

    }


}
