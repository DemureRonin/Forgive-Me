using UnityEngine;

namespace _Scripts.Components
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _distance = 0.1f;
        [SerializeField] private float _maxSlopeAngle;

        private RaycastHit _slopeHit;

        public RaycastHit SlopeHit => _slopeHit;

        public bool IsGrounded()
        {
            var raycast = Physics.Raycast(transform.position, Vector3.down, _distance, _layer);
            return raycast;
        }

        public bool IsOnSlope()
        {
            var raycast = Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _distance);
            if (!raycast) return false;
            var angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle > 0;
        }
    }
}