using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NOJUMPO.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NOJUMPO.ConveyingSystem
{
    public class ConveyorObjectSpawner : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] GameObject m_objectPrefab;
        [Space]
        [SerializeField,Range(0.1f,25.0f)] float m_spawnDelay = 15.0f;
        [Space]
        [SerializeField] Transform[] m_spawnPositions;
        [Space]
        [SerializeField] List<ObjectSpawnData> m_spawnDatas;

        int m_remainingSpawnAmount;
        bool m_isSelected = false;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            PoolManager.Instance.CreatePool(m_objectPrefab, initialSize: 25);
            SetTotalAmount();
        }

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Conveyor Object"))
            {
                ArrivedToDestination(other).Forget();
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void StartSpawning() {
            SetTotalAmount();
            StartCoroutine(SpawnObjects());
        }

        public void StopSpawning() {
            StopAllCoroutines();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetTotalAmount() {
            m_remainingSpawnAmount = 0;
            for (int i = 0; i < m_spawnDatas.Count; i++)
            {
                m_remainingSpawnAmount += m_spawnDatas[i].m_Amount;
            }
        }

        async UniTaskVoid ArrivedToDestination(Collider other) {
            await other.gameObject.transform.DOMove(transform.position, 0.1f).AsyncWaitForCompletion(); ;
            PoolManager.Instance.ReleaseToPool(other.gameObject, m_objectPrefab);
        }

        IEnumerator SpawnObjects() {
            while (m_remainingSpawnAmount > 0)
            {
                yield return NJWait.ForSeconds(m_spawnDelay);

                for (int i = 0; i < m_spawnDatas.Count; i++)
                {
                    if (m_spawnDatas[i].m_Amount > 0)
                        continue;

                    m_spawnDatas.RemoveAt(i);
                }

                int randomIndex = Random.Range(0, m_spawnDatas.Count);
                ObjectSpawnData spawnData = m_spawnDatas[randomIndex];

                int randomPos = Random.Range(0, m_spawnPositions.Length);
                Vector3 position = m_spawnPositions[randomPos].position;

                GameObject pooledObj = PoolManager.Instance.SpawnFromPool(m_objectPrefab, position, Quaternion.identity);

                ConveyorObject conveyorObject = pooledObj.GetComponent<ConveyorObject>();
                conveyorObject.SetDestination(spawnData.m_Destination, spawnData.m_Material);

                m_remainingSpawnAmount--;
                spawnData.SetSpawnAmount(spawnData.m_Amount - 1);
            }
        }
    }
}