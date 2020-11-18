using UnityEngine;
using System.Collections;
using System;

public class CardFlipAnimation
{
    #region Events

    private event Action OnFinish = null;

    #endregion

    #region Properties

    private MonoBehaviour Owner { get; } = null;
    private float Speed { get; } = 1;
    private CommonCoroutine rotateRoutine { get; set; } = null;

    #endregion

    public CardFlipAnimation(MonoBehaviour owner, float speed, Action onFinish)
    {
        Owner = owner;
        Speed = speed;
        OnFinish = onFinish;
    }

	public void Play()
    {
		if (rotateRoutine?.isRunning ?? false)
        {
			Log.Warning("Корутина вращения еще проигрывается");
			Log.Message("Остановка корутины вращения");

			rotateRoutine.Stop();
        }

		Log.Message("Запуск корутины вращения");

		rotateRoutine = new CommonCoroutine(Owner, Rotation);
		rotateRoutine.OnFinish += OnFinish;

        rotateRoutine.Start();
    }

	private IEnumerator Rotation()
    {
        Transform transform = Owner.transform;

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
