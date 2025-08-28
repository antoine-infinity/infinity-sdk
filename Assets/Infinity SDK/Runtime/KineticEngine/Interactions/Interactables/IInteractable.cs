using UnityEngine;

namespace Infinity.Runtime.KineticEngine.Interactions
{
    public interface IInteractable
    {
        void OnHoverEnter(IInteractor interactor);
        void OnHoverStay(IInteractor interactor);
        void OnHoverExit(IInteractor interactor);
        void OnGrab(IInteractor interactor);
        void OnRelease(IInteractor interactor);
        void OnSelect(IInteractor interactor);
        void OnDeselect(IInteractor interactor);

        void Attach(Transform attachPoint);
        void Detach();
    }
}