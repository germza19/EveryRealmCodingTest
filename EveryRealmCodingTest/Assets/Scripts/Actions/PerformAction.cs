using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class PerformAction : ScriptableObject
{
    public bool shouldPerformAction = false;

    public CancellationTokenSource cancellationTokenSource;
    public virtual void DoAction(Transform transform)
    {

    }
    public virtual void StopAction(Transform transform)
    {

    }
    public virtual void CancelTask()
    {

    }
}
