using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSizeAction", menuName = "Actions/ Change Size")]
public class ChangeSizeAction : PerformAction
{
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _minScale;

    private float _accumulatedTime = 0f;

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
    public async override void DoAction(Transform transform)
    {
        base.DoAction(transform);

        shouldPerformAction = true;
        _accumulatedTime = 0f;

        cancellationTokenSource = new CancellationTokenSource();
        await ScaleAsync(transform, cancellationTokenSource.Token);
    }
    public override void StopAction(Transform transform)
    {
        base.StopAction(transform);

        shouldPerformAction = false;

        _accumulatedTime = 0f;

        CancelTask();
    }
    private async Task ScaleAsync(Transform transform, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (shouldPerformAction)
            {
                _accumulatedTime += Time.deltaTime;

                float scaleFactor = Mathf.Sin(_accumulatedTime * _scaleSpeed);

                float newScale = Mathf.Lerp(_minScale, _maxScale, (scaleFactor + 1f) / 2f);

                transform.localScale = new Vector3(newScale, newScale, newScale);

                await Task.Yield();
            }
            else
            {
                await Task.Yield();
            }
        }
    }
}
