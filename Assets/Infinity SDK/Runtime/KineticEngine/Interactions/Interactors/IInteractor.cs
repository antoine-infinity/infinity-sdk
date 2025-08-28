namespace Infinity.Runtime.KineticEngine.Interactions
{
    public interface IInteractor
    {
        void HandleGrabInput();
        void HandleReleaseInput();
        void HandleSelectInput();
        void HandleDeselectInput();
        void HoverEnter(IInteractable interactable);
        void HoverExit(IInteractable interactable);
        void Grab(IInteractable interactable);
        void Release(IInteractable interactable);
        void Select(IInteractable interactable);
        void Deselect(IInteractable interactable);
        Handedness GetHandedness();
    }
}