using System;
using Infinity.Runtime.Core.Logging;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public class ControllerInteractor : NetworkBehaviour
    {
        [SerializeField] private InputActionReference gripAction;
        [SerializeField] private InputActionReference triggerAction;

        public Transform attachPoint;
        public float interactionRadius = 0.2f;
        public LayerMask interactionLayerMask = ~0;
        public Handedness handedness;
        
        private Collider[] _hitBuffer = new Collider[10];

        private Interactable _hovered;
        private Interactable _currentInteractable;

        public override void OnNetworkSpawn()
        {
            if (attachPoint == null)
            {
                var newAttach = new GameObject("[SimpleInteractor] Attach Point");
                newAttach.transform.SetParent(transform);
            }
        }

        private void Update()
        {
            if (!IsOwner) return;

            DetectInteractables();
            HandleGrip();
            HandleTrigger();

            if (_hovered && _hovered.OwnerClientId == OwnerClientId)
            {
                if (!_currentInteractable)
                {
                    _currentInteractable = _hovered;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f,0.4f,0.1f,0.3f);
            Gizmos.DrawSphere(transform.position, interactionRadius);
        }

        private void HandleGrip()
        {
            if (gripAction.action.WasPressedThisFrame())
            {
                if (_hovered)
                    InteractionManager.Instance.RequestInteraction(NetworkObjectId, _hovered.NetworkObjectId,
                        InteractionType.Interacting, handedness);
            }
            else if (gripAction.action.WasReleasedThisFrame())
            {
                if (_currentInteractable)
                {
                    InteractionManager.Instance.RequestInteraction(NetworkObjectId,
                        _currentInteractable.NetworkObjectId, InteractionType.EndInteracting, handedness);
                    _currentInteractable = null;
                }
            }
        }

        private void HandleTrigger()
        {
            if (triggerAction.action.WasPressedThisFrame())
            {
                if (_currentInteractable)
                {
                    InteractionManager.Instance.RequestInteraction(NetworkObjectId,
                        _currentInteractable.NetworkObjectId, InteractionType.Activating, handedness);
                }
            }
            else if (triggerAction.action.WasReleasedThisFrame())
            {
                if (_currentInteractable)
                {
                    InteractionManager.Instance.RequestInteraction(NetworkObjectId,
                        _currentInteractable.NetworkObjectId, InteractionType.EndActivating, handedness);
                }
            }
        }

        private void DetectInteractables()
        {
            // Find the closest interactable to the interactor.
            var hits = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, _hitBuffer,
                interactionLayerMask);
            Interactable closest = null;
            var closestDist = float.MaxValue;

            for (int i = 0; i < hits; i++)
            {
                if (!_hitBuffer[i].TryGetComponent(out Interactable interactable)) continue;

                var dist = Vector3.Distance(transform.position, _hitBuffer[i].transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = interactable;
                }
            }

            // if the closest interactor is not the one currently hovered. Process events and replace the current hover
            if (closest != _hovered)
            {
                if (_hovered)
                    InteractionManager.Instance.RequestInteraction(NetworkObjectId, _hovered.NetworkObjectId,
                        InteractionType.EndHovering, handedness);
                
                _hovered = closest;
                
                if (_hovered)
                    InteractionManager.Instance.RequestInteraction(NetworkObjectId, _hovered.NetworkObjectId,
                        InteractionType.Hovering, handedness);
            }
        }
    }
}