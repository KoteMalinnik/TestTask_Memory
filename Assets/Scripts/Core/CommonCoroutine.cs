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

    #region Properties

    public MonoBehaviour Owner { get; } = null;
    private Coroutine Coroutine { get; set; } = null;
    private Func<IEnumerator> Routine { get; } = null;

    /// <summary>
    /// Вернет true, если корутина в данный момент выполняется.
    /// </summary>
    public bool IsRunning => Coroutine != null;
	
    #endregion

	#region Constructors
	
    public CommonCoroutine(MonoBehaviour owner, Func<IEnumerator> routine)
    {
        if (owner == null || routine == null) throw new ArgumentNullException();

        this.Owner = owner;
        this.Routine = routine;
    }

	#endregion
	
	#region Public Methods
	
    /// <summary>
    /// Запустить выполнение корутины.
    /// </summary>
    public void Start()
    {
        Stop();
        Coroutine = Owner.StartCoroutine(Process());
    }

    /// <summary>
    /// Остановить выполнение корутины.
    /// </summary>
    public void Stop()
    {
        if (IsRunning)
        {
            Owner.StopCoroutine(Coroutine);
            Coroutine = null;
        }
    }
	
	#endregion
	
	#region Private Methods

    private IEnumerator Process()
    {
        yield return Routine.Invoke();
        Coroutine = null;

        OnFinish?.Invoke();
    }
	
	#endregion
}
