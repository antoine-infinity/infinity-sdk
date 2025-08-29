using System;
using System.Collections.Generic;
using System.Linq;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.XR;
using Unity.Netcode;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public class InteractionManager : NetworkBehaviour
    {
        public static InteractionManager Instance { get; private set; }
        public bool debugLog;
        private void Awake() => Instance = this;

        private readonly Queue<InteractionRequest> _interactionRequests = new();
        
        private void LateUpdate()
        {
            if (!IsServer) return;

            ProcessRequests();
        }
        
        private void ProcessRequests()
        {
            if (_interactionRequests.Count == 0) return;

            var request = _interactionRequests.Dequeue();
            InfinityLog.Server(this, $"Processing interaction request: {request}", debugLog);

            var interactorObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[request.InteractorId];
            var interactableObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[request.ObjectId];

            var interactable = interactableObject.GetComponent<Interactable>();
            interactable.interactorObjectId.Value = request.InteractorId;
            interactable.interactorHandedness.Value = request.Handedness;

            switch (request.Type)
            {
                case InteractionType.Interacting:
                    interactableObject.ChangeOwnership(interactorObject.OwnerClientId);
                    interactable.SetState(InteractableState.Interacting);
                    break;
                case InteractionType.EndInteracting:
                    interactableObject.RemoveOwnership();
                    interactable.SetState(InteractableState.Idle);
                    break;
                case InteractionType.Activating:
                    interactable.SetState(InteractableState.Activated);
                    break;
                case InteractionType.EndActivating:
                    interactable.SetState(InteractableState.Interacting);
                    break;
                case InteractionType.Hovering:
                    interactable.SetState(InteractableState.Hovered);
                    break;
                case InteractionType.EndHovering:
                    interactable.SetState(InteractableState.Idle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        #region Interaction Request
        public void RequestInteraction(ulong interactorId, ulong objectId, InteractionType type, Handedness handedness)
        {
            if (!IsServer)
            {
                RequestInteractionRpc(interactorId, objectId, type, handedness);
                return;
            }

            _interactionRequests.Enqueue(new InteractionRequest(interactorId, objectId, type, handedness));
        }

        [Rpc(SendTo.Server, RequireOwnership = false)]
        private void RequestInteractionRpc(ulong interactorId, ulong objectId, InteractionType type, Handedness handedness)
        {
            RequestInteraction(interactorId, objectId, type, handedness);
        }
        #endregion
    }

    public readonly struct InteractionRequest
    {
        public readonly ulong InteractorId;
        public readonly ulong ObjectId;
        public readonly InteractionType Type;
        public readonly Handedness Handedness;

        public InteractionRequest(ulong interactorId, ulong objectId, InteractionType type, Handedness handedness)
        {
            InteractorId = interactorId;
            ObjectId = objectId;
            Type = type;
            Handedness = handedness;
        }

        public override string ToString()
        {
            return $"Request[{InteractorId}, {ObjectId}, {Type}, {Handedness}]";
        }
    }

    public enum InteractionType
    {
        Interacting,
        EndInteracting,
        Activating,
        EndActivating,
        Hovering,
        EndHovering
    }
}