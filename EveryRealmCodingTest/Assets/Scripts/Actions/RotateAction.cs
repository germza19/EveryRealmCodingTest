using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RotateAction : PerformAction
{
    public float rotationSpeed = 20f;

    private CancellationTokenSource _cancellationTokenSource;

    public RotateAction(float rotationSpeed)
    {
        this.rotationSpeed = rotationSpeed;
    }

    public async override void DoAction(Transform transform)
    {
        shouldPerformAction = true;

        _cancellationTokenSource = new CancellationTokenSource();
        await RotateAsync(transform, _cancellationTokenSource.Token);
    }
    private async Task RotateAsync(Transform transform, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (shouldPerformAction)
            {
                float rotationX = rotationSpeed * Time.deltaTime;
                float rotationY = rotationSpeed * Time.deltaTime;
                float rotationZ = rotationSpeed * Time.deltaTime;

                Quaternion rotationQuaternionX = Quaternion.Euler(rotationX, 0, 0);
                Quaternion rotationQuaternionY = Quaternion.Euler(0, rotationY, 0);
                Quaternion rotationQuaternionZ = Quaternion.Euler(0, 0, rotationZ);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, transform.rotation * rotationQuaternionX * rotationQuaternionY * rotationQuaternionZ,
                    rotationSpeed * Time.deltaTime);

                await Task.Yield();
            }
            else
            {
                await Task.Delay(16);
            }
        }
    }

    public override void StopAction(Transform transform)
    {
        shouldPerformAction = false;


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
}
