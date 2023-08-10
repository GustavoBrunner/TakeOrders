using Controller.Object;
using Controller.Observer;
using Controller.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class PlayerController : PhysicsController, IPoolable
    {
        private static PlayerController _instance;
        public static PlayerController Instance { get => _instance; }

        public GamePhases Phase => GamePhases.first;

        private NavMeshAgent agent;

        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
        }
        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0))
            {
                Move();
            }
        }
        public override void AddObserver(IObserver observer)
        {
            this.observers.Add(observer);
        }

        protected override void NotifyObservers<T>(NotificationTypes type, T value)
        {

        }

        protected override void RemoveObserver(IObserver observer)
        {
            this.observers.Remove(observer);
        }

        public void Move()
        {
            RaycastHit hit;
            if(Physics.Raycast(MouseInfos.MousePosition(), out hit))
            {
                this.agent?.SetDestination(hit.point);
                Debug.Log("Player movendo");
            }
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

        public void TurnOn()
        {
            this.gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            this.gameObject.SetActive(false);
        }
    }
}