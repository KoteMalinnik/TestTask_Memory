using System;
using UnityEngine;

namespace UI.CustomScreen.Core
{
    /// <summary>
    /// Базовый тип для всех окон, которые могут открываться с помощью ScreenManager.
    /// Необходимо унаследоваться от данного класса, чтобы создать окно с уникальной логикой.
    /// </summary>
    public class BaseScreen : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ScreenType screenType;

        #endregion

        #region Properties

        /// <summary>
        /// Тип окна.
        /// Назначение этого поля напрямую зависит от успешного открывания окна с помощью ScreenManager.
        /// </summary>
        public ScreenType ScreenType => screenType;

        #endregion

        /// <summary>
        /// Открыть окно.
        /// </summary>
        /// <param name="onOpen">Событие, которое должно произойти по открытии окна.</param>
        public void Open(Action onOpen = null)
        {
            Log.Message("Показ окна " + ScreenType);
            gameObject.SetActive(true);
            onOpen?.Invoke();
        }

        /// <summary>
        /// Закрыть окно.
        /// </summary>
        /// <param name="onHide">Событие, которое должно произойти по закрытии окна.</param>
        public void Close(Action onHide = null)
        {
            Log.Message("Закрытие окна " + ScreenType);
            Destroy(gameObject);
            onHide?.Invoke();
        }
    }
}