using UnityEngine;
using System.Collections;
using System;

public class CardFlipAnimation
{
    #region Properties
    private float Speed { get; } = 1;
    private CommonCoroutine rotateRoutine { get; set; } = null;

    #endregion

    public CardFlipAnimation(MonoBehaviour owner, float speed, Action onFinish)
    {
        Speed = speed;

        rotateRoutine = new CommonCoroutine(owner, Rotation);
        rotateRoutine.OnFinish += onFinish;
    }

	public void Play()
    {
		Log.Message("Запуск корутины вращения");

        rotateRoutine.Start();
    }

	private IEnumerator Rotation()
    {
        Transform transform = rotateRoutine.Owner.transform;

        Quaternion statringRotation = transform.localRotation;
        Quaternion targetRotation = statringRotation * Quaternion.Euler(180, 0, 0);

        for (float T = 0; T < 1; T += Time.deltaTime * Speed)
        {
            transform.localRotation = Quaternion.Lerp(statringRotation, targetRotation, T);

            yield return new WaitForEndOfFrame();
        }

        transform.localRotation = targetRotation;

        yield return null;
    }
}
