using UnityEngine;
using System.Collections.Generic;
using Player;

namespace Cards
{
    [RequireComponent(typeof(CardObjectsGenerator))]
    public class PlayingArea : MonoBehaviour //контроль поведения открытых игроком карт
    {
        #region Serialize Fields

        [Range(0, 5)]
        [SerializeField] private float startPauseDuration = 3.0f;
        [Range(0, 5)]
        [SerializeField] private float delayBeforeHidingCards = 1.0f;

        #endregion

        #region Properties

        private List<Card> CardsAtPlayingArea { get; } = new List<Card>();

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            Card[] generatedCards = GetComponent<CardObjectsGenerator>().Generate();
            CardsAtPlayingArea.AddRange(generatedCards);
        }

        private void OnEnable()
        {
            MovesChain.OnCompleted += MovesChainCompletedEventHandler;
            MovesChain.OnIncomplited += MovesChainIncompletedEventHandler;

            GameOverStatements.OnLoss += GameOverLossStatementEventHandler;
        }

        private void OnDisable()
        {
            MovesChain.OnCompleted -= MovesChainCompletedEventHandler;
            MovesChain.OnIncomplited -= MovesChainIncompletedEventHandler;
        }

        #endregion

        #region Public Methods

        public void ShowAllCardsDelayed()
        {
            DelayCoroutine delayCoroutine = new DelayCoroutine(this, HideAll, startPauseDuration);

            ShowAll();
            delayCoroutine.Start();
        }

        #endregion

        #region Private Methods

        private void ShowAll()
        {
            for (int i = 0; i < CardsAtPlayingArea.Count; i++)
            {
                CardsAtPlayingArea[i].Show(publishEvent: false);
            }
        }

        private void HideAll()
        {
            for (int i = 0; i < CardsAtPlayingArea.Count; i++)
            {
                CardsAtPlayingArea[i].Hide();
            }
        }

        #endregion

        #region Event Handlers

        private void GameOverLossStatementEventHandler() //отключаем игровое поле, чтобы игрок с ним не взаимодействовал
        {
            GameOverStatements.OnLoss -= GameOverLossStatementEventHandler;

            gameObject.SetActive(false);
        }

        private void MovesChainCompletedEventHandler(Card[] cards)
        {
            Log.Message("Обработка события успешного завершения цепочки ходов");

            Card.Block();

            DelayCoroutine delayRoutine = new DelayCoroutine(this, () =>
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    CardsAtPlayingArea.Remove(cards[i]);

                    Destroy(cards[i].gameObject); //Удаление карты с игрового поля
                }

                Card.Unblock();

                if (CardsAtPlayingArea.Count == 0)
                {
                    Log.Message("Карт на игровом поле больше нет");

                    GameOverStatements.EnterVictoryState();
                }
            }, delayBeforeHidingCards);

            delayRoutine.Start();
        }

        private void MovesChainIncompletedEventHandler(Card[] cards)
        {
            Log.Message("Обработка события неуспешного завершения цепочки ходов");

            Card.Block();

            DelayCoroutine delayRoutine = new DelayCoroutine(this, () =>
                {
                    for (int i = 0; i < cards.Length; i++)
                    {
                        cards[i].Hide(); //переворачивание карты рубашкой вверх
                    }

                    Card.Unblock();

                    if (Lifes.Value == 0)
                    {
                        Log.Message("Жизни закончились");

                        GameOverStatements.EnterLossState();
                    }
                }, delayBeforeHidingCards);

            delayRoutine.Start();
        }

        #endregion
    }
}