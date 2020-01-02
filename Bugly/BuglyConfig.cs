using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hunter;

namespace Hunter
{
    [Serializable]
    public class BuglyConfig : SDKAdapterConfig
    {
        public string buglyID_Android;
        public string buglyID_iOS;
    }
}
