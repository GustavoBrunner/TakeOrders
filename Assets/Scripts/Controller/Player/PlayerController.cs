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
        public NavMeshAgent Agent { get => this.agent; }
        public Transform _Tf { get => this.Tf; }

        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
            CreateSingleton();
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
                Debug.Log($"Player movendo {this.agent.remainingDistance}");
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
        public void UpdateData(WorldCreationDTO dto)
        {
            transform.position = dto.PlayerPosition;
        }
        public float RemainingDistance()
        {
            var distance = Mathf.Abs(this.agent.remainingDistance);
            return distance;
        }
    }
}