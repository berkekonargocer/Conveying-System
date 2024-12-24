using System.Collections.Generic;
using UnityEngine;
using NOJUMPO.Modules.Managers;

namespace NOJUMPO.CaseStudy
{
    public class PoolManager : Singleton<PoolManager>
    {
        // -------------------------------- FIELDS ---------------------------------
        Dictionary<GameObject, ObjectPool> m_pools = new Dictionary<GameObject, ObjectPool>();


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void CreatePool(GameObject prefab, int initialSize = 10) {
            if (!m_pools.ContainsKey(prefab))
            {
                var newPool = new ObjectPool(prefab, initialSize, this.transform);
                m_pools.Add(prefab, newPool);
            }
        }

        public GameObject SpawnFromPool(GameObject prefab,
            Vector3 position, Quaternion rotation) {
            if (!m_pools.ContainsKey(prefab))
            {
                CreatePool(prefab, 1);
            }

            var pooledObject = m_pools[prefab].GetObject();
            pooledObject.transform.SetPositionAndRotation(position, rotation);
            pooledObject.SetActive(true);
            return pooledObject;
        }

        public void ReleaseToPool(GameObject obj, GameObject prefab) {
            if (m_pools.ContainsKey(prefab))
            {
                m_pools[prefab].ReleaseObject(obj);
            }
            else
            {
                Destroy(obj);
            }
        }
    }
}