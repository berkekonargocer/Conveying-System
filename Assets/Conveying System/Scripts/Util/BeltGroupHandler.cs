using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public class BeltGroupHandler : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField, Range(1, 4)] float m_conveyorSpeed = 2f;

        ConveyorBelt[] m_childBelts;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            m_childBelts = GetComponentsInChildren<ConveyorBelt>();
        }

        void Start() {
            UpdateChildConveyors();
        }

        void OnValidate() {
            UpdateChildConveyors();
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SetConveyorSpeed(float newSpeed) {
            m_conveyorSpeed = newSpeed;
            UpdateChildConveyors();
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void UpdateChildConveyors() {
            if (m_childBelts == null) return;

            foreach (var belt in m_childBelts)
            {
                if (belt != null)
                {
                    belt.SetSpeed(m_conveyorSpeed);
                }
            }
        }
    } 
}
