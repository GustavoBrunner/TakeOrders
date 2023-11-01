using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Controller.Object;

namespace Controller
{
    public class MouseInfos : MonoBehaviour
    {
        public static Ray MousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }
        public static bool CheckMouseOverUi()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
        
    }
}