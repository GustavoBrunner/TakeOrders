using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Object;

namespace Controller.Commands
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance { get => _instance; }
        BaseCommand interaction;
        BaseCommand walk;

        CommandController cmdCtllr;

        Queue<BaseCommand> commandList;

        public IInteractable interactable;
        private void Awake()
        {
            CreateSingleton();
            commandList = new Queue<BaseCommand>();
            StartCommands();
        }
        void Update()
        {
            if (InputState.InteractionBtn)
            {
                cmdCtllr?.Walk();
                //interactable = null;
            }
            if(interactable != null && GameController.Instance.DistanceInteractablePlayer(interactable) < 2f)
            {
                interactable.Interact();
                interactable = null;
            }
        }
        private void StartCommands()
        {
            //this.interaction = new InteractCommand(interactable.Interact, "Interacting");
            this.walk = new WalkCommand(PlayerController.Instance.Move, "Moving");
            cmdCtllr = new CommandController(this.interaction, this.walk);
        }
        private void AddCommandToList(BaseCommand command)
        {
            commandList.Enqueue(command);
        }
        private void CreateSingleton()
        {
            if(_instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}