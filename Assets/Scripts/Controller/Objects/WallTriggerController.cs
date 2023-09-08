using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Object
{
    public class WallTriggerController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            var col = collider.gameObject.GetComponent<PlayerController>();
            if(col != null)
            {
                GameEvents.onShowFadeWall.Invoke();
                Debug.Log("Player no trigger da cozinha");
            }
        }
    }
}