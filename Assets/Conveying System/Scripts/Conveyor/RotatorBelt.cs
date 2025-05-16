using System.Collections;
using System.Collections.Concurrent;
using NOJUMPO.Utils;
using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public class RotatorBelt : ConveyorBelt
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] MoveDirection m_destinationADirection;
        [SerializeField] MoveDirection m_destinationBDirection;
        [SerializeField] MoveDirection m_destinationCDirection;
        [SerializeField] MoveDirection m_destinationDDirection;

        [SerializeField] bool m_isProcessing = false;

        ConcurrentQueue<RotationQueueData> m_rotateQueue = new ConcurrentQueue<RotationQueueData>();


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Start() {
            StartCoroutine(DequeueRoutine());
        }

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Conveyor Object"))
            {
                ConveyorObject conveyorObject = other.GetComponent<ConveyorObject>();
                RotateToDestination(conveyorObject);
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void QueueConveyorObject(RotationQueueData rotationQueueData) {
            ConveyorObject conveyorObject = rotationQueueData.m_ConveyorObject;
            conveyorObject.m_HasBeenQueued = true;
            conveyorObject.SetQueueState(true);
            conveyorObject.SetGorilla(true);

            m_rotateQueue.Enqueue(rotationQueueData);
        }

        public void FinishProcessing(ConveyorObject conveyorObject) {
            RemoveConveyorObject(conveyorObject);
            conveyorObject.m_HasBeenQueued = false;
            m_isProcessing = false;
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void TryProcessNextObject() {
            if (m_isProcessing) return;

            if (m_rotateQueue.Count == 0) return;

            if (!m_rotateQueue.TryDequeue(out RotationQueueData rotationQueueData))
                return;

            m_isProcessing = true;
            ConveyorObject conveyorObject = rotationQueueData.m_ConveyorObject;

            SetDirection(rotationQueueData.m_ComingDirection);
            conveyorObject.SetGorilla(false);
            conveyorObject.SetQueueState(false);
        }

        void RotateToDestination(ConveyorObject conveyorObject) {
            SetDestinationDirection(conveyorObject.m_Destination);
        }

        void SetDestinationDirection(Destination destination) {
            switch (destination)
            {
                case Destination.A:
                    SetDirection(m_destinationADirection);
                    break;
                case Destination.B:
                    SetDirection(m_destinationBDirection);
                    break;
                case Destination.C:
                    SetDirection(m_destinationCDirection);
                    break;
                case Destination.D:
                    SetDirection(m_destinationDDirection);
                    break;
                default:
                    break;
            }
        }

        IEnumerator DequeueRoutine() {
            while (true)
            {
                yield return NJWait.ForSeconds(0.05f);

                if (!m_rotateQueue.IsEmpty)
                {
                    TryProcessNextObject();
                }
            }
        }
    }
}