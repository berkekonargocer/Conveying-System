using MoveDirection = NOJUMPO.ConveyingSystem.ConveyorBelt.MoveDirection;
using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public class RotatorStationEnter : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] RotatorBelt m_rotatorBelt;
        [SerializeField] MoveDirection m_moveDirection;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Conveyor Object"))
            {
                ConveyorObject conveyorObject = other.GetComponent<ConveyorObject>();

                if (conveyorObject.m_HasBeenQueued)
                    return;

                RotationQueueData rotationQueueData = new RotationQueueData(conveyorObject, m_moveDirection);

                m_rotatorBelt.QueueConveyorObject(rotationQueueData);
            }
        }
    }
}