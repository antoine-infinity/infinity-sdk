using Unity.Netcode;
using UnityEngine;

namespace Infinity.Runtime.XR
{
    public class InfinityPlayerRoot : NetworkBehaviour
    {
        public Transform localRoot;
        public Transform localHeadset;
        public Transform localLeftController;
        public Transform localRightController;

        public Transform remoteRoot;
        public Transform remoteHeadset;
        public Transform remoteLeftController;
        public Transform remoteRightController;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                localRoot.gameObject.SetActive(false);
            }
        }

        private void LateUpdate()
        {
            if (!IsOwner) return;
            
            remoteRoot.SetPositionAndRotation(localRoot.position, localRoot.rotation);
            remoteHeadset.SetPositionAndRotation(localHeadset.position, localHeadset.rotation);
            remoteLeftController.SetPositionAndRotation(localLeftController.position, localLeftController.rotation);
            remoteRightController.SetPositionAndRotation(localRightController.position, localRightController.rotation);
        }
    }
}