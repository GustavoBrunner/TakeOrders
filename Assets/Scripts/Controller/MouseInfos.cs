using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class MouseInfos : MonoBehaviour
    {
        public static Ray MousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }
        public static PointerEventData PointEventData()
        {
            PointerEventData pointEventData = new PointerEventData(EventSystem.current);
            pointEventData.position = Input.mousePosition;
            return pointEventData;
        }
        public static bool IsMouseOverUi()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}