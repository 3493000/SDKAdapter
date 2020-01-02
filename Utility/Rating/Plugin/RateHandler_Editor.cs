using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hunter.Plugin
{
    public class RateHandler_Editor : RateHandler
    {


        #region Features

        public override void RequestReview()
        {
            UIMgr.S.OpenTopPanel(EngineUI.RatePanel, null);
        }

        public override void OpenRatingPage()
        {
            Log.i("No rating page for editor.");
        }

        #endregion


    }
}
