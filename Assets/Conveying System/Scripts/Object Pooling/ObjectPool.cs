using System.Collections.Generic;
using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public class ObjectPool
    {
        // -------------------------------- FIELDS ---------------------------------
        Queue<GameObject> m_objectQueue;
        GameObject m_prefab;
        Transform m_poolParent;


        // ---------------------------- CONSTRUCTORS ------------------------------
        public ObjectPool(GameObject prefab, int initialSize, Transform poolParent) {
            m_prefab = prefab;
            m_poolParent = poolParent;
            m_objectQueue = new Queue<GameObject>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                GameObject newObject = CreateNewObject();
                m_objectQueue.Enqueue(newObject);
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public GameObject GetObject() {
            if (m_objectQueue.Count == 0)
            {
                m_objectQueue.Enqueue(CreateNewObject());
            }

            return m_objectQueue.Dequeue();
        }

        public void ReleaseObject(GameObject gameObject) {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(m_poolParent);
            m_objectQueue.Enqueue(gameObject);
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        GameObject CreateNewObject() {
            GameObject newObject = Object.Instantiate(m_prefab, m_poolParent);
            newObject.SetActive(false);
            return newObject;
        }
    }
}