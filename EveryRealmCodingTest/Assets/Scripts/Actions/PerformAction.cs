using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PerformAction
{
    public bool hasBeenActivated = false;
    public bool shouldPerformAction = false;
    public abstract void DoAction(Transform transform);
    public abstract void StopAction(Transform transform);
}
