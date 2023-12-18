using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public List<PerformAction> performActions;

    private void OnEnable()
    {
        ResetActions();
    }
    private void OnDisable()
    {
        ResetActions();
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

    private void ResetActions()
    {
        foreach (PerformAction performAction in performActions)
        {
            if (performAction != null)
            {
                performAction.CancelTask();
            }
        }
        performActions.Clear();
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
}



