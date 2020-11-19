using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UI.CustomScreen.Core;
using Player;

namespace UI.CustomScreen.GameOverScreen
{
	public class GameOverScreen : BaseScreen
	{
        #region Serialize Fields

        [SerializeField] private GameObject panel = null; //панель с нижеописанными элементами
        [Space]
		[SerializeField] private string lossStatementLabel = "ВЫ ПРОИГРАЛИ!";
		[SerializeField] private string victoryStatementLabel = "ВЫ ПОБЕДИЛИ!";
		[SerializeField] private Text statementLabel = null;
		[Space]
		[SerializeField] private Text newRecordLabel = null;
        [Space]
        [SerializeField] private string bestResultLabel = "Лучший результат: ";
        [SerializeField] private Text bestResult = null;
        [Space]
        [SerializeField] private string currentResultLabel = "Ваш результат: ";
        [SerializeField] private Text currentResult = null;
        [Space]
		[SerializeField] private Button replay = null;
        [Space]
        [SerializeField] private int startingSceneID = 0;
		[SerializeField] private Button exit = null;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            newRecordLabel.gameObject.SetActive(false);
            panel.SetActive(false);
        }

        private void OnEnable()
        {
            GameOverStatements.OnGameOver += GameOverStatementEventHandler;
            GameOverStatements.OnLoss += LossStatementEventHandler;
            GameOverStatements.OnVictory += VictoryStatementEventHandler;

            Score.OnNewBest += ShowNewRecordLabel;
            Score.OnNewBest += UpdateBestResult;

            int currentSceneID = SceneManager.GetActiveScene().buildIndex;
            replay.onClick.AddListener(() => SceneManager.LoadSceneAsync(currentSceneID));
            exit.onClick.AddListener(() => SceneManager.LoadSceneAsync(startingSceneID));
        }

        private void OnDisable()
        {
            GameOverStatements.OnGameOver -= GameOverStatementEventHandler;
            GameOverStatements.OnLoss -= LossStatementEventHandler;
            GameOverStatements.OnVictory -= VictoryStatementEventHandler;

            Score.OnNewBest -= ShowNewRecordLabel;
            Score.OnNewBest -= UpdateBestResult;

            replay.onClick.RemoveAllListeners();
            exit.onClick.RemoveAllListeners();
        }

        #endregion

        #region Private Methods

        private void ShowNewRecordLabel(int e) => newRecordLabel.gameObject.SetActive(true);
        private void UpdateBestResult(int bestScore) => bestResult.text = bestResultLabel + bestScore.ToString();
        private void UpdateCurrentResult(int currentScore) => currentResult.text = currentResultLabel + currentScore.ToString();

        #endregion

        #region Event Handlers

        private void GameOverStatementEventHandler()
        {
            Log.Message("Обработка события конца игры");

            panel.SetActive(true);

            UpdateBestResult(Score.Best);
            UpdateCurrentResult(Score.Value);
        }
            
        private void VictoryStatementEventHandler() => statementLabel.text = victoryStatementLabel;
        private void LossStatementEventHandler() => statementLabel.text = lossStatementLabel;

        #endregion
    }
}