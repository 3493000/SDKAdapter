using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Hunter {

    public class UtilityMgr : TSingleton<UtilityMgr> {

        public void Vibrate()
        {
#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
            Handheld.Vibrate();
#else
            return;
#endif
        }

        private Dictionary<string ,float> ratioDic = new Dictionary<string, float>();

        public override void OnSingletonInit()
        {
            InitCountryRatio();
        }

        private void InitCountryRatio()
        {
            foreach (var item in DataAnalysisDefine.countryRatioDic.Keys)
            {
                var list = DataAnalysisDefine.countryRatioDic[item];
                for (int i = 0; i < list.Count; i++)
                {
                    if (!ratioDic.ContainsKey(list[i]))
                    {
                        ratioDic.Add(list[i], item);
                    }
                }
            }
        }

        public float getRatio()
        {
            if (ratioDic == null || ratioDic.Count == 0)
            {
                InitCountryRatio();
            }

            string lang = Application.systemLanguage.ToString();
            if (ratioDic.ContainsKey(lang))
            {
                Log.i("lang ==>" + lang+ "ratio ==>" + ratioDic[lang]);
                return ratioDic[lang];
            }
            Log.i("nodic lang ==>" + lang + "ratio ==>" + 0.1f);
            return 0.1f;
        }


        private readonly Dictionary<string,float> ECPM_RATIO_DIC = new Dictionary<string, float>()
        {
            
        };

        //public string GetCountryCode()
        //{
        //    string countryCode = PlayerPrefs.GetString("m_prefix_Country");
        //    if (string.IsNullOrEmpty(countryCode))
        //    {
        //        getGeographicalCoordinates();
        //    }

        //    return countryCode;
        //}

        //public string playerPrefsKey = "m_prefix_Country";
        //private float m_EcpmRatio = 0.1f;
        //private float ecpmRatio
        //{
        //    get { return m_EcpmRatio; }
        //}

        //public void getGeographicalCoordinates()
        //{
        //    if (Input.location.isEnabledByUser)
        //        StartCoroutine(getGeographicalCoordinatesCoroutine());
        //}

        //private IEnumerator getGeographicalCoordinatesCoroutine()
        //{
        //    Input.location.Start();
        //    int maximumWait = 20;
        //    while (Input.location.status == LocationServiceStatus.Initializing && maximumWait > 0)
        //    {
        //        yield return new WaitForSeconds(1);
        //        maximumWait--;
        //    }

        //    if (maximumWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        //    {
        //        Input.location.Stop();
        //        yield break;
        //    }

        //    float latitude = Input.location.lastData.latitude;
        //    float longitude = Input.location.lastData.longitude;
        //    //      Asakusa.
        //    //      float latitude = 35.71477f;
        //    //      float longitude = 139.79256f;
        //    Input.location.Stop();
        //    WWW www = new WWW("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude +
        //                      "&sensor=true");
        //    yield return www;
        //    if (www.error != null) yield break;
        //    XmlDocument reverseGeocodeResult = new XmlDocument();
        //    reverseGeocodeResult.LoadXml(www.text);
        //    if (reverseGeocodeResult.GetElementsByTagName("status").Item(0).ChildNodes.Item(0).Value != "OK")
        //        yield break;
        //    string countryCode = null;
        //    bool countryFound = false;
        //    foreach (XmlNode eachAdressComponent in reverseGeocodeResult.GetElementsByTagName("result").Item(0)
        //        .ChildNodes)
        //    {
        //        if (eachAdressComponent.Name == "address_component")
        //        {
        //            foreach (XmlNode eachAddressAttribute in eachAdressComponent.ChildNodes)
        //            {
        //                if (eachAddressAttribute.Name == "short_name")
        //                    countryCode = eachAddressAttribute.FirstChild.Value;
        //                if (eachAddressAttribute.Name == "type" && eachAddressAttribute.FirstChild.Value == "country")
        //                    countryFound = true;
        //            }

        //            if (countryFound) break;
        //        }
        //    }

        //    if (countryFound && countryCode != null)
        //    {
        //        PlayerPrefs.SetString(playerPrefsKey, countryCode);
        //        RebuildEcpmValueRatio(countryCode);
        //        EventSystem.S.Send(SDKEventID.OnCountryCodeGet);
        //        Log.i("GetCountryCodeSuccess,Country code is" + countryCode);
        //    }

          
        //}

        //private void RebuildEcpmValueRatio(string countryCode)
        //{

        //    if (!ECPM_RATIO_DIC.TryGetValue(countryCode, out m_EcpmRatio))
        //    {
        //        m_EcpmRatio = 0.1f;
        //    }
        //}

        


    }
}