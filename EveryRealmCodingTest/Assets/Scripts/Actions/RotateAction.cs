using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "RotateAction", menuName = "Actions/ Rotate")]
public class RotateAction : PerformAction
{
    [SerializeField] private float rotationSpeed;
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

        cancellationTokenSource = new CancellationTokenSource();
        await RotateAsync(transform, cancellationTokenSource.Token);
    }

    public override void StopAction(Transform transform)
    {
        base.StopAction(transform);

        shouldPerformAction = false;

        CancelTask();
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
}
