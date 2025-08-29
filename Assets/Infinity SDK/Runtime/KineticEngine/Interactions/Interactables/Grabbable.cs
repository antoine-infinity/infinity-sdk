using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.XR;
using Unity.Netcode.Components;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public class Grabbable : Interactable
    {
        public bool isGrabbed;

        public void LateUpdate()
        {
            if (IsOwner)
            {
                if (isGrabbed)
                {
                    InfinityLog.Info(this, $"{InfinityPlayerRoot.LocalPlayer.name} is grabbing {gameObject.name}");
                    InfinityLog.Info(this, $"{interactorHandedness.Value}");
                    var interactor = interactorHandedness.Value == Handedness.Left ? InfinityPlayerRoot.LocalPlayer.leftControllerInteractor : InfinityPlayerRoot.LocalPlayer.rightControllerInteractor;
                    if (interactor)
                    {
                        transform.position = interactor.attachPoint.position;
                        transform.rotation = interactor.attachPoint.rotation;
                    }
                }
            }
        }

        public void DebugLocalPlayer()
        {
            InfinityLog.Info(this, $"Local Player: {InfinityPlayerRoot.LocalPlayer.name}");
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            onInteracting.AddListener(Grab);
            onIdle.AddListener(UnGrab);
        }

        private void Grab()
        {
            isGrabbed = true;
        }

        private void UnGrab()
        {
            isGrabbed = false;
        }
    }
}