using System;

namespace Player
{
    public class Lifes
    {
        #region Events

        public static event Action<int> OnInitialized = null;
        public static event Action<int> OnChanged = null; //количество жизней изменилось

        #endregion

        #region Properties

        public int Value { get; private set; } = 0;

        #endregion

        #region Constructors

        public Lifes(int initialLifesCount)
        {
            if (initialLifesCount <= 0)
            {
                Log.Warning($"Передано неверное количество жизней ({initialLifesCount})");

                initialLifesCount = 5; //по умолчанию будем выставлять 5 жизней, если неправильно подали аргумент
            }

            Log.Message($"Установка количества жизней: {initialLifesCount}");

            Value = initialLifesCount;

            OnInitialized?.Invoke(Value);
        }

        #endregion

        #region Public Methods

        public void Decrease()
        {
            if (Value == 0) //не надо вызвать какие-либо события, если жизни закончились
            {
                return;
            }

            Log.Message($"Уменшьение количества жизней ({Value}) -> ({Value - 1})");

            Value--;
            OnChanged?.Invoke(Value);

            if (Value == 0)
            {
                Log.Message("Жизни закончились");

                GameOverStatements.EnteLossState();
            }
        }

        #endregion
    }
}