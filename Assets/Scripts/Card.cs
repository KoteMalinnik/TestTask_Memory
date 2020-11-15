using UnityEngine;
using System;

public class Card : MonoBehaviour
{
    #region Evenets

    public static event Action<Card> OnShowed = null;
    public static event Action<Card> OnHided = null;

    #endregion

    #region Serialized Fields

    [SerializeField] private MeshRenderer imageRenderer = null;

    #endregion

    #region Properties

    private bool IsOpen { get; set; } = false;

    public string ImageName => imageRenderer?.sharedMaterial?.mainTexture?.name ?? "NotDefined";

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        //установка начального вращения на случай, если префаб забудут повернуть на 90 градусов по оси X
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    private void OnMouseDown()
    {
        Log.Message($"Нажатие на карту {name}");

        if (IsOpen == false) //открытие карты игроком должно происходить только один раз
        {
            Show();
        }
    }

    #endregion

    #region Public Methods

    public void SetImage(Texture texture)
    {
        if (texture == null)
        {
            Log.Error("Текстура не определена");
            return;
        }

        Log.Message($"Установка текстуры карты {name}. Текстура: {texture.name}");

        imageRenderer.sharedMaterial.mainTexture = texture;
    }

    public void Show()
    {
        Log.Message($"Открытие карты {name}");

        FlipOver(true);

        OnShowed?.Invoke(this);
    }

    public void Hide()
    {
        Log.Message($"Закрытие карты {name}");

        FlipOver(false);

        OnHided?.Invoke(this);
    }

    #endregion

    #region Private Methods

    private void FlipOver(bool openState)
    {
        Log.Message($"Переворот карты {this.name}. Открытое состояние: {openState}");

        transform.localRotation *= Quaternion.Euler(180, 0, 0);

        IsOpen = openState;
    }

    #endregion
}