using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Phase;

namespace Controller.Object
{
    public interface IPoolable
    {
        GamePhases Phase { get; }

        void TurnOn();
        void TurnOff();
    }
}