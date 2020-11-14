using UnityEngine;
using Object = UnityEngine.Object;

namespace Extencions
{
    public static class ComponentFinder
    {
        /// <summary>
        /// Вернет первый попавшийся на сцене объект типа T.
        /// </summary>
        public static T FindAtScene<T>() where T : Object
        {
            Log.Message($"Поиск объекта {typeof(T)} на сцене.");

            T obj = default(T);
            obj = GameObject.FindObjectOfType<T>();

            if (obj == null)
            {
                Log.Error($"Искомый объект {typeof(T)} отсутствует на сцене!");
                return null;
            }

            Log.Message($"Объект {typeof(T)} найден.");
            return obj;
        }
		
        /// <summary>
        /// Вернет первый попавшийся в родительском объекте компонент типа T.
        /// </summary>
        public static T FindInParent<T>(GameObject gameObject) where T : Component
        {
            Log.Message($"Поиск компонента {typeof(T)} в родительском объекте.");

            T obj = default(T);
            obj = gameObject.GetComponentInParent<T>();

            if (obj == null)
            {
                Log.Error($"Искомый объект {typeof(T)} отсутствует в родительском объекте!");
                return null;
            }

            Log.Message($"Компонент {typeof(T)} найден.");
            return obj;
        }

        /// <summary>
        /// Вернет первый попавшийся в дочернем объекте компонент типа T.
        /// </summary>
        public static T FindInChildren<T>(GameObject gameObject) where T : Component
        {
            Log.Message($"Поиск компонента {typeof(T)} в дочернем объекте.");

            T obj = default(T);
            obj = gameObject.GetComponentInChildren<T>();

            if (obj == null)
            {
                Log.Error($"Искомый объект {typeof(T)} отсутствует в дочернем объекте!");
                return null;
            }

            Log.Message($"Компонент {typeof(T)} найден.");
            return obj;
        }

        /// <summary>
        /// Вернет первый попавшийся в объекте компонент типа T.
        /// </summary>
        public static T FindInSelf<T>(GameObject gameObject) where T : Component
        {
            Log.Message($"Поиск компонента {typeof(T)} в объекте.");

            T obj = default(T);
            obj = gameObject.GetComponent<T>();

            if (obj == null)
            {
                Log.Error($"Искомый объект {typeof(T)} отсутствует в объекте!");
                return null;
            }

            Log.Message($"Компонент {typeof(T)} найден.");
            return obj;
        }
    }
}