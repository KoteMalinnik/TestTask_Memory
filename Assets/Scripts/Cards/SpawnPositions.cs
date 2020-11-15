using UnityEngine;
using System.Collections.Generic;

namespace Cards
{
    public class SpawnPositions
    {
        #region Properties

        private List<Vector3> positions { get; } = null;

        #endregion

        #region Constructors

        public SpawnPositions(Vector2 cardScale, int coloumns, int raws, float offset, float spawnPositionY)
        {
            positions = new List<Vector3>();

            //отсуп на половину с учетом размера карты по оси: (var - cardScale) / 2
            //учет отступа между элементами: (var - 1) * offset / 2
            //итоговый отступ: (var - cardScale) / 2 + (var - 1) * offset / 2
            float coloumnOffset = (coloumns - cardScale.x + (coloumns - 1) * offset) / 2;
            float rawOffset = (raws - cardScale.y + (raws - 1) * offset) / 2;

            Log.Message($"Смещение: по столбцу = {coloumnOffset}, по строке = {rawOffset}");

            for (int raw = 0; raw < raws; raw++)
            {
                for (int coloumn = 0; coloumn < coloumns; coloumn++)
                {
                    //благодаря смещению центр сетки с картами будет находится в нулевой координате
                    float spawnPositionX = coloumn - coloumnOffset + coloumn * offset;
                    float spawnPositionZ = raw - rawOffset + raw * offset;
                    Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);

                    positions.Add(spawnPosition);
                }
            }
        }

        #endregion

        #region Public Methods

        public bool TryGetPosition(out Vector3 position)
        {
            if (positions.Count == 0)
            {
                position = Vector3.zero;
                return false;
            }

            //выбираем случайное значение из индекса позиций и удаляем его из списка
            int index = UnityEngine.Random.Range(0, positions.Count);

            position = positions[index];

            positions.RemoveAt(index);

            return true;
        }

        #endregion
    }
}