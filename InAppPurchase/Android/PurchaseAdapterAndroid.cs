using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class PurchaseAdapterAndroid : PurchaseAdapter
    {
        public override bool IsSupportPurchase
        {
            get
            {
                if (I18Mgr.S.language == SystemLanguage.ChineseSimplified)
                {
                    return true;
                }

                return true;
            }
        }
    }
}
