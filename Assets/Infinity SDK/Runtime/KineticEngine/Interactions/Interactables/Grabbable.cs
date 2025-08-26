using UnityEngine;
using UnityEngine.Events;

namespace Infinity.Runtime.XR.Interactions.Interactables
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
    public class Grabbable : MonoBehaviour
    {
        private Rigidbody _rb;

        public UnityEvent hoverEnter;
        public UnityEvent hoverExit;
        public UnityEvent grabEnter;
        public UnityEvent grabExit;

        private bool _kinematic;
        private bool _gravity;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            
            _kinematic = _rb.isKinematic;
            _gravity = _rb.useGravity;
        }

        public void ToggleKinematic(bool state)
        {
            _rb.isKinematic = state;
        }

        public void ToggleGravity(bool state)
        {
            _rb.useGravity = state;
        }

        public void ResetRigidbodyPreviousState()
        {
            _rb.isKinematic = _kinematic;
            _rb.useGravity = _gravity;
        }
        
        public void ApplyVelocity(Vector3 velocity, Vector3 angular)
        {
            _rb.linearVelocity = velocity;
            _rb.angularVelocity = angular;
        }

        public void MoveTowards(Vector3 position, Quaternion rotation)
        {
            _rb.MovePosition(position);
            _rb.MoveRotation(rotation);
        }
    }
}