using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public abstract class PerformAction
{
    public bool hasBeenActivated = false;
    public bool shouldPerformAction = false;

    public CancellationTokenSource cancellationTokenSource;

    public abstract void ChangeActivationState();
    public abstract void DoAction(Transform transform);
    public abstract void StopAction(Transform transform);
    public abstract void CancelTask();
}
