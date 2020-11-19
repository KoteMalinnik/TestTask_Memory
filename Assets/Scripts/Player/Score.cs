using System;

namespace Player
{
    public class Score
    {
        #region Events

        public static event Action<int> OnNewBest = null; //новый лучший счет
        public static event Action<int> OnChanged = null; //количество очков изменилось

        #endregion

        #region Properties

        public static int Value { get; private set; } = 0;
        public static int Best { get; private set; } = 0;
        
        public static string BestScoreKey => "BestScore";

        #endregion

        #region Constructors

        public Score()
        {
            LoadBestScore();
        }

        #endregion

        #region Public Methods

        public void Increase(int delta)
        {
            if (delta < 0)
            {
                Log.Warning($"Передано отрицательное изменение количества очков ({delta})");

                return;
            }

            Log.Message($"Увеличение количества очков ({Value}) -> ({Value + delta})");

            Value += delta;

            OnChanged?.Invoke(Value);
        }

        public void UpdateBestScore()
        {
            Log.Message($"Достигнут новый лучший счет! ({Best}) -> ({Value})");

            Best = Value;
            OnNewBest?.Invoke(Best);
        }

        public bool CheckNewBestScore()
        {
            Log.Message($"Проверка на новый лучший счет. Результат проверки (Value > Best): {Value > Best}");

            return Value > Best;
        }
            
        public void SaveBestScore() => Serialization.Save(BestScoreKey, Best);
        public static int LoadBestScore()
        {
            Best = Serialization.Load(BestScoreKey, 0);
            return Best;
        }

        #endregion
    }
}