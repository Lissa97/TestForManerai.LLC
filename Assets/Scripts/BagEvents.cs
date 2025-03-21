using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

static class BagEvents
{ 
    public enum EventType
    {
        putInBag,
        removeFromBag
    }
    /// <summary>
    /// Collection of event subscribers
    /// </summary>
    private static Dictionary<EventType, UnityAction<int>> eventActions = new();
    static public void SubscribeOn(EventType type, UnityAction<int> action)
    {
        if(eventActions.ContainsKey(type))
        {
            eventActions[type] += action;
            return;
        }

        eventActions.Add(type, action);
    }

    static public void Invoke(EventType type,  int id)
    {
        if(!eventActions.ContainsKey(type)) return;
        eventActions[type]?.Invoke(id);
    }

    static public void Unsubscribe(EventType type, UnityAction<int> action)
    {
        if (!eventActions.ContainsKey(type)) return;

        eventActions[type] -= action;
    }
}

