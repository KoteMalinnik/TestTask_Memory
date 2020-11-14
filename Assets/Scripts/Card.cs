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

    //TODO: публичный элемент для сравнения изображений карт

    #endregion

    #region MonoBehaviour Callbacks

    private void OnMouseDown()
    {
        Log.Message($"Нажатие на карту {name}");

        if (IsOpen == false)
        {
            Show();
        }
    }

    #endregion

    #region Public Methods

    public void SetImageMaterial(Material material)
    {
        if (material == null)
        {
            Log.Error($"Материал карты {name} не определен");
            return;
        }

        Log.Message($"Установка материала карты {name}. Материал: {material.name}");

        //TODO: присвоение публичного элемента для сравнения карт

        imageRenderer.sharedMaterial = material;
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

        //TODO: логика переворота карты

        transform.localRotation = Quaternion.Euler(0, 0, 0);

        IsOpen = openState;
    }

    #endregion
}