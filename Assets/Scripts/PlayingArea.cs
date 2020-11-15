using UnityEngine;
using System;
using Player;

namespace Cards
{
    public class PlayingArea : MonoBehaviour //контроль поведения открытых игроком карт
    {
        #region MonoBehaviour Callbacks

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
                Destroy(cards[i].gameObject); //Удаление карты с игрового поля
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