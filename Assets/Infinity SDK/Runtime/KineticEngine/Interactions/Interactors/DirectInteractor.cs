using System;
using UnityEngine;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public class DirectInteractor : MonoBehaviour, IInteractor
    {
        [Header("Settings")] public LayerMask interactableMask;
        public float grabRange = 0.1f;
        public Handedness handedness;

        private IInteractable _hovered;
        private IInteractable _selected;

        private readonly Collider[] _overlapBuffer = new Collider[8];
        public Transform attachPoint;

        #region Unity Callbacks

        private void Awake()
        {
            if (attachPoint == null)
            {
                attachPoint = new GameObject("[DirectInteractor] Attach").transform;
                attachPoint.SetParent(transform, false);
            }
        }

        private void Update()
        {
            DetectInteractables();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.5f, 0.2f, 0.8f, 0.2f);
            Gizmos.DrawSphere(transform.position, grabRange);
        }

        #endregion

        private void DetectInteractables()
        {
            // Find the closest interactable to the interactor.
            var hits = Physics.OverlapSphereNonAlloc(transform.position, grabRange, _overlapBuffer, interactableMask);
            IInteractable closest = null;
            var closestDist = float.MaxValue;

            for (int i = 0; i < hits; i++)
            {
                if (!_overlapBuffer[i].TryGetComponent(out IInteractable interactable)) continue;

                var dist = Vector3.Distance(transform.position, _overlapBuffer[i].transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = interactable;
                }
            }

            // if the closest interactor is not the one currently hovered. Process events and replace the current hover
            if (closest != _hovered)
            {
                HoverExit(_hovered);

                _hovered = closest;

                HoverEnter(_hovered);
            }
        }

        #region IInteractor

        public void HandleGrabInput()
        {
            if (_hovered == null) return;

            _selected = _hovered;
            Grab(_selected);
        }

        public void HandleReleaseInput()
        {
            if (_selected == null) return;

            Release(_selected);
            _selected = null;
        }

        public void HandleSelectInput()
        {
            if (_selected == null) return;

            Select(_selected);
        }

        public void HandleDeselectInput()
        {
            if (_selected == null) return;

            Deselect(_selected);
        }

        public void HoverEnter(IInteractable interactable)
        {
            interactable?.OnHoverEnter(this);
        }

        public void HoverExit(IInteractable interactable)
        {
            interactable?.OnHoverExit(this);
        }

        public void Grab(IInteractable interactable)
        {
            interactable?.OnGrab(this);
            interactable?.Attach(attachPoint);
        }

        public void Release(IInteractable interactable)
        {
            interactable?.OnRelease(this);
            interactable?.Detach();
        }

        public void Select(IInteractable interactable)
        {
            interactable?.OnSelect(this);
        }

        public void Deselect(IInteractable interactable)
        {
            interactable?.OnDeselect(this);
        }

        public Handedness GetHandedness()
        {
            return handedness;
        }

        #endregion
    }
}