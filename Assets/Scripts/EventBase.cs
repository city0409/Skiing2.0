using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventBase 
{
    private List<Action> _action;

    public bool KeepOnLevelChanging { get; protected set; }//KeepOnLevelChanging保持消息在场景切换的时候

    public  void Publish() 
	{
        if (_action == null) return;
        foreach (var action in _action)
        {
            action();
        }
	}
	
	public  void Subscribe ( Action action) 
	{
        if (_action == null)
        {
            _action = new List<Action>();
        }
        if (!_action.Contains(action))
        {
            _action.Add(action);
        }
	}

    public void UnSubscribe(Action action)
    {
        if (_action==null)
        {
            return;
        }
        if (!_action.Contains(action))
        {
            _action.Remove(action);
        }
    }

    public void Clear()
    {
        if (_action==null)
        {
            return;
        }
        _action.Clear();
    }
}
