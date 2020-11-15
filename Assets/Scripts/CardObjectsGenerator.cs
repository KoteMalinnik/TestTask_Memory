using UnityEngine;
using System.Collections.Generic;
using System;

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

    #endregion

    private void Awake()
    {
        Generate();
    }

    public Card[] Generate()
    {
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
        List<Vector3> spawnPositions = GetSpawnPositions();

        List<Card> cards = new List<Card>();
        for (int deskNumber = 0; deskNumber < cardDeckCount; deskNumber++)
        {
            for (int cardNumber = 0; cardNumber < cardDeckCapacity; cardNumber++)
            {
                Card item = InstantiateCard(spawnPositions, deskNumber, cardNumber);
                cards.Add(item);
            }
        }

        return cards.ToArray();
    }

    private List<Vector3> GetSpawnPositions()
    {
        Vector2 cardScale = cardPrefab.transform.localScale;

        //отсуп на половину с учетом размера карты по оси: (var - cardScale) / 2
        //учет отступа между элементами: (var - 1) * offset / 2
        //итоговый отступ: (var - cardScale) / 2 + (var - 1) * offset / 2
        float coloumnOffset = (coloumns - cardScale.x + (coloumns - 1) * offset) / 2;
        float rawOffset = (raws - cardScale.y + (raws - 1) * offset) / 2;

        Log.Message($"Смещение: по столбцу = {coloumnOffset}, по строке = {rawOffset}");

        List<Vector3> spawnPositions = new List<Vector3>();
        for (int raw = 0; raw < raws; raw++)
        {
            for (int coloumn = 0; coloumn < coloumns; coloumn++)
            {
                //благодаря смещению центр сетки с картами будет находится в нулевой координате
                float spawnPositionX = coloumn - coloumnOffset + coloumn * offset;
                float spawnPositionZ = raw - rawOffset + raw * offset;
                Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);

                spawnPositions.Add(spawnPosition);
            }
        }

        return spawnPositions;
    }

    private Card InstantiateCard(List<Vector3> spawnPositions, int deskNumber, int cardNumber)
    {
        //выбираем случайное значение из индекса позиций и удаляем его из списка
        int index = UnityEngine.Random.Range(0, spawnPositions.Count);
        Vector3 spawnPosition = spawnPositions[index];
        spawnPositions.RemoveAt(index);

        Card card = GameObject.Instantiate(cardPrefab, spawnPosition, Quaternion.identity, cardParentTransform);

        Texture texture = imagesSet.GetImage(deskNumber);
        card.SetImage(texture);

        card.name = $"Card-{cardNumber}-{texture.name}";

        return card;
    }
}