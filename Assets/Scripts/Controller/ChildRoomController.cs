using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Controller.Object;

namespace Controller
{
    public class ChildRoomController : MonoBehaviour
    {
        [SerializeField] private Flowchart ChildRoomFlowChart;
        [SerializeField] private GameObject ChildRoomSayDialog;
        [SerializeField] private IInteractable[] SceneInteractable;
        private void Awake()
        {
            this.SceneInteractable = FindObjectsOfType<Objects>();
        }
        private void Start()
        {
            GameController.Instance.ItemFlowChart = ChildRoomFlowChart;
            GameController.Instance.sayDialogue = ChildRoomSayDialog;
            GameController.Instance.sceneInteractables = this.SceneInteractable;
            GameController.Instance.TurnInteractablesOn();
        }
        public void LetPlayerMove()
        {
            GameEvents.onTurnMovementOn.Invoke();
        }
        public void LoadMenu()
        {
            GameController.Instance.LoadMenu();
        }
    }
}