using System.Collections.Generic;
using System.Linq;
using Infinity.Runtime.Core.Logging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager Instance;
        
        private HashSet<IInteractor> _interactors;

        private HashSet<IInteractor> LeftHandedInteractors =>
            _interactors.Where(x => x.GetHandedness() == Handedness.Left).ToHashSet();
        private HashSet<IInteractor> RightHandedInteractors =>
            _interactors.Where(x => x.GetHandedness() == Handedness.Right).ToHashSet();
        
        [Header("Left Hand Action References")] 
        [SerializeField] private InputActionReference leftGrab;
        [SerializeField] private InputActionReference leftTrigger;
        [SerializeField] private float leftGrabThreshold = 0.5f;
        [SerializeField] private float leftTriggerThreshold = 0.5f;
        [Header("Right Hand Action References")]
        [SerializeField] private InputActionReference rightGrab;
        [SerializeField] private InputActionReference rightTrigger;
        [SerializeField] private float rightGrabThreshold = 0.5f;
        [SerializeField] private float rightTriggerThreshold = 0.5f;
        
        private void Awake()
        {
            Instance = this;
            CollectInteractors();
            
            // TODO: Handle Inputs and Grab/Release Requests.
        }
        
        private void CollectInteractors()
        {
            _interactors = transform.GetComponentsInChildren<IInteractor>(true).ToHashSet();
            InfinityLog.Info(this, $"Found {_interactors.Count} interactors.");
        }

        private void HandleInputs()
        {
            if (leftGrab?.action.ReadValue<float>() >= leftGrabThreshold)
            {
                foreach (var leftInteractor in LeftHandedInteractors)
                {
                    leftInteractor.HandleGrabInput();
                }
            }

            if (rightGrab?.action.ReadValue<float>() >= rightGrabThreshold)
            {
                foreach (var rightInteractor in RightHandedInteractors)
                {
                    rightInteractor.HandleGrabInput();
                }
            }
        }
    }
}