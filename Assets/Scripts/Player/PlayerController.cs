using UnityEngine;
using System.Collections.Generic;
using System;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private int lifesCount = 5;

        #endregion

        #region Properties

        private Lifes Lifes { get; set; } = null;
        private Score Score { get; set; } = null;

        #endregion

        #region MonoBehaviour Callbacks

        //TODO: подписка на события изменения счета и жизней

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnApplicationQuit()
        {
            if (Score?.CheckNewBestScore() ?? false) //если Score не определен, то условие не выполняется
            {
                Score.UpdateBestScore(); //обновление лучшего счета
                Score.SaveBestScore(); //сохранение лучшего счета
            }
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            Lifes = new Lifes(lifesCount);
            Score = new Score();
        }

        #endregion
    }
}