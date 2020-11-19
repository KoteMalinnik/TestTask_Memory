using System;

public static class GameOverStatements //состояния конца игры (пародия на машину состояний)
{
    #region Events

    public static event Action OnVictory = null;
    public static event Action OnLoss = null;
    public static event Action OnGameOver = null;

    #endregion

    #region Public Methods

    public static void EnterVictoryState()
    {
        Log.Message("Вход в состояние победы игрока");

        OnVictory?.Invoke();
        OnGameOver?.Invoke();
    }

    public static void EnteLossState()
    {
        Log.Message("Вход в состояние проигрыша игрока");

        OnLoss?.Invoke();
        OnGameOver?.Invoke();
    }

    #endregion
}
