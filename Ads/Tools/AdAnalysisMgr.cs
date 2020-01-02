using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class AdAnalysisMgr : TSingleton<AdAnalysisMgr>
    {
        private Dictionary<string, AdRecord> m_RecordMap;

        public class AdRecord
        {
            protected string m_Key;
            protected int m_RewardCount;

            public AdRecord(string key)
            {
                m_Key = key;

                m_RewardCount = PlayerPrefs.GetInt(m_Key, 0);
            }

            public void AddRewardCount()
            {
                ++m_RewardCount;
                PlayerPrefs.SetInt(m_Key, m_RewardCount);

                if (m_RewardCount % 10 == 0)
                {
                    DataAnalysisMgr.S.CustomEventWithAppsflyer(string.Format("{0}-{1}", m_Key, m_RewardCount));
                }

                if (m_RewardCount / 10 == 0)
                {
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_IPU_KEY,string.Format("IPU_{0}",m_RewardCount));
                }
                else if(m_RewardCount % 5 == 0)
                {
                    DataAnalysisMgr.S.CustomEvent(DataAnalysisDefine.AD_IPU_KEY, string.Format("IPU_{0}", m_RewardCount));
                }
            }
        }

        public void RecordAdReward(string adInterface)
        {
            if (m_RecordMap == null)
            {
                m_RecordMap = new Dictionary<string, AdRecord>();
            }

            AdRecord record = null;

            if (!m_RecordMap.TryGetValue(adInterface, out record))
            {
                record = new AdRecord(adInterface);
                m_RecordMap.Add(adInterface, record);
            }

            record.AddRewardCount();
        }
    }
}
