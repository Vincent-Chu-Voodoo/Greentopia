using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<GameEventEnum, GameEvent> eventDictionary = new Dictionary<GameEventEnum, GameEvent>();

    public void StartListening(GameEventEnum eventName, UnityAction<object> listener)
    {
        GameEvent thisEvent = new GameEvent();
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Add more event to the existing one
            thisEvent.AddListener(listener);

            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent = new GameEvent();
            //Add event to the Dictionary for the first time
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(GameEventEnum eventName, UnityAction<object> listener)
    {
        GameEvent thisEvent = new GameEvent();
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent.RemoveListener(listener);

            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(GameEventEnum eventName, object eventParam)
    {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
            // OR USE  instance.eventDictionary[eventName](eventParam);
        }
    }
}
