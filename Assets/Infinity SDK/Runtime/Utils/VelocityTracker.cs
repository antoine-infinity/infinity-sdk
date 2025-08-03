using UnityEngine;

namespace Infinity.Runtime.Utils
{
    public class VelocityTracker : MonoBehaviour
    {
        private Vector3 _linearVelocity;
        private Vector3 _previousLinear;

        private Vector3 _angularVelocity;
        private Quaternion _deltaRotation;
        private Quaternion _previousRotation;

        public Vector3 Velocity => _linearVelocity;
        public Vector3 AngularVelocity => _angularVelocity;

        private void Start()
        {
            _previousLinear = transform.position;
            _previousRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            _linearVelocity = (transform.position - _previousLinear) / Time.fixedDeltaTime;
            _deltaRotation = transform.rotation * Quaternion.Inverse(_previousRotation);

            _deltaRotation.ToAngleAxis(out var angle, out var axis);
            angle *= Mathf.Deg2Rad;

            _angularVelocity = (1.0f / Time.deltaTime) * angle * axis;

            _previousLinear = transform.position;
            _previousRotation = transform.rotation;
        }
    }
}