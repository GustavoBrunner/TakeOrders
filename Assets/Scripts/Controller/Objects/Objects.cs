using Controller.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Object
{
    public class Objects : PhysicsController
    {
        public override void AddObserver(IObserver observer)
        {

        }

        protected override void NotifyObservers<T>(NotificationTypes type, T value)
        {

        }

        protected override void RemoveObserver(IObserver observer)
        {

        }
    }
}