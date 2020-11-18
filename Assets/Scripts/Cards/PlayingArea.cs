using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Player;

namespace Cards
{
    [RequireComponent(typeof(CardObjectsGenerator))]
    public class PlayingArea : MonoBehaviour //контроль поведения открытых игроком карт
    {
        #region Serialize Fields

        [Range(0, 5)]
        [SerializeField] private float pauseDuration = 3.0f;

        #endregion

        #region Properties

        List<Card> cardsAtPlayingArea = null;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            Card[] generatedCards = GetComponent<CardObjectsGenerator>().Generate();

            cardsAtPlayingArea = new List<Card>();
            cardsAtPlayingArea.AddRange(generatedCards);

            ShowAllCards();
        }

        private void OnEnable()
        {
            MovesChain.OnCompleted += MovesChainCompletedEventHandler;
            MovesChain.OnIncomplited += MovesChainIncompletedEventHandler;
        }

        private void OnDisable()
        {
            MovesChain.OnCompleted -= MovesChainCompletedEventHandler;
            MovesChain.OnIncomplited -= MovesChainIncompletedEventHandler;
        }

        #endregion

        #region Public Methods

        public void ShowAllCards()
        {
            IEnumerator RotateAll()
            {
                for (int i = 0; i < cardsAtPlayingArea.Count; i++)
                {
                    cardsAtPlayingArea[i].Show(silently: true);
                }

                yield return new WaitForSeconds(pauseDuration);

                for (int i = 0; i < cardsAtPlayingArea.Count; i++)
                {
                    cardsAtPlayingArea[i].Hide();
                }

                yield return null;
            }

            CommonCoroutine routine = new CommonCoroutine(this, RotateAll);
            routine.Start();
        }

        #endregion

        #region Event Handlers

        private void MovesChainCompletedEventHandler(Card[] cards)
        {
            Log.Message("Обработка события успешного завершения цепочки ходов");

            for (int i = 0; i < cards.Length; i++)
            {
                cardsAtPlayingArea.Remove(cards[i]);

                Destroy(cards[i].gameObject); //Удаление карты с игрового поля
            }

            if (cardsAtPlayingArea.Count == 0)
            {
                Log.Message("Карт на игровом поле больше нет");

                GameOverStatements.EnterVictoryState();
            }
        }

        private void MovesChainIncompletedEventHandler(Card[] cards)
        {
            Log.Message("Обработка события неуспешного завершения цепочки ходов");

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Hide(); //переворачивание карты рубашкой вверх
            }
        }

        #endregion
    }
}