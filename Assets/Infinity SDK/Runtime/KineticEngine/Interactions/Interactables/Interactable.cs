using System;
using Unity.Netcode;
using UnityEngine.Events;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public class Interactable : NetworkBehaviour
    {
        public NetworkVariable<InteractableState> state = new();
        public NetworkVariable<Handedness> interactorHandedness = new();
        public NetworkVariable<ulong> interactorObjectId = new();

        public UnityEvent onIdle;
        public UnityEvent onHover;
        public UnityEvent onInteracting;
        public UnityEvent onActivated;

        public override void OnNetworkSpawn()
        {
            state.OnValueChanged += StateValueChanged;
        }

        private void StateValueChanged(InteractableState previousvalue, InteractableState newvalue)
        {
            switch (newvalue)
            {
                case InteractableState.Idle:
                    onIdle?.Invoke();
                    break;
                case InteractableState.Hovered:
                    onHover?.Invoke();
                    break;
                case InteractableState.Interacting:
                    onInteracting?.Invoke();
                    break;
                case InteractableState.Activated:
                    onActivated?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newvalue), newvalue, null);
            }
        }

        public void SetState(InteractableState newState)
        {
            if (!IsServer) return;

            state.Value = newState;
        }
    }

    public enum InteractableState
    {
        Idle,
        Hovered,
        Interacting,
        Activated
    }
}