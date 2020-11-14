using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Класс корутины без параметров, который содержит:
/// -информацию о том, работает ли корутина в данный момент;
/// -действие по завершению, на которое надо подписываться;
/// -методы для запуска и остановки корутины.
/// </summary>
public sealed class CommonCoroutine
{
    #region Events
	
    /// <summary>
    /// Действие, которое необходимо выполнить по завершению корутины.
    /// </summary>
    public event Action OnFinish = null;
	
    #endregion

    #region Fields
	
    private Coroutine coroutine = null;
	
    #endregion

    #region Properties
	
    private MonoBehaviour owner { get; } = null;
    private Func<IEnumerator> routine { get; } = null;

    /// <summary>
    /// Вернет true, если корутина в данный момент выполняется.
    /// </summary>
    public bool isRunning => coroutine != null;
	
    #endregion

	#region Constructors
	
    public CommonCoroutine(MonoBehaviour owner, Func<IEnumerator> routine)
    {
        if (owner == null || routine == null) throw new ArgumentNullException();

        this.owner = owner;
        this.routine = routine;
    }

	#endregion
	
	#region Public Methods
	
    /// <summary>
    /// Запустить выполнение корутины.
    /// </summary>
    public void Start()
    {
        Stop();
        coroutine = owner.StartCoroutine(Process());
    }

    /// <summary>
    /// Остановить выполнение корутины.
    /// </summary>
    public void Stop()
    {
        if (isRunning)
        {
            owner.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
	
	#endregion
	
	#region Private Methods

    private IEnumerator Process()
    {
        yield return routine.Invoke();
        coroutine = null;

        OnFinish?.Invoke();
    }
	
	#endregion
}
