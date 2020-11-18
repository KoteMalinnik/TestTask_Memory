using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomScreen.InfoScreen
{
    [RequireComponent(typeof(Image))]
    public class UiLife : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private Image filler = null;

        #endregion

        #region Public Methods

        public void Deactivate()
        {
            Log.Message($"Отключение объекта {filler.name}");

            filler.gameObject.SetActive(false);
        }

        #endregion
    }
}