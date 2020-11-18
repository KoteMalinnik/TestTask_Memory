using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UI.CustomScreen.Core;

namespace UI.CustomScreen.InfoScreen
{
	public class InfoScreen : BaseScreen
	{
        #region Serialize Fields

        [SerializeField] private int startSceneID = 0;
		[SerializeField] private Button back = null;

        #endregion

        #region MonoBehaviour Callbacks

        private void OnEnable()
        {
            back.onClick.AddListener(() => SceneManager.LoadSceneAsync(startSceneID));
        }

        private void OnDisable()
        {
            back.onClick.RemoveAllListeners();
        }

        #endregion
    }
}