using UnityEngine;

public class FrameRateController: MonoBehaviour
{
    #region Serialize Fields

    [Range(30, 90)]
    [SerializeField] private int frameRate = 60;

    #endregion

    #region Static Properties

    private static bool IsInitialized { get; set; } = false;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        if (IsInitialized)
        {
            SelfDestroy();

            return;
        }

        Application.targetFrameRate = frameRate;
        IsInitialized = true;

        SelfDestroy();
    }

    #endregion

    private void SelfDestroy() => Destroy(gameObject);
}