using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Hunter
{

    public class ADVideoPanel : AbstractPanel
    {
        [SerializeField]
        private GameObject m_PlatformPrefab;
        [SerializeField]
        private USimpleListView m_ListVideo;

        [SerializeField]
        private Text m_ProcessText;
        [SerializeField]
        private Button m_ShowProcessButton;
        [SerializeField]
        private Button m_BackButton;

        private Dictionary<String, int> m_ProcessMap = new Dictionary<string, int>();

        private const string VIDEO = "Reward0";
        private AdInterface m_ADInterFace = null;

        private List<AdHandler> m_ADhandler;
        StringBuilder m_Sbuilder = new StringBuilder();

        protected override void OnUIInit()
        {
            m_ShowProcessButton.onClick.AddListener(ShowProcessText);
            m_BackButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick()
        {
            CloseSelfPanel();
        }

        private void ShowProcessText()
        {
            if (m_ProcessMap.Count == 0)
            {
                m_ProcessText.text = "您暂时还未观看过广告";
                return;
            }

            string SBuilder = "已经看{0}平台{1}个广告";
            string resulttext = "";
            int Count = 0;
            foreach (var item in m_ProcessMap.Keys)
            {
                resulttext += string.Format(SBuilder, item, m_ProcessMap[item]);
                Count += m_ProcessMap[item];
            }

            m_ProcessText.text = "恭喜你，" + resulttext + "共计观看了" + Count + "个广告";
        }

        protected override void OnOpen()
        {
            GetAdHandler();
        }

        private void GetAdHandler()
        {
            m_ADInterFace = AdsMgr.S.GetAdInterface(VIDEO);
            if (m_ADInterFace != null)
            {
                m_ADhandler = m_ADInterFace.GetHandlerList();
                ProcessShowADItem();
            }

        }

        private void ProcessShowADItem()
        {
            if (m_ADhandler == null)
            {
                return;
            }
            m_ListVideo.SetDataCount(m_ADhandler.Count);

            m_ListVideo.SetCellRenderer(ListCellRenderder);
        }

        private void ListCellRenderder(Transform root, int index)
        {
            var item =  root.GetComponent<VideoItem>();
            item.DoInit(m_ADhandler[index], this);
        }

        public void OnValueUpdate(string platform)
        {
            //Save
            if (!m_ProcessMap.ContainsKey(platform))
            {
                m_ProcessMap.Add(platform, 1);
            }
            else
            {
                m_ProcessMap[platform]++;
            }

            SaveProcess(platform);
        }

        private void SaveProcess(string Platform)
        {
            if (!m_ProcessMap.ContainsKey(Platform))
            {
                return;
            }

            PlayerPrefs.SetInt(Platform, m_ProcessMap[Platform]);
        }
    }
}