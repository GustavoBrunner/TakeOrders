using Cinemachine;
using Controller.Object;
using Controller.Observer;
using Controller.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Controller
{
    public class PlayerController : PhysicsController, IPoolable
    {
        private static PlayerController _instance;
        private CameraController cam;

        public static PlayerController Instance { get => _instance; }
        public GamePhases Phase => GamePhases.first;

        private NavMeshAgent agent;
        public NavMeshAgent Agent { get => this.agent; }
        public Transform _Tf { get => this.Tf; }

        private bool canMove;

        
        public int walkIgnoreLayer;
        protected override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
            CreateSingleton();
            canMove = true;
            cam = FindObjectOfType<CameraController>();
            this.agent.stoppingDistance = 0.6f;
            walkIgnoreLayer = 1 << LayerMask.NameToLayer("Floor");
            GameEvents.onLetPlayerMove.AddListener(CheckCanMove);
        }
        protected override void Update()
        {
            base.Update();
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
            /**
             * Para excluir, ou detectar somente uma camada do raycast, precisa declarar ela como int, 
             * depois passar o valor 1 << layermask.NameToLayer("Nome da Camada"). Também podemos passar mais camadas ao mesmo tempo, 
             * repetindo esse processo de 1 << layermask.NameToLayer("Nome da Camada")
             * usar o Mathf.Infinity para a distância máxima, e deixar isso tudo em um if. As filtragens de camada, se necessário, 
             * podem ser feitas nos ifs
             */
            if (Physics.Raycast(cam.camTf.position, MouseInfos.MousePosition().direction,out hit, Mathf.Infinity, walkIgnoreLayer))
            {
                if(canMove)
                {
                    this.agent.SetDestination(hit.point);
                }
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
        public void CheckCanMove(bool b)
        {
            this.canMove = b;
        }
        public void LetMove()
        {
            this.canMove = true;
            Debug.Log(canMove);
        }
        public void StopPlayerMovement()
        {

        }
        //IEnumerator ChangeMoveState()
        //{
        //    agent.stoppingDistance
        //    yield return new WaitForSeconds(2f);
        //}
    }
}