using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private List<PerformAction> _performActions = new List<PerformAction>();
    private RotateAction _rotateAction = new RotateAction(30f);
    private ChangeSizeAction _changeSizeAction = new ChangeSizeAction(5f, 2f, 0.5f);

    public bool enableActions;
    public bool isSelected = false;

    private void Awake()
    {
        AddActionsToList();
    }
    private void OnEnable()
    {
        ResetActions();
    }
    private void OnDisable()
    {
        ResetActions();
    }

    public void DoActions()
    {
        foreach (PerformAction performAction in _performActions)
        {
            if (performAction != null)
            {
                performAction.DoAction(transform);
            }
        }
    }
    public void StopActions()
    {
        foreach (PerformAction performAction in _performActions)
        {
            if (performAction != null)
            {
                performAction.StopAction(transform);
            }
        }
    }

    private void ResetActions()
    {
        foreach (PerformAction performAction in _performActions)
        {
            if (performAction != null)
            {
                performAction.hasBeenActivated = false;
            }
        }
    }

    private void AddActionsToList()
    {
        _performActions.Add(_rotateAction);
        _performActions.Add(_changeSizeAction);
    }
}



