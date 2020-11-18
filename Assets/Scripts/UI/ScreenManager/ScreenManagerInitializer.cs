using UnityEngine;
using System.Collections.Generic;

namespace UI.CustomScreen.Core
{
    /// <summary>
    /// Инициализатор ScreenManager.
    /// Передает в ScreenManager необходимые параметры.
    /// </summary>
    public class ScreenManagerInitializer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<BaseScreen> screenPrefabs = null;
        [SerializeField] private Transform screenTransform = null;
        [Space]
        [SerializeField] private List<ScreenType> openOnAwake = new List<ScreenType>();

        #endregion

        private void Awake()
        {
            if (screenPrefabs == null || screenTransform == null)
            {
                Log.Error("Список префабов или трансформ окон пуст.");
                return;
            }

            ScreenManager.Initialize(screenPrefabs, screenTransform);

            for (int i = 0; i < openOnAwake.Count; i++)
            {
                ScreenManager.Open(openOnAwake[i], false);
            }

            Destroy(this);
        }
    }
}