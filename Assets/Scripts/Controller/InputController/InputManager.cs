using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Commands
{
    public class InputManager : MonoBehaviour
    {
        BaseCommand interaction;
        BaseCommand walk;
        CommandController cmdCtllr;
        private void Awake()
        {
            StartCommands();
        }
        void Update()
        {
            if (InputState.InteractionBtn)
            {
                cmdCtllr?.Walk();
                cmdCtllr?.Interact();
            }
        }
        private void StartCommands()
        {
            this.interaction = new InteractCommand(GameController.Instance.CheckInteractableObject, "Interacting");
            this.walk = new WalkCommand(PlayerController.Instance.Move, "Moving");
            cmdCtllr = new CommandController(this.interaction, this.walk);
        }
    }
}