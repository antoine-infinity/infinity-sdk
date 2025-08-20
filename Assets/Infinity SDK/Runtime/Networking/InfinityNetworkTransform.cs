using Unity.Netcode;
using UnityEngine;

namespace Infinity.Runtime.Networking
{
    public class InfinityNetworkTransform : NetworkBehaviour
    {
        [Header("Position")]
        public bool positionX = true;
        public bool positionY = true;
        public bool positionZ = true;
        
        [Space(10), Header("Rotation")]
        public bool rotationX = true;
        public bool rotationY = true;
        public bool rotationZ = true;
        
        [Space(10), Header("Options")]
        public int positionSmoothingFrames = 5;
        public float positionExtrapolationFactor = 1f;
        public float rotationInterpolationFactor = 0.5f;
    }
}