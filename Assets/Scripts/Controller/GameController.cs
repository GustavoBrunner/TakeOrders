using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Phase;
using Controller.Observer;
using Controller.Object;
using System;

namespace Controller
{
    public class GameController : Observable, IObserver
    {
        private static GameController _instance;
        public static GameController Instance { get => _instance; }
        private GameObject player;

        protected override void Awake()
        {
            base.Awake();
            CreateSingleton();
            GetPrefabs();
        }
        public void OnNotify<T>(NotificationTypes type, T value)
        {
        }

        public override void AddObserver(IObserver observer)
        {
        }

        protected override void NotifyObservers<T>(NotificationTypes type, T value)
        {
        }

        protected override void RemoveObserver(IObserver observer)
        {
        }
        private void CreateSingleton()
        {
            if(_instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        private void GetPrefabs()
        {
            try
            {
                this.player = Resources.Load<GameObject>("Prefabs/Player");
                Debug.Log(player.name);
                PoolableController.AddPrefab(player);
            }
            catch (Exception e)
            {
                Debug.Log($"Error {e.Message}");
            }
        }
    }
}