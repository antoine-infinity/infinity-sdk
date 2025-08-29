using System;
using UnityEngine;

namespace Infinity.Runtime.Utils
{
    public class FollowTransform : MonoBehaviour
    {
        public Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}