using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RotateAction : PerformAction
{
    public float rotationSpeed;

    public RotateAction(float rotationSpeed)
    {
        this.rotationSpeed = rotationSpeed;
    }

    public async override void DoAction(Transform transform)
    {
        shouldPerformAction = true;

        cancellationTokenSource = new CancellationTokenSource();
        await RotateAsync(transform, cancellationTokenSource.Token);
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
