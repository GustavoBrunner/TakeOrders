using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Controller
{

    public class TestEvent : UnityEvent { }
    public class GameEvents 
    {
        public static TestEvent onEventoParaTest = new TestEvent();
    }
}
