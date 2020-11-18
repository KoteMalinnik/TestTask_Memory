using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreController: MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private Text scoreLabel = null;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        scoreLabel.text = 0.ToString();

        Player.Score.OnChanged += ScoreChangedEventHandler;
    }

    private void OnDisable()
    {
        Player.Score.OnChanged -= ScoreChangedEventHandler;
    }

    #endregion

    #region Event Handlers

    private void ScoreChangedEventHandler(int score)
    {
        scoreLabel.text = score.ToString();
    }

    #endregion
}