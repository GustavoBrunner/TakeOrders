using UnityEngine;

namespace Controller.Object
{
    public interface IInteractable
    {
        float radius { get; }
        bool isInteractive { get; set; }
        string Name { get; }
        Transform InteractablePosition { get; }
        void Interact();
        void HighLight();
        void DownLight();
        void TransitionHighLight();
        void TurnOnInteraction();
    }
}
