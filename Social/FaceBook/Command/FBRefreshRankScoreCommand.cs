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
        public class FBRefreshRankScoreCommand : FBCommand
        {
        
            public void Execute(int limit)
            {
                if (!isExecuteAble)
                {
                    return;
                }

                OnExecuteBegin();
                RefreshRankScore(limit);
            }

            protected void RefreshRankScore(int limit)
            {
                FB.API(string.Format("/app/scores?fields=score,user.limit({0})", limit), HttpMethod.GET, (result) =>
                {
                    OnExecuteFinish();

                    if (!string.IsNullOrEmpty(result.Error))
                    {
                        EventSystem.S.Send(SDKEventID.OnFBRefreshRankScoreEvent, false);
                    }
                    else
                    {

                        FacebookSocialAdapter.S.OnRefreshScoreCallBack(result);

                        EventSystem.S.Send(SDKEventID.OnFBRefreshRankScoreEvent, true);
                    }
                });
            }
        }
    }
}