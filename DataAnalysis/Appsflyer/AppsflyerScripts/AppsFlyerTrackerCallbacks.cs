using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hunter;
using UnityEngine;
using System;

namespace Hunter
{
    [TMonoSingletonAttribute("AppsFlyerTrackerCallbacks")]
    public class AppsFlyerTrackerCallbacks : TMonoSingleton<AppsFlyerTrackerCallbacks>
    {

        private int m_CancleID = -1;

        public class JsonText
        {
            public string af_status;
        }
        public void didReceiveConversionData(string conversionData)
        {
            
            var JsonText = JsonUtility.FromJson<JsonText>(conversionData);
            string userP_ = PlayerPrefs.GetInt("PurchaseUserTag", 0) == 0 ?  JsonText.af_status : "purchaseUser";


            try
            {

                string appsFlyerAttr = PlayerPrefs.GetString("AppsFlyerAttr", "");
                if (string.IsNullOrEmpty(appsFlyerAttr))
                {
                    PlayerPrefs.SetString("AppsFlyerAttr", JsonText.af_status.Trim().ToLower());
                    DataAnalysisMgr.S.CustomEvent("AppsFlyerAttribute", JsonText.af_status.Trim().ToLower());
                }
                else
                {
                    if (JsonText.af_status.Trim().ToLower() != appsFlyerAttr)
                    {
                        Log.e("AppsFlyerTrackerCallbacks SaveAttr:" + appsFlyerAttr + "   CurrentAttr" + JsonText.af_status.Trim().ToLower());
                        DataAnalysisMgr.S.CustomEvent("AppsFlyerTrackerExpection", JsonText.af_status.Trim().ToLower());
                    }
                }
            }
            catch (System.Exception e)
            {
                Log.e("AppsFlyerTrackerCallbacks:" + e.Message + e.StackTrace);
            }

        }

        public void didReceiveConversionDataWithError(string error)
        {
        }

        public void didFinishValidateReceipt(string validateResult)
        {
        }

        public void didFinishValidateReceiptWithError(string error)
        {

        }

        public void onAppOpenAttribution(string validateResult)
        {
            
        }

        public void onAppOpenAttributionFailure(string error)
        {

        }

        public void onInAppBillingSuccess()
        {
            
        }

        public void onInAppBillingFailure(string error)
        {
            
        }

        public bool IsNonOrganicUser()
        {
            string attr = PlayerPrefs.GetString("AppsFlyerAttr", "");
            bool isNonOrganic = attr.Trim().ToLower().Equals("non-organic");
            return isNonOrganic;
        }

        public static long GetTimeStamp(bool bflag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long ret;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);
            return ret;
        }

        public override void OnSingletonInit()
        {

          
        }

    }

}



