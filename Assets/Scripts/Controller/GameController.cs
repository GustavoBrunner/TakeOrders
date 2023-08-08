using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Phase;
using Controller.Observer;
namespace Controller
{
    public class GameController : Observable, IObserver
    {
        private static GameController _instance;
        public static GameController Instance { get => _instance; }

        public override void AddObserver(IObserver observer)
        {
            throw new System.NotImplementedException();
        }

        public void OnNotify<T>(NotificationTypes type, T value)
        {
            throw new System.NotImplementedException();
        }

        protected override void NotifyObservers<T>(NotificationTypes type, T value)
        {
            throw new System.NotImplementedException();
        }

        protected override void RemoveObserver(IObserver observer)
        {
            throw new System.NotImplementedException();
        }
    }
}