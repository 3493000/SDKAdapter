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
        public class FBLoginCommand : FBCommand
        {

            public void Execute(bool publishPermissions)
            {
                if (!isExecuteAble)
                {
                    return;
                }

                OnExecuteBegin();

                if (publishPermissions)
                {
                    PromptForPublishLogin();
                }
                else
                {
                    PromptForLogin();
                }
            }

            [Obsolete]
            protected void PromptForPublishLogin()
            {
                FB.LogInWithPublishPermissions(publishPermissions, delegate (ILoginResult result)
                {
                    OnExecuteFinish();

                    if (FB.IsLoggedIn)
                    {
                        FacebookSocialAdapter.S.OnLoginWithPublishPermission();
                        EventSystem.S.Send(SDKEventID.OnFBLoginEvent, true);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(result.Error))
                        {
                            Log.e(result.Error);
                        }
                        Log.e("#Facebook Not Logged In with public permission.");
                        EventSystem.S.Send(SDKEventID.OnFBLoginEvent, false);
                    }
                });
            }

            protected void PromptForLogin()
            {
                FB.LogInWithReadPermissions(readPermissions, delegate (ILoginResult result)
                {
                    OnExecuteFinish();

                    if (FB.IsLoggedIn)
                    {
                        FacebookSocialAdapter.S.OnLoginWithReadPermissions();
                        EventSystem.S.Send(SDKEventID.OnFBLoginEvent, true);
                    }
                    else
                    {
                        if (result.Error != null)
                        {
                            Log.e(result.Error);
                        }
                        Log.e("#Facebook Not Logged In");

                        EventSystem.S.Send(SDKEventID.OnFBLoginEvent, false);
                    }
                });
            }
        }
    }
}