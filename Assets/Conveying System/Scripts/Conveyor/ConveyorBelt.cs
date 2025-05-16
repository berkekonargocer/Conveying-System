using System.Collections.Generic;
using UnityEngine;

namespace NOJUMPO.ConveyingSystem
{
    public class ConveyorBelt : MonoBehaviour
    {
        public enum MoveDirection
        {
            FORWARD,
            BACKWARD,
            RIGHT,
            LEFT
        }

        // -------------------------------- FIELDS ---------------------------------
        [SerializeField, Range(1, 4)] protected float m_beltSpeed = 2.0f;

        [SerializeField] protected MoveDirection m_moveDirection;

        protected HashSet<ConveyorObject> m_conveyorObjectsOnBelt = new HashSet<ConveyorObject>();

        protected Vector3 m_forceVector;
        protected Vector2 m_scrollDirection;
        Material m_material;

        const float c_BELT_SCROLL_SPEED_MULTIPLIER = 0.59f;
        const float c_SPEED_MULTIPLIER = 0.5f;


        // ------------------------- UNITY BUILT-IN METHODS ------------------------
        protected virtual void Awake() {
            SetComponents();
            SetDirection(m_moveDirection);
        }

        protected virtual void Update() {
            ScrollBelt();
        }

        protected virtual void FixedUpdate() {
            MoveConveyorObjects();
        }

        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Conveyor Object"))
            {
                ConveyorObject conveyorObject = collision.gameObject.GetComponent<ConveyorObject>();
                AddConveyorObject(conveyorObject);
            }
        }

        protected virtual void OnCollisionExit(Collision collision) {
            if (collision.gameObject.CompareTag("Conveyor Object"))
            {
                ConveyorObject conveyorObject = collision.gameObject.GetComponent<ConveyorObject>();
                RemoveConveyorObject(conveyorObject);
            }
        }


        // ------------------------- CUSTOM PUBLIC METHODS -------------------------
        public virtual void AddConveyorObject(ConveyorObject conveyorObject) {
            if (m_conveyorObjectsOnBelt.Contains(conveyorObject))
                return;

            m_conveyorObjectsOnBelt.Add(conveyorObject);
        }

        public void RemoveConveyorObject(ConveyorObject conveyorObject) {
            if (!m_conveyorObjectsOnBelt.Contains(conveyorObject))
                return;

            m_conveyorObjectsOnBelt.Remove(conveyorObject);
        }

        public void SetSpeed(float speed) {
            m_beltSpeed = speed;
        }


        // ------------------------ CUSTOM PROTECTED METHODS -----------------------
        protected void MoveConveyorObject(ConveyorObject conveyorObject) {
            conveyorObject.SetVelocity(m_forceVector, m_beltSpeed * c_SPEED_MULTIPLIER);
        }

        protected virtual void MoveConveyorObjects() {
            foreach (ConveyorObject conveyorObject in m_conveyorObjectsOnBelt)
            {
                if (conveyorObject.m_IsInQueue)
                {
                    continue;
                }

                MoveConveyorObject(conveyorObject);
            }
        }

        protected void SetDirection(MoveDirection moveDirection) {
            switch (moveDirection)
            {
                case MoveDirection.FORWARD:
                    m_forceVector = transform.forward;
                    m_scrollDirection = Vector2.up;
                    break;
                case MoveDirection.BACKWARD:
                    m_forceVector = -transform.forward;
                    m_scrollDirection = -Vector2.up;
                    break;
                case MoveDirection.RIGHT:
                    m_forceVector = transform.right;
                    m_scrollDirection = Vector2.right;
                    break;
                case MoveDirection.LEFT:
                    m_forceVector = -transform.right;
                    m_scrollDirection = -Vector2.right;
                    break;
                default:
                    m_forceVector = transform.forward;
                    m_scrollDirection = Vector2.up;
                    break;
            }
        }

        protected void ScrollBelt() {
            Vector2 textureOffset = m_material.mainTextureOffset;

            textureOffset += m_scrollDirection * m_beltSpeed * c_SPEED_MULTIPLIER * c_BELT_SCROLL_SPEED_MULTIPLIER * Time.deltaTime;
            m_material.mainTextureOffset = new Vector2(Mathf.Repeat(textureOffset.x, 1f), Mathf.Repeat(textureOffset.y, 1f));
        }


        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            m_material = GetComponent<MeshRenderer>().material;
        }
    }
}