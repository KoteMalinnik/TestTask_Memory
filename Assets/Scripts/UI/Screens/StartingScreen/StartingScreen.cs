using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UI.CustomScreen.Core;

namespace UI.CustomScreen.StartingScreen
{
    public class StartingScreen : BaseScreen
    {
        #region Serialize Fields

        [SerializeField] private int playingSceneID = 0;
        [SerializeField] private Button play = null;
        [Space]
        [SerializeField] private string bestScoreLabel = "Лучший счет: ";
        [SerializeField] private Text bestScore = null;

        #endregion

        #region MonoBehaviour Callbacks

        private void OnEnable()
        {
            bestScore.text = bestScoreLabel + Serialization.Load(Player.Score.BestScoreKey, 0).ToString();

            play.onClick.AddListener(() => SceneManager.LoadSceneAsync(playingSceneID));
        }

        private void OnDisable()
        {
            play.onClick.RemoveAllListeners();
        }

        #endregion
    }
}