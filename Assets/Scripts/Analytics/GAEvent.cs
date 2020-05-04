using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAEvent : MonoBehaviour
{
    public string musicDesignEvent;

    public void SendDesignEvent(string eventName)
    {
        GameAnalytics.NewDesignEvent(eventName);
    }

    public void SendDesignEvent_Music(float eventValue)
    {
        GameAnalytics.NewDesignEvent(musicDesignEvent, eventValue);
    }

    public void SendProgressionEventComplete(string eventName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, eventName);
    }
}
