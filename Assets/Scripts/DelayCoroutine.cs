using UnityEngine;
using System.Collections;
using System;

public class DelayCoroutine
{
    #region Properties

    private CommonCoroutine DelayRoutine { get; } = null;
    private float DelayInSeconds { get; } = 1;
    
    #endregion

    public DelayCoroutine(MonoBehaviour owner, Action onFinish, float delayInSeconds)
    {
        DelayInSeconds = delayInSeconds;

        DelayRoutine = new CommonCoroutine(owner, Delay);
        DelayRoutine.OnFinish += onFinish;
    }

    public void Start()
    {
        DelayRoutine.Start();
    }

    private IEnumerator Delay()
    {
        Log.Message($"Начало задержки ({DelayInSeconds} сек)");

        yield return new WaitForSeconds(DelayInSeconds);

        Log.Message("Конец задержки");
    }
}