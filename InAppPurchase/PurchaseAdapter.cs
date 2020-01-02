﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    public class PurchaseAdapter
    {
        public virtual bool IsPurchaseReady
        {
            get { return true; }
        }

        public virtual bool IsSupportPurchase
        {
            get
            {
                return true;
            }
        }

        public virtual void InitPurchaseInfo(string keyJson)
        {

        }

        public virtual void DoPurchase(string key)
        {

        }
    }
}
