using System.Collections.Generic;
using System;
using Cards;

namespace Player
{
    public class MovesChain
    {
        #region Events

        public static event Action<Card[]> OnCompleted = null; //цепочка успешна завершена
        public static event Action<Card[]> OnIncomplited = null; //цепочка не завершилась (несовпадение)
        
        public static event Action OnMatch = null; //успешное добавление карты в цепочку

        #endregion

        #region Properties

        private static int MaxCardsCount { get; set; } = 3; //максимальное количество ходов

        private List<Card> opendedCards { get; } = null; //открытые карты, прошедшие проверку на совпадение
        private string DeckName { get; } = null; //имя колоды, карты которой проходят проверку

        #endregion

        #region Constructors

        public MovesChain(Card initialCard)
        {
            Log.Message($"Создание цепочки ходов для колоды {initialCard.DeckName}");

            DeckName = initialCard.DeckName; //первая карта задает целевое имя колоды

            opendedCards = new List<Card>();
            Add(initialCard); //добавляем карту в список без проверок
        }

        #endregion

        #region Public Methods

        #region Static

        public static void SetMaxCardsCount(int maxCardsCount)
        {
            if (maxCardsCount < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            Log.Message($"Установка максимального количества карт в цепочке: {maxCardsCount}");

            MaxCardsCount = maxCardsCount;
        }

        #endregion

        public void Append(Card card)
        {
            Log.Message($"Попытка добавить карту {card.name} в цепочку ходов");

            if (card.DeckName.Equals(DeckName))
            {
                Log.Message("Карты совпадают");

                Add(card);

                if (opendedCards.Count == MaxCardsCount)
                {
                    Log.Message("Цепочка ходов завершена");

                    OnCompleted?.Invoke(opendedCards.ToArray());
                }
                else
                {
                    OnMatch?.Invoke();
                }
            }
            else
            {
                Log.Message("Карты не совпадают");

                opendedCards.Add(card); //необходимо добавить несовпавшую карту в список, чтобы при обработке события она закрылась
                OnIncomplited?.Invoke(opendedCards.ToArray());
            }
        }

        #endregion

        #region Private Methods

        private void Add(Card card)
        {
            Log.Message($"Добавление карты {card.name} в цепочку ходов");

            opendedCards.Add(card);

            Log.Message($"Ход {opendedCards.Count} из {MaxCardsCount}");
        }

        #endregion
    }
}