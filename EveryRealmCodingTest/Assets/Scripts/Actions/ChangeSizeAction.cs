using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ChangeSizeAction : PerformAction
{
    private float _scaleSpeed;
    private float _maxScale;
    private float _minScale;

    private float _accumulatedTime = 0f;

    private CancellationTokenSource _cancellationTokenSource;

    public ChangeSizeAction(float scaleSpeed, float maxScale, float minScale)
    {
        this._scaleSpeed = scaleSpeed;
        this._maxScale = maxScale;
        this._minScale = minScale;
    }

    public async override void DoAction(Transform transform)
    {
        CancelScalingTask();

        shouldPerformAction = true;
        _accumulatedTime = 0f;

        _cancellationTokenSource = new CancellationTokenSource();
        await ScaleAsync(transform, _cancellationTokenSource.Token);
    }

    private async Task ScaleAsync(Transform transform, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if(shouldPerformAction)
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

    public override void StopAction(Transform transform)
    {
        shouldPerformAction = false;

        _accumulatedTime = 0f;

        CancelScalingTask();
    }

    private void CancelScalingTask()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    public override void ChangeActivationState()
    {
        hasBeenActivated = !hasBeenActivated;
    }
}
