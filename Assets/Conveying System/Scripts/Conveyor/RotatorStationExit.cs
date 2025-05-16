using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public class RotatorStationExit : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] RotatorBelt m_rotatorBelt;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnTriggerExit(Collider other) {
            ConveyorObject conveyorObject = other.GetComponent<ConveyorObject>();
            m_rotatorBelt.FinishProcessing(conveyorObject);
        }
    }
}