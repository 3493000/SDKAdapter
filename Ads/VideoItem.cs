using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hunter
{
    public class VideoItem : IUListItemView
    {

        private AdHandler m_Handler;
        [SerializeField]
        private Button m_RequestButton;
        [SerializeField]
        private Button m_ShowButton;
        [SerializeField]
        private Text m_Platform;
        [SerializeField]
        private Text m_State;

        private ADVideoPanel m_Panel;

        private void Awake()
        {
            m_RequestButton.onClick.AddListener(DoRequestAdButton);
            m_ShowButton.onClick.AddListener(DoShowAdButton);
        }

        private void DoShowAdButton()
        {
            if (m_Handler != null)
            {
                m_Handler.ShowAd();

                if (m_Handler.isAdReady)
                {
                    m_Panel.OnValueUpdate(m_Handler.adAdapter.adPlatform);
                }
               
            }
        }

        private void DoRequestAdButton()
        {
            if (m_Handler != null)
            {
                m_Handler.PreLoadAd();
            }
        }

        public void DoInit(AdHandler handler, ADVideoPanel panel)
        {
            m_Handler = handler;
            m_Panel = panel;
            InitUI();
            DoRequestAdButton();
        }

        private void InitUI()
        {
            if(m_Handler == null)
            {
                return;
            }

            m_Platform.text = m_Handler.adAdapter.adPlatform;
        }

        private void Update()
        {
            if(m_Handler == null)
            {
                return;
            }

            if (m_Handler.adState == AdState.Loading)
            {
                m_State.text = "AD Loading";
                return;
            }
            else if (m_Handler.adState == AdState.Failed)
            {

                m_State.text = "AD Load Failed";
                return;
            }

            m_State.text = m_Handler.isAdReady.ToString();

        }
    }
}