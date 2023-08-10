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
    }
}