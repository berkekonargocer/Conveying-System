using UnityEngine;

namespace NOJUMPO.Modules.Managers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        static T m_instance;

        public static T Instance {
            get {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<T>();

                    if (m_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        m_instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T)}";
                    }
                }

                return m_instance;
            }
        }


        // ------------------------- UNITY BUILT-IN METHODS ------------------------

        protected virtual void Awake() {
            if (m_instance == null)
            {
                m_instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}