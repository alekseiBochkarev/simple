using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsBackground : MonoBehaviour
{
    private void Start()
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();
    }
}
