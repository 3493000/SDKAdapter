//  Desc:        Framework For Game Develop with Unity3d
//  Copyright:   Copyright (C) 2019 Hunter. All rights reserved.
//  ！！！！！！！！！！！！！！！！！！！！！！！！
//  ！！！！！！！！！！！！！！！！！！！！！！！！
//  Author:      Hunter
//  E-mail:      3493000@qq.com
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hunter
{
    public enum SDKEventID
    {
        SDKEventIDMin = 2000000,

        OnNoAdModeChange,

        //FB
        OnFBRefreshPlayerInfoEvent, //ID, RESULT
        OnFBLoginEvent, //bool
        OnFBLogoutEvent,
        OnFBRefreshSelfScoreEvent,
        OnFBRefreshRankScoreEvent,
        OnFBRefreshFriendEvent,
        OnFBPostScoreEvent,
        OnFBRetrievePhoto,
        OnShareImageFinish,
        //
        OnPurchaseInitSuccess,
        OnPurchaseInitFailed,
        OnPurchaseSuccess,
        OnPurchaseFailed,
        OnFBRequestFinsh,
        OnAppWallClosed,
        OnInstanceIDGet,
        OnCountryCodeGet
    }
}