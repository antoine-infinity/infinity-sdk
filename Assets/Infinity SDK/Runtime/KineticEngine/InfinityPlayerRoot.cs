using Infinity.Runtime.KineticEngine.Interactions;
using Unity.Netcode;
using UnityEngine;

namespace Infinity.Runtime.XR
{
    public class InfinityPlayerRoot : NetworkBehaviour
    {
        public static InfinityPlayerRoot LocalPlayer
        {
            get;
            private set;
        }
        
        public Transform localRoot;
        public Transform localHeadset;
        public Transform localLeftController;
        public Transform localRightController;

        public Transform remoteRoot;
        public Transform remoteHeadset;
        public Transform remoteLeftController;
        public Transform remoteRightController;

        public MeshRenderer leftControllerRenderer;
        public MeshRenderer rightControllerRenderer;

        public ControllerInteractor leftControllerInteractor;
        public ControllerInteractor rightControllerInteractor;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                localRoot.gameObject.SetActive(false);
            }
            else
            {
                LocalPlayer = this;
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

        public ControllerInteractor GetControllerInteractor(Handedness handedness)
        {
            return handedness switch
            {
                Handedness.Left => leftControllerInteractor,
                Handedness.Right => rightControllerInteractor,
                _ => null
            };
        }
    }
}