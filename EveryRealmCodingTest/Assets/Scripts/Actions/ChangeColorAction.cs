using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ChangeColorAction : PerformAction
{

    private MaterialPropertyBlock _materialPropertyBlock;
    private Renderer _rend;

    public async override void DoAction(Transform transform)
    {
        shouldPerformAction = true;
        cancellationTokenSource = new CancellationTokenSource();
        await ChangeColorAsync(transform, cancellationTokenSource.Token);
    }

    private async Task ChangeColorAsync(Transform transform, CancellationToken cancellationToken)
    {
        _rend = transform.GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();

        while (!cancellationToken.IsCancellationRequested)
        {
            if (shouldPerformAction)
            {
                Color randomColor = new Color(Random.value, Random.value, Random.value);

                _materialPropertyBlock.SetColor("_Color", randomColor);

                _rend.SetPropertyBlock(_materialPropertyBlock);

                await Task.Yield();
            }
            else
            {
                await Task.Yield();
            }
        }
    }

    public override void StopAction(Transform transform)
    {
        shouldPerformAction = false;

        CancelTask();
    }
    public override void ChangeActivationState()
    {
        hasBeenActivated = !hasBeenActivated;
    }

    public override void CancelTask()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }
}
