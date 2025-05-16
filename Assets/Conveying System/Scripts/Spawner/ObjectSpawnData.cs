using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    [CreateAssetMenu(fileName = "NewObjectSpawnData", menuName = "NOJUMPO/Scriptable Objects/Conveyor Spawner/New Object Spawn Data")]
    public class ObjectSpawnData : ScriptableObject
    {
        // -------------------------------- FIELDS ---------------------------------
        [field: SerializeField] public Destination m_Destination { get; private set; }
        [field: SerializeField] public Material m_Material { get; private set; }
        [field: SerializeField] public int m_Amount { get; private set; }


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            m_Amount = 0;
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SetSpawnAmount(int amount) {
            m_Amount = amount;
        }

        public void SetSpawnAmount(string amount) {
            if (int.TryParse(amount, out int parsedAmount))
            {
                parsedAmount = Mathf.Max(0, parsedAmount);
                m_Amount = parsedAmount;
            }
            else
            {
                Debug.LogWarning("Please Enter Valid Numbers!");
            }
        }
    }
}