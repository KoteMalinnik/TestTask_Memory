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
        FlipOver(openState: false);
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

    public void SetMaterial(Material material)
    {
        if (material == null)
        {
            Log.Error("Материал не определена");
            return;
        }

        Log.Message($"Установка материала карты {name}. Текстура: {material.mainTexture.name}");

        //imageRenderer.sharedMaterial.mainTexture = texture;
        imageRenderer.material = material;
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
        float targetAngle = openState == true ? 270 : 90;
        transform.localRotation = Quaternion.Euler(targetAngle, 0, 0);

        IsOpen = openState;
    }

    #endregion
}