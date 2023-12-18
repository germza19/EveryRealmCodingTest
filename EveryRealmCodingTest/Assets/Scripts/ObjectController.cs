using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public List<PerformAction> performActions {  get; private set; }

    private RotateAction _rotateAction = new RotateAction(30f);
    private ChangeSizeAction _changeSizeAction = new ChangeSizeAction(5f, 2f, 0.5f);
    private ChangeColorAction _changeColorAction = new ChangeColorAction();

    private void Awake()
    {
        performActions = new List<PerformAction>();
        AddActionsToList();
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
                if(performAction.hasBeenActivated)
                {
                    performAction.DoAction(transform);
                }
            }
        }
    }
    public void HandleDeselection()
    {
        foreach (PerformAction performAction in performActions)
        {
            if (performAction != null)
            {
                if (performAction.hasBeenActivated)
                {
                    performAction.StopAction(transform);
                }
            }
        }
    }

    private void ResetActions()
    {
        performActions.Clear();
    }

    private void AddActionsToList()
    {
        performActions.Add(_rotateAction);
        performActions.Add(_changeSizeAction);
        performActions.Add(_changeColorAction);
    }
}



