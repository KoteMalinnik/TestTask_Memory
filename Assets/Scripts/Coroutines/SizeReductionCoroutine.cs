using UnityEngine;
using System.Collections;
using System;
using Cards;

public class SizeReductionCoroutine
{
    #region Properties

    private CommonCoroutine ReductionRoutine { get; } = null;
    private Transform Transform { get; } = null;
    private float Speed { get; } = 0;

    #endregion

    #region Constructors

    public SizeReductionCoroutine(MonoBehaviour owner, Card card, float speed, Action onFinish)
    {
        Transform = card.transform;
        Speed = speed;

        ReductionRoutine = new CommonCoroutine(owner, Reduction);
        ReductionRoutine.OnFinish += onFinish;
    }

    #endregion

    public void Play()
    {
        Log.Message($"Запуск корутины уменьшения объекта {Transform.name}");

        ReductionRoutine.Start();
    }

    private IEnumerator Reduction()
    {
        Log.Message("Начало уменьшения");

        float scale = Transform.localScale.x;

        while (scale > 0.1f)
        {
            scale -= Time.deltaTime * Speed;

            Transform.localScale = new Vector3(scale, scale, scale);

            yield return new WaitForEndOfFrame();
        }

        Log.Message("Завершение уменьшения");
    }
}