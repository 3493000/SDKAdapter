using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class ADLabelDefine
    {
        public static string CLKICK = "click";
        public static string REWARD = "reward";
        public static string ABANDON = "abandon";
        public static string SHOW = "show";
        public static string IMPRESSION = "impression";
        public static string LOADED_SUCCESS = "load_success";
        public static string LOADED_FAILED = "load_failed";
        public static string LOADED_REQUEST = "load_request";
        public static string INIT = "init";
    }


    public class DataAnalysisDefine
    {

        public static string EVENTID_STARTLEVEL = "Event_StartLevel";
        public static string EVENTID_OVERLEVEL = "Event_OverLevel";
        public static string EVENTID_DYING = "Event_Dying";
        public static string EVENTID_REVIVE = "Event_ReviveLevel";

        public static string LEVELID_BEGIN = "Level_Begin";
        public static string LEVELID_OVER = "Level_Over";
        public static string LEVELID_SCORE = "Level_Score";

        public static string CHALLENGE_LEVENT = "Level_Challenge";

        public static string SHARE_POSTCARD = "Share2Social";

        public static string PURCHASE_REQUEST = "PurchaseRequest";
        public static string PURCHASE_FAILED = "PurchaseFailed";
        public static string PURCHASE_SUCCESS = "PurchaseSuccess";
        public static string PURCHASE_CANCEL = "PurchaseCancel";
        public static string PURCHASE_PRODUCT_FAILED = "ProductRequestFailed";

        public static string CROSS_EXT = "CrossExt";

        public static string SHOW_INTERSTITIAL = "ShowInterstitial";
        public static string SHOW_REWARD_VIDEO = "ShowRewardedVideo";

        public static string REWARD_VIDEO_STATE = "RewardVideoState";
        public static string INTERSTITIAL_STATE = "InterstitialState";

        public static string REWARD_VIDEO_IPU = "IPU_RewardVideo";
        public static string INTERSTITIAL_IPU = "IPU_Interstitial";

        public static string IMPRESSION_VIDEO = "Impression_Video";
        public static string IMPRESSION_INTERSTITIAL = "Impression_Inter";

        public static string OPEN_MARKET = "OpenMarketRatePage";

        public static string AD_DISPLAY = "ShowAD";

        public static string PANEL_EVENT = "PanelEvent";
        public static string SINGLE_PANEL_EVENT = "SinglePanelEvent";

        
        public static string FAKE_APP = "FakeApp";
        public static string DOWNLOAD_OFFICIAL_VERSION = "DownloadOfficial";
        public static string OPEN_OFFICIAL_AD_PANEL = "OpenOfficialAdPanel";


        public static string LOGIN_COUNT_INDAY = "Login_Count_InDay";
        public static string LOGIN_OUTDAY = "Login_Count_OutDay";
        public static string LOGIN_DAILY_COUNT = "Login_Daily_Count";
        public static string LOGIN_DAILY_TIMES = "Login_Daily_Times";

        public static string AD_SHOW_COUNT = "Ad_Show_Count";

        public static string AD_REQUEST_PLATFORM_COUNT = "ad_request_count_placement";
        public static string AD_FILL_PLATFORM_COUNT = "ad_fill_count_placement";
        public static string AD_REQUEST_UNITID_COUNT = "ad_request_count_unitid";
        public static string AD_FILL_UNITID_COUNT = "ad_fill_count_unitid";
        public static string AD_SHOW_UNITID_COUNT = "ad_show_count_unitid";
        public static string AD_GET_STATE_KEY = "ad_get_state_key";

        public static string W_AD_TRIGGER = "w_ad_trigger";
        public static string AD_IPU_KEY = "ad_ipu_key";
        public static string AF_AD_CLICK_TYPE = "type";
        public static string W_TOKEN_ACQUIRE = "w_token_acquire";
        public static string AF_AD_REQUEST = "w_ad_request";
        public static string AF_AD_FILL = "w_ad_fill";
        public static string AF_AD_IMP = "w_ad_imp";
        public static string AF_AD_SHOW = "w_ad_show";
        public static string AF_AD_CLICK = "w_ad_click";
        public static string AF_AD_CLOSE = "w_ad_close";
        public static string AF_AD_REWARD = "w_ad_reward";
        public static string AF_PID = "pid";
        public static string AF_SDK_NAME = "sdk_name";
        public static string AF_SDK_ECPM = "ecpm";
        public static string AF_SDK_VALUE = "af_revenue";
        public static string AD_SDK_ITEM_ID = "item_id";
        public static string PLAY_TIME = "w_play_time";
        public static string W_RATE_STAR = "w_rate_star";
        public static string W_OPEN_MARKET = "w_open_market_rate_page";
        public static string W_GUIDE = "w_guide";
        public static string W_ENGAGEMENT = "w_engagement";

        public static List<string> PLATFORMLIST = new List<string>
        {
            "mopub",  "adx", "admob","facebook", "applovin" ,"max", "unity", "fyber", "ironsource", "adcolony", "chartboost", "vungle", "displayio", "alt", "senjoy", "appnext", "bat", "inmobi" ,"inneractive", "csj","gdt","spread","dspmob"
            ,"wemob","360","4399","baidu","mobvista","oppo","vivo","xiaomi","creative","duadplatform","amazon","flurry",
            "tapjoy","nend","unknown"

        };

        public static Dictionary<float,List<string>> countryRatioDic = new Dictionary<float, List<string>>()
        {
            { 1,new List<string>(){  "Dutch","English","German","Swedish","German","Dutch", "Japanese","Norwegian", "Danish"}},
            { 0.6f,new List<string>(){"ChineseTraditional","Icelandic","English","French","Finnish","Spanish"}},
            { 0.36f, new List<string>(){"Korean","Spanish","Portuguese","Estonian","Slovenian","Latvian","Italian","Greek","Bulgarian"}},
            { 0.1f,new List<string>(){"Romanian","Belarusian","Polish","Slovak","Czech","SerboCroatian","Indonesian","Hungarian","Russian","Ukrainian"}}
        };

        public static List<string> WesdkPlatformList = new List<string>
        {
            "unknown","adcolony","admob","applovin","chartboost","facebook","ironsource","mopub","unity","dspmob","fyber","inmobi","vungle","adx",
            "creative","duadplatform","baidu","displayio","csj","gdt","amazon","flurry","tapjoy","360","xiaomi","4399",
            "oppo","vivo","mobvista","nend"

        };

        public static int GetPlatformIndexByWesdkIndex(int index)
        {
            int id = -1;
            if (PLATFORMLIST.Contains(WesdkPlatformList[index]))
            {
                id = PLATFORMLIST.IndexOf(WesdkPlatformList[index]);
                return id;
            }

            return 37;
        }
       
        }
    }





