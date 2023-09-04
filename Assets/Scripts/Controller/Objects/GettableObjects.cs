using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Object
{
    public class GettableObjects : Objects, IInteractable, IGettable
    {
        public void Get()
        {
            Debug.Log("Pegando item");
            GameEvents.onGetItemTest.Invoke(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}
