using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeColorAction", menuName = "Actions/ Change Color")]
public class ChangeColorAction : PerformAction
{
    private MaterialPropertyBlock _materialPropertyBlock;
    private Renderer _rend;

    public async override void DoAction(Transform transform)
    {
        base.DoAction(transform);

        shouldPerformAction = true;
        cancellationTokenSource = new CancellationTokenSource();
        await ChangeColorAsync(transform, cancellationTokenSource.Token);
    }

    public override void StopAction(Transform transform)
    {
        base.StopAction(transform);

        shouldPerformAction = false;

        CancelTask();
    }

    public override void CancelTask()
    {
        base.CancelTask();

        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
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
}
