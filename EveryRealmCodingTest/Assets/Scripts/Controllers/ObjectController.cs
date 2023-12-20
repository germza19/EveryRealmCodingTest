using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private List<PerformAction> performActions;

    private void Awake()
    {
        performActions = new List<PerformAction>();
    }
    public void HandleSelection()
    {
        foreach (PerformAction performAction in performActions)
        {
            if (performAction != null)
            {
                performAction.DoAction(transform);
            }
        }
    }
    public void HandleDeselection()
    {
        foreach (PerformAction performAction in performActions)
        {
            if (performAction != null)
            {
                performAction.StopAction(transform);
            }
        }
    }
    public void HandleActionButton(PerformAction performAction)
    {
        if (!performActions.Contains(performAction))
        {
            performActions.Add(performAction);
        }
        else
        {
            performActions.Remove(performAction);
        }
    }

    public List<PerformAction> GetCurrentActions()
    {
        return performActions;
    }
}



