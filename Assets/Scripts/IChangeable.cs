using UnityEngine;
using System.Collections.Generic;
using System;

public interface IChangeable
{
    #region Events

    event Action<int> OnChanged;

    #endregion

    #region Properties

    int Value { get; set; }

    #endregion
}