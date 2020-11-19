using UnityEngine;
using Cards;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Множители очков")]
        [SerializeField] private int opened2cardsMultiplier = 1;
        [SerializeField] private int opened3cardsMultiplier = 3;
        [Space]
        [SerializeField] private int lifesCount = 5;

        #endregion

        #region Properties

        private static Lifes Lifes { get; set; } = null;
        private static Score Score { get; set; } = null;
        private static MovesChain MovesChain { get; set; } = null;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            Lifes = new Lifes(lifesCount);
            Score = new Score();
        }

        private void OnEnable()
        {
            Card.OnShowed += CardShowedEventHandler;

            MovesChain.OnCompleted += MovesChainComplitedEventHandler;
            MovesChain.OnIncomplited += MovesChainIncomplitedEventHanlder;
            MovesChain.OnMatch += MovesChainMatchEventHandler;

            GameOverStatements.OnGameOver += GameOverStatementEventHandler;
        }

        private void OnDisable()
        {
            Card.OnShowed -= CardShowedEventHandler;

            MovesChain.OnCompleted -= MovesChainComplitedEventHandler;
            MovesChain.OnIncomplited -= MovesChainIncomplitedEventHanlder;
            MovesChain.OnMatch -= MovesChainMatchEventHandler;

            GameOverStatements.OnGameOver -= GameOverStatementEventHandler;
        }

        private void OnApplicationQuit()
        {
            UpdateBestScore();
        }

        #endregion

        #region Private Methods

        private void UpdateBestScore()
        {
            //Проверка на новый лучший счет и последующее его сохранение
            if (Score?.CheckNewBestScore() ?? false) //если Score не определен, то условие не выполняется
            {
                Score.UpdateBestScore(); //обновление лучшего счета
                Score.SaveBestScore(); //сохранение лучшего счета
            }
        }

        #endregion

        #region Event Handlers

        private void GameOverStatementEventHandler()
        {
            Log.Message("Обработка события конца игры");

            UpdateBestScore();
        }

        private void CardShowedEventHandler(Card card)
        {
            Log.Message("Обработка события открытия новой карты");

            if (card == null)
            {
                Log.Error("Карта не определена");

                return;
            }

            if (MovesChain == null)
            {
                MovesChain = new MovesChain(card);

                return;
            }

            MovesChain.Append(card);
        }

        private void MovesChainMatchEventHandler()
        {
            Log.Message("Обработка события совпадения карт");

            Score.Increase(opened2cardsMultiplier * Lifes.Value); //увеличение счета игрока за 2 открытые карты
        }

        private void MovesChainComplitedEventHandler(Card[] e)
        {
            Log.Message("Обработка события успешного завершения цепочки ходов");

            Score.Increase(opened3cardsMultiplier * Lifes.Value); //увеличение счета игрока за 3 открытые карты

            MovesChain = null;
        }

        private void MovesChainIncomplitedEventHanlder(Card[] e)
        {
            Log.Message("Обработка события неуспешного завершения цепочки ходов");

            Lifes.Decrease();

            MovesChain = null;
        }

        #endregion
    }
}