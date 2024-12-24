using MoveDirection = NOJUMPO.ConveyorBelt.MoveDirection;

namespace NOJUMPO
{
    public class RotationQueueData
    {
        // -------------------------------- FIELDS ---------------------------------
        public ConveyorObject m_ConveyorObject;
        public MoveDirection m_ComingDirection;

        // ----------------------------- CONSTRUCTORS ------------------------------
        public RotationQueueData(ConveyorObject conveyorObject, MoveDirection direction) {
            m_ConveyorObject = conveyorObject;
            m_ComingDirection = direction;
        }
    }
}