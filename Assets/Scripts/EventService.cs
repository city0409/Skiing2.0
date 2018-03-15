using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EventService
{
    private readonly static EventService _instance = new EventService();
    private readonly Dictionary<Type, EventBase> _eventBases = new Dictionary<Type, EventBase>();

	public static EventService Instance { get { return _instance; } }

    private EventService() { }

    public T GetEvent<T>() where T: EventBase
	{
        Type eventType = typeof(T);
        if (!_eventBases.ContainsKey(eventType))
        {
            T e = Activator.CreateInstance<T>();//c#API ,反射 Activator
            _eventBases.Add(eventType, e);
        }
        return (T)_eventBases[eventType];
	}

    public void ClearAll()
    {
        foreach (EventBase e in _eventBases.Values)
        {
            e.Clear();
        }
        _eventBases.Clear();
    }

    public void ClearOnLevelChanging(int newLevelId)
    {
        foreach (EventBase e in _eventBases.Values.Where(e=>!e.KeepOnLevelChanging))
        {
            e.Clear();
        }
    }
}
//System.Linq;是为了(e=>!e.KeepOnLevelChanging )