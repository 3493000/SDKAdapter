using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;
using UnityEngine.UI;
using Facebook.Unity;

namespace Hunter
{

    public partial class FacebookSocialAdapter
    {
        public class FBRefreshSelfScoreCommand : FBCommand
        {
        
            public void Execute()
            {
                if (!isExecuteAble)
                {
                    return;
                }

                OnExecuteBegin();
                RefreshSelfScore();
            }

            protected void RefreshSelfScore()
            {
                FB.API("/me/scores", HttpMethod.GET, (result) =>
                {
                    OnExecuteFinish();
                    
                    if (!string.IsNullOrEmpty(result.Error))
                    {
                        EventSystem.S.Send(SDKEventID.OnFBRefreshSelfScoreEvent, false);
                    }
                    else
                    {

                        FacebookSocialAdapter.S.OnRefreshScoreCallBack(result);

                        EventSystem.S.Send(SDKEventID.OnFBRefreshSelfScoreEvent, true);
                    }
                });
            }
        }
    }
}
