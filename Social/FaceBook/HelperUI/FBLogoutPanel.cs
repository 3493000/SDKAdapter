using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;
using UnityEngine.UI;

namespace Hunter
{
    public class FBLogoutPanel : AbstractPanel
    {
        [SerializeField]
        private Button m_OKButton;
        [SerializeField]
        private Button m_CloseButton;

        protected override void OnUIInit()
        {
            m_CloseButton.onClick.AddListener(CloseSelfPanel);
            m_OKButton.onClick.AddListener(Logout2FB);
        }

        protected override void OnOpen()
        {
            OpenDependPanel(EngineUI.MaskPanel, -1);
        }

        private void Logout2FB()
        {
            if (FacebookSocialAdapter.S.isPublishLoggedIn)
            {
                FacebookSocialAdapter.S.LogOutSocialPlatform();
            }

            CloseSelfPanel();
        }
    }
}