using UnityEngine;
using System.Collections;
using System;

public class FlipoverCoroutine
{
    #region Properties
    private float Speed { get; } = 1;
    private CommonCoroutine RotateRoutine { get; set; } = null;

    #endregion

    public FlipoverCoroutine(MonoBehaviour owner, float speed, Action onFinish)
    {
        Speed = speed;

        RotateRoutine = new CommonCoroutine(owner, Rotation);
        RotateRoutine.OnFinish += onFinish;
    }

	public void Play()
    {
        RotateRoutine.Start();
    }

	private IEnumerator Rotation()
    {
        Log.Message($"Начало вращения объекта {RotateRoutine.Owner.name}");

        Transform transform = RotateRoutine.Owner.transform;

        Quaternion statringRotation = transform.localRotation;
        Quaternion targetRotation = statringRotation * Quaternion.Euler(180, 0, 0);

        for (float T = 0; T < 1; T += Time.deltaTime * Speed)
        {
            transform.localRotation = Quaternion.Lerp(statringRotation, targetRotation, T);

            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = targetRotation;

        Log.Message($"Конец вращения объекта {RotateRoutine.Owner.name}");
    }
}
