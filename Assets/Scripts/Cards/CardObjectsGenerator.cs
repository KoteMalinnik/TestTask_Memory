using UnityEngine;
using System.Collections.Generic;

namespace Cards
{
    public class CardObjectsGenerator : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Параметры колод")]
        [SerializeField] private int cardDeckCount = 5; //количество колод
        [SerializeField] private int cardDeckCapacity = 3; //вместимость колоды

        [Header("Параметры генерации")]
        [SerializeField] private int raws = 5; //количество строк
        [SerializeField] private int coloumns = 3; //количество столбцов
        [SerializeField] private float offset = 0.2f; //отступ между соседними элементами
        [Space]
        [SerializeField] private Card cardPrefab = null; //префаб карты
        [SerializeField] private Transform cardParentTransform = null; //родительский Transform сгенерированых карт
        [SerializeField] private float spawnPositionY = 0.1f; //отступ по оси Y от нулевой координаты

        [Header("Текстуры карт")]
        [SerializeField] private CardImagesSet imagesSet = null; //сет текстур для карт
        [SerializeField] private Material imageMaterial = null; //материал с текстурой

        #endregion

        #region Public Methods
        
        public Card[] Generate()
        {
            Log.Message($"Генерация объектов {typeof(Card)}");

            //проверка совпадения количества текстур с количеством колод
            if (imagesSet.Length != cardDeckCount)
            {
                Log.Error($"Указанное количество колод в генераторе ({cardDeckCount}) " +
                    $"не совпадает с количеством текстур для колод ({imagesSet.Length})");

                return null;
            }

            //проверка совпадения количества карт с размером игрового поля
            if (raws * coloumns != cardDeckCount * cardDeckCapacity)
            {
                Log.Error($"Общее количество карт во всех колодах ({cardDeckCount * cardDeckCapacity}) " +
                    $"не совпадает с расчетным количеством карт ({raws * coloumns}) " +
                    $"исходя из размеров игровго поля (строки: {raws}, столбцы: {coloumns})");

                return null;
            }

            //Ось Х - вправо
            //Ось Z - вверх
            SpawnPositions spawnPositions = new SpawnPositions(cardPrefab.transform.localScale, coloumns, raws, offset, spawnPositionY);

            List<Card> cards = new List<Card>();
            for (int deskNumber = 0; deskNumber < cardDeckCount; deskNumber++)
            {
                Material material = new Material(imageMaterial);
                material.mainTexture = imagesSet.GetImage(deskNumber);

                for (int cardNumber = 0; cardNumber < cardDeckCapacity; cardNumber++)
                {
                    Vector3 spawnPosition;
                    if (spawnPositions.TryGetPosition(out spawnPosition) == true)
                    {
                        Card item = CreateCard(material, spawnPosition, deskNumber, cardNumber);
                        cards.Add(item);
                    }
                }
            }

            Log.Message($"Генерация завершена");

            return cards.ToArray();
        }

        #endregion

        #region Private Methods

        private Card CreateCard(Material imageMaterial, Vector3 spawnPosition, int deskNumber, int cardNumber)
        {
            Card card = GameObject.Instantiate(cardPrefab, spawnPosition, Quaternion.identity, cardParentTransform);
            card.SetMaterial(imageMaterial);
            card.name = $"Card-{cardNumber}-{imageMaterial.mainTexture.name}";

            return card;
        }

        #endregion
    }
}