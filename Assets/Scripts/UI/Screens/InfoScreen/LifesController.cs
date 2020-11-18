using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UI.CustomScreen.InfoScreen
{
	public class LifesController : MonoBehaviour
	{
        #region Serialize Fields

        [SerializeField] private UiLife lifePrefab = null;

        #endregion

        #region Properties

        private List<UiLife> LifeIcons { get; set; } = new List<UiLife>();

        #endregion

        #region MonoBehaviour Callbacks

        private void OnEnable()
        {
            Player.Lifes.OnInitialized += LifesInitializedEventHandler;
            Player.Lifes.OnChanged += LifesChangedEventHandler;
        }

        private void OnDisable()
        {
            Player.Lifes.OnChanged -= LifesChangedEventHandler;
        }

        #endregion

        #region Event Handlers

        private void LifesInitializedEventHandler(int count) //создание иконок жизней
        {
            float itemWidth = (lifePrefab.transform as RectTransform).rect.width;
            float spacing = GetComponent<HorizontalLayoutGroup>().spacing;

            //выставление необходимой ширины, чтобы элементы были по центру HorizontalLayoutGroup 
            (transform as RectTransform).sizeDelta = new Vector2(itemWidth * count + spacing * (count - 1), 0);

            for (int i = 0; i < count; i++)
            {
                UiLife item = GameObject.Instantiate(lifePrefab, transform);
                RectTransform itemTransform = item.transform as RectTransform;
                itemTransform.localRotation = Quaternion.Euler(0, 0, 0);
                itemTransform.anchoredPosition = Vector2.zero;

                LifeIcons.Add(item);
            }

            Player.Lifes.OnInitialized -= LifesInitializedEventHandler;
        }

        private void LifesChangedEventHandler(int lifesCount)
        {
            LifeIcons[lifesCount].Deactivate();
            LifeIcons.RemoveAt(lifesCount);
        }

        #endregion
    }
}