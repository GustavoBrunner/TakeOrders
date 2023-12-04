using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Controller.Phase;

namespace Controller.Object
{
    public class Doors : Objects, IInteractable
    {
        [SerializeField] private string NextScene;
        [SerializeField] private string LastScene;

        protected override void Awake()
        {
            base.Awake();
            
        }
        protected override void Start()
        {
            base.Start();
            isInteractive = true;
        }
        
    }
}
