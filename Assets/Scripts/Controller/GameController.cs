using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Phase;
using Controller.Observer;
using Controller.Object;
using System;
using System.Linq;


namespace Controller
{
    public class GameController : Observable, IObserver
    {
        private static GameController _instance;
        public static GameController Instance { get => _instance; }
        [SerializeField]
        private GameObject player;
        private RaycastHit hitInfo;
        public bool IsTestMode { get; private set; }

        private GameObject[] sceneInteractables;
        private IInteractable lastInteractable;

        
        protected override void Awake()
        {
            base.Awake();
            CreateSingleton();
            GetPrefabs();
            IsTestMode = true;
            PoolableController.TurnObjectsOn(GamePhases.first);
        }
        protected override void Start()
        {
            base.Start();
            this.sceneInteractables = GameObject.FindGameObjectsWithTag("Interactable");
        }
        protected override void Update()
        {
            base.Update();
            CheckInteractableObject();
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


                PoolableController.InstantiateObjects();
            }
            catch (Exception e)
            {
                Debug.Log($"Error: {e.Message}");
            }
        }
        //checa se o mouse est� em cima de um objeto interativo
        private void CheckInteractableObject()
        {
            try
            {
                if(Physics.Raycast(MouseInfos.MousePosition(), out hitInfo))
                {
                    var interactable = hitInfo.collider.GetComponent<IInteractable>();
                    //verifica se onde o mouse est� tem um objeto com componente interactable
                    if (interactable != null)
                    {
                        //se tiver, vai chamar a fun��o highlight do objeto
                        interactable?.HighLight();
                        //o objeto � guardado em uma vari�vel, vari�vel de apoio para poder desligar um objeto ap�s o mouse sair do item
                        lastInteractable = interactable;
                        if (Input.GetMouseButtonDown(0))
                        {
                            //checa se dist�ncia do player para o item � menor que 3
                            Debug.Log($"Distance {DistanceInteractablePlayer(interactable)}");
                            if(DistanceInteractablePlayer(interactable) < 3f)
                            {
                                //se for, chama a fun��o de intera��o do objeto
                                interactable.Interact();
                            }
                        }
                    }
                    else
                    {
                        //se n�o existir mais um objeto interativo onde o mouse t�,
                        //o objeto anterior, na vari�vel de apoio, � desligado
                        lastInteractable?.DownLight();
                        //E esse objeto recebe o valor nulo.
                        lastInteractable = null;
                    }
                }
                //momentaneamente est� como um input, mas posteriormente esses c�digos
                //ser�o chamados quando trocar o c�modo
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    //todos os objetos interativos ser�o armazenados num array de gameobjets
                    foreach (var interact in sceneInteractables)
                    {
                        //pegamos o componente Iinteractable
                        var interac = interact.GetComponent<IInteractable>();
                        Debug.Log($"iniciando transi��o de {interac}");
                        //E chamamos o m�todo de piscar o objeto
                        interac.TransitionHighLight();
                    }
                }

            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.Break();
            }
        }
        /// <summary>
        /// Fun��o que verifica a dist�ncia entre um objeto interativo, e o personagem
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        private float DistanceInteractablePlayer(IInteractable interactable)
        {
            var distance = PlayerController.Instance._Tf.position - interactable.InteractablePosition.position;
            var sqrDistance = distance.sqrMagnitude;

            return sqrDistance;
        }
    }
}