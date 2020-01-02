using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Hunter
{
    public interface ISDKAdapter
    {
        int GetPriorityScore();
        bool InitWithConfig(SDKConfig config, SDKAdapterConfig adapterConfig);
    }
}
