using UnityEngine;

namespace UnityCraft.Patterns
{
    /// <summary>
    /// Signleton design pattern class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DisallowMultipleComponent]
    public class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
    {
        private static T instance;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance is null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T)} (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Property that returns value if Singleton object contains ready instance.
        /// </summary>
        public static bool HasInstance
        {
            get => instance;
        }

        protected virtual void Awake()
        {
            if (!(instance is null))
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
                return;
            }
            instance = (T)this;
            if (transform.parent)
            {
                return;
            }
            DontDestroyOnLoad(instance.gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}