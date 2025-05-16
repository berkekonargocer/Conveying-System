using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public enum Destination
    {
        A,
        B,
        C,
        D
    }

    [RequireComponent(typeof(Rigidbody))]
    public class ConveyorObject : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        public Destination m_Destination { get { return m_destination; } private set { m_destination = value; } }
        [SerializeField] Destination m_destination = Destination.A;

        public bool m_HasBeenQueued { get; set; }
        public bool m_IsInQueue { get; private set; }

        Rigidbody m_rigidbody;
        Renderer m_renderer;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }

        void OnEnable() {
            m_HasBeenQueued = false;
            m_IsInQueue = false;
        }

        void OnDisable() {
            m_HasBeenQueued = false;
            m_IsInQueue = false;
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public void SetDestination(Destination destination, Material material) {
            m_destination = destination;
            m_renderer.sharedMaterial = material;
        }

        public void SetVelocity(Vector3 direction) {
            m_rigidbody.linearVelocity = direction;
        }

        public void SetVelocity(Vector3 direction, float multiplier) {
            m_rigidbody.linearVelocity = direction.normalized * multiplier;
        }

        public void SetQueueState(bool isInQueue) {
            m_IsInQueue = isInQueue;
        }

        public void SetGorilla(bool gorilla) {
            if (gorilla)
            {
                m_rigidbody.linearDamping = 9999.0f;
                m_rigidbody.angularDamping = 9999.0f;
                m_rigidbody.mass = 9999.0f;
                return;
            }

            m_rigidbody.linearDamping = 0.0f;
            m_rigidbody.angularDamping = 0.05f;
            m_rigidbody.mass = 1.0f;
        }

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            m_rigidbody = GetComponent<Rigidbody>();
            m_renderer = GetComponent<Renderer>();
        }
    }
}