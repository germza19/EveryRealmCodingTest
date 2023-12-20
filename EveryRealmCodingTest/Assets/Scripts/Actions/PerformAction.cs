using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class PerformAction : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;
    public Sprite Sprite { get { return _iconSprite; } }

    protected bool shouldPerformAction = false;

    protected CancellationTokenSource cancellationTokenSource;
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
