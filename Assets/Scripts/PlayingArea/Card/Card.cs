using UnityEngine;
using System;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        #region Evenets

        public static event Action<Card> OnShowed = null;
        public static event Action<Card> OnHided = null;

        #endregion

        #region Serialized Fields

        [SerializeField] private MeshRenderer imageRenderer = null;
        [Range(1, 10)]
        [SerializeField] private float flipAnimationSpeed = 5.0f; //скорость анимации переворота

        #endregion

        #region Properties

        private bool IsOpen { get; set; } = false;
        private static bool ControlBlock { get; set; } = false; 
        public string DeckName => imageRenderer?.sharedMaterial?.mainTexture?.name ?? "NotDefined";

        #endregion

        #region MonoBehaviour Callbacks

        private void OnMouseDown()
        {
            Log.Message($"Нажатие на карту {name}");

            if (ControlBlock == true)
            {
                Log.Message("Карта заблокирована");

                return;
            }

            if (IsOpen == false) //открытие карты игроком должно происходить только один раз
            {
                Show();
            }
        }

        #endregion

        #region Public Methods

        #region Static

        public static void Block() => ControlBlock = true; //блокировка нажатия на карту
        public static void Unblock() => ControlBlock = false; //разблокировака нажатия на карточку

        #endregion

        public void SetMaterial(Material material)
        {
            if (material == null)
            {
                Log.Error("Материал не определена");
                return;
            }

            Log.Message($"Установка материала карты {name}. Текстура: {material.mainTexture.name}");

            imageRenderer.material = material;
        }

        public void Show(bool publishEvent = true)
        {
            Log.Message($"Открытие карты {name}");

            if (publishEvent == true)
            {
                Flip(true, () => OnShowed?.Invoke(this));
            }
            else
            {
                Flip(true, null);
            }
        }

        public void Hide()
        {
            Log.Message($"Закрытие карты {name}");

            Flip(false, () => OnHided?.Invoke(this));
        }

        #endregion

        #region Private Methods

        private void Flip(bool openState, Action onFinish)
        {
            onFinish += () => IsOpen = openState;

            float speed = openState == true ? flipAnimationSpeed : -flipAnimationSpeed;

            FlipoverCoroutine flipAnimation = new FlipoverCoroutine(this, flipAnimationSpeed, onFinish);
            flipAnimation.Play();
        }

        #endregion
    }
}