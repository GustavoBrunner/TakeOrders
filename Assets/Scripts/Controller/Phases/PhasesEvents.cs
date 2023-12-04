using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Controller.Phase
{
    public class ChangePlayerPosition : UnityEvent<string> { } 
    public class PhasesEvents : MonoBehaviour
    {
        public static ChangePlayerPosition onChangePlayerPosition = new ChangePlayerPosition();
    }
}