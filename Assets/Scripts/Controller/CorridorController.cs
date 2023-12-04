using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

namespace Controller
{
    public class CorridorController : MonoBehaviour
    {
        [SerializeField] private Flowchart CorridorFlowChart;
        [SerializeField] private GameObject CorridorSayDialog;


        private void Start()
        {
            GameController.Instance.ItemFlowChart = this.CorridorFlowChart;
            GameController.Instance.sayDialogue = this.CorridorSayDialog;
        }
    }
}