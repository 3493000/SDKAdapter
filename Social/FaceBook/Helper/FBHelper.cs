﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class FBHelper : TSingleton<FBHelper>
    {
        public void Login()
        {
            if (FacebookSocialAdapter.S.isPublishLoggedIn)
            {
                return;
            }

            UIMgr.S.OpenTopPanel(SDKUI.FBLoginPanel, null);
        }

        public void Logout()
        {
            if (!FacebookSocialAdapter.S.isPublishLoggedIn)
            {
                return;
            }

            UIMgr.S.OpenTopPanel(SDKUI.FBLogoutPanel, null);
        }
    }
}
