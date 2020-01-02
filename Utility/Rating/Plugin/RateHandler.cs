using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hunter.Plugin
{
    public class RateHandler
    {
        #region Features
        public virtual void RequestReview() { }
        public virtual void OpenRatingPage() { }
        public virtual bool IsInSandbox() { return false; }
        #endregion
    }
}
