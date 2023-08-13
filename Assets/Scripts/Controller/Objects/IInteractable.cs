using UnityEngine;

namespace Controller.Object
{
    public interface IInteractable
    {
        float radius { get; }
        Transform InteractablePosition { get; }
        void Interact();
        void HighLight();
        void DownLight();
        void TransitionHighLight();
    }
}
