using UnityEngine;
using System.Collections.Generic;
using Player;

namespace Cards
{
    [RequireComponent(typeof(CardObjectsGenerator))]
    public class PlayingArea : MonoBehaviour //контроль поведения открытых игроком карт
    {
        #region Properties

        List<Card> cardsAtPlayingArea = null;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            Card[] generatedCards = GetComponent<CardObjectsGenerator>().Generate();

            cardsAtPlayingArea = new List<Card>();
            cardsAtPlayingArea.AddRange(generatedCards);
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