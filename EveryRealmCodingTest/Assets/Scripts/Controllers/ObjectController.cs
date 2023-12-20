using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private List<PerformAction> _performActions;
    public List<PerformAction> PerformActions { get { return _performActions; } }

    private void Awake()
    {
        _performActions = new List<PerformAction>();
    }
    public void HandleSelection()
    {
        foreach (PerformAction performAction in _performActions)
        {
            if (performAction != null)
            {
                performAction.DoAction(transform);
            }
        }
    }
    public void HandleDeselection()
    {
        foreach (PerformAction performAction in _performActions)
        {
            if (performAction != null)
            {
                performAction.StopAction(transform);
            }
        }
    }
    public void HandleActionButton(PerformAction performAction)
    {
        if (!_performActions.Contains(performAction))
        {
            _performActions.Add(performAction);
        }
        else
        {
            _performActions.Remove(performAction);
        }
    }
}



