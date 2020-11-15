using UnityEngine;

[CreateAssetMenu(fileName = "NewCardImagesSet", menuName = "Card Images Set")]
public class CardImagesSet : ScriptableObject
{
    #region Serialize Fields

    [SerializeField] private Texture[] imageTextures = new Texture[5];

    #endregion

    #region Properties

    public int Length => imageTextures?.Length ?? 0;

    #endregion

    #region Public Methods

    public Texture GetImage(int index)
    {
        try
        {
            return imageTextures[index];
        }
        catch
        {
            return null;
        }
    }

    #endregion
}