using System.Collections.Generic;
using System.Linq;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.KineticEngine.Interactions;
using Infinity.Runtime.Utils;
using Infinity.Runtime.XR.Interactions.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infinity.Runtime.XR.Interactions.Interactors
{
    public class Grabber : MonoBehaviour
    {
        public Handedness handedness;
        public InputActionReference grabAction;

        public Collider grabCollider;
        public float grabThreshold;
        public bool isGrabbing;
        public bool velocityTracking;

        public bool interactorLinkedToPlayer;
        public bool reparentToPlayer;
        public bool disableRendererOnGrab;

        private InfinityPlayerRoot _playerRoot;
        private MeshRenderer _controllerRenderer;

        private HashSet<Grabbable> _hovered = new();
        private Grabbable _current;
        private VelocityTracker _vTracker;

        private void Start()
        {
            ResolveReferences();

            if (velocityTracking) _vTracker = gameObject.AddComponent<VelocityTracker>();
        }

        private void Update()
        {
            HandleInput();
            HandleRenderer();

            if (isGrabbing)
            {
                ProcessGrab();
            }
            else
            {
                ReleaseGrab();
            }
        }
        
        private void ResolveReferences()
        {
            if (interactorLinkedToPlayer)
            {
                _playerRoot = GetComponentInParent<InfinityPlayerRoot>();
                if (_playerRoot == null)
                {
                    InfinityLog.Error(this,
                        $"Player is null but the interactor is linked to player. Problem in Rig Setup or Grabber Setup");
                    enabled = false;
                    return;
                }

                // Get the player controller renderer
                _controllerRenderer = (handedness & Handedness.Right) != 0
                    ? _playerRoot.rightControllerRenderer
                    : _playerRoot.leftControllerRenderer;

                if (reparentToPlayer)
                {
                    // Reparent the grabber to the correct controller
                    transform.SetParent((handedness & Handedness.Right) != 0
                        ? _playerRoot.localRightController
                        : _playerRoot.localLeftController);
                }
            }
        }

        private void HandleInput()
        {
            isGrabbing = grabAction.action.ReadValue<float>() >= grabThreshold;
        }

        private void HandleRenderer()
        {
            if(_controllerRenderer != null)
                _controllerRenderer.enabled = !disableRendererOnGrab || !isGrabbing;
        }

        private void ProcessGrab()
        {
            if (_current != null)
            {
                if (velocityTracking)
                {
                    _current.MoveTowards(transform.position, transform.rotation);
                }
                else
                {
                    _current.transform.SetPositionAndRotation(transform.position, transform.rotation);
                }
            } else if (_hovered.Any())
            {
                SelectGrab();
            }
        }

        private void SelectGrab()
        {
            _current = GetNearestGrabbable();
            _current.grabEnter?.Invoke();

            if (velocityTracking)
            {
                _current.ToggleGravity(false);
            }
            else
            {
                _current.ToggleKinematic(true);
            }
        }

        private void ReleaseGrab()
        {
            if (_current != null)
            {
                _current.ApplyVelocity(_vTracker.Velocity, _vTracker.AngularVelocity);

                _current.ResetRigidbodyPreviousState();

                _current.grabExit?.Invoke();
                _current = null;
            }
        }

        private Grabbable GetNearestGrabbable()
        {
            var minDistance = float.MaxValue;
            Grabbable nearest = null;
            
            foreach (var grabbable in _hovered)
            {
                var distance = Vector3.Distance(transform.position, grabbable.transform.position);
                if (!(distance < minDistance)) continue;

                nearest = grabbable;
                minDistance = distance;
            }

            return nearest;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Grabbable>(out var grabbable))
            {
                _hovered.Add(grabbable);
                grabbable.hoverEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Grabbable>(out var grabbable))
            {
                _hovered.Remove(grabbable);
                grabbable.hoverExit?.Invoke();
            }
        }
    }
}