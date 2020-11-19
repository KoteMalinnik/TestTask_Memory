using UnityEngine;
using System.Collections.Generic;
using System;

public class AndroidInput: MonoBehaviour
{
    #region Events

    public static event Action<Collider> OnTouch = null;

    #endregion

    #region Fields

    private Ray Ray;
    private RaycastHit Hit;

    #endregion

    #region Properties

    private Camera cachedCamera { get; set; }

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
#if UNITY_EDITOR == false
        cachedCamera = Camera.main;
#else
        Destroy(this);
#endif
    }

#if UNITY_EDITOR == false
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                Ray = cachedCamera.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(Ray, out Hit))
                {
                    OnTouch?.Invoke(Hit.collider);
                }
            }
        }
    }
#endif

    #endregion
}