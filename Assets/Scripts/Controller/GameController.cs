using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Phase;
using Controller.Observer;
using Controller.Object;
using System;
using System.Linq;
using Controller.Commands;
using Fungus;

namespace Controller
{
    public class GameController : Observable, IObserver
    {
        private List<Flowchart> gameFlowCharts = new List<Flowchart>();
        public Flowchart ItemFlowChart { get; private set; }
        public Flowchart FirstPhaseObjFlowchart { get; private set; }
        private static GameController _instance;
        public static GameController Instance { get => _instance; }
        [SerializeField]
        private GameObject player;
        private RaycastHit hitInfo;
        public bool IsTestMode { get; private set; }

        private IInteractable[] sceneInteractables;
        private IInteractable lastInteractable;

        private InputController inpCtllr;

        
        public int Tendency { get; private set; }

        public GameObject sayDialogue;

        public bool _isUiOpened { get; private set; }
        public bool isUiOpened 
        { get => _isUiOpened;
            set {
                if (!value)
                {
                    GameEvents.onLetPlayerMove.Invoke(true);
                }
                else
                {
                    GameEvents.onLetPlayerMove.Invoke(false);
                }
                _isUiOpened = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            CreateSingleton();
            GetPrefabs();
            IsTestMode = true;
            PoolableController.TurnObjectsOn(GamePhases.first);
            isUiOpened = false;
            LoadFirstScene();
            Debug.Log(gameFlowCharts.Count);
            sayDialogue = FindObjectOfType<SayDialog>().gameObject;
        }
        protected override void Start()
        {
            base.Start();
            GetSceneInteractables();
            inpCtllr = this.gameObject.AddComponent<InputController>();
            this.gameObject.AddComponent<InputManager>();
            GetFlowcharts();
        }
        protected override void Update()
        {
            base.Update();
            CheckInteractableObject();
        }
        public void SubTendency(int tend)
        {
            this.Tendency-= tend;
            this.ItemFlowChart.SetIntegerVariable("Tendency", this.Tendency);
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
        //criar o singleton do gamecontroller
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
        //Função para pegar os prefabs na pasta resources
        private void GetPrefabs()
        {
            try
            {
                this.player = Resources.Load<GameObject>("Prefabs/Player");
                Debug.Log(player.name);
                PoolableController.AddPrefab(player);


                //PoolableController.InstantiateObjects();
            }
            catch (Exception e)
            {
                Debug.Log($"Error: {e.Message}");
            }
        }
        //checa se o mouse está em cima de um objeto interativo
        public void CheckInteractableObject()
        {
            try
            {
                if(Physics.Raycast(MouseInfos.MousePosition(), out hitInfo))
                {
                    var interactable = hitInfo.collider.GetComponent<IInteractable>();
                    //verifica se onde o mouse está tem um objeto com componente interactable
                    
                    if (interactable != null)
                    {
                        //se tiver, vai chamar a função highlight do objeto
                        interactable?.HighLight();
                        //o objeto é guardado em uma variável, variável de apoio para poder desligar um objeto após o mouse sair do item
                        lastInteractable = interactable;
                        
                        if (InputState.InteractionBtn)
                        {
                            
                            InputManager.Instance.interactable = lastInteractable;
                        }
                    }
                    else
                    {
                        //se não existir mais um objeto interativo onde o mouse tá,
                        //o objeto anterior, na variável de apoio, é desligado
                        lastInteractable?.DownLight();
                        //E esse objeto recebe o valor nulo.
                        lastInteractable = null;
                    }
                }
                //momentaneamente está como um input, mas posteriormente esses códigos
                //serão chamados quando trocar o cômodo
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    //todos os objetos interativos serão armazenados num array de gameobjets
                    foreach (var interact in sceneInteractables)
                    {
                        //E chamamos o método de piscar o objeto
                        if(interact.isInteractive)
                            interact.TransitionHighLight();
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
        /// Função que verifica a distância entre um objeto interativo, e o personagem
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        public float DistanceInteractablePlayer(IInteractable interactable)
        {
            var distance = PlayerController.Instance._Tf.position - interactable.InteractablePosition.position;
            var sqrDistance = distance.sqrMagnitude;

            return sqrDistance;
        }
        private void GetSceneInteractables()
        {
            this.sceneInteractables = FindObjectsOfType<Objects>();
            foreach (var interactable in sceneInteractables)
            {
                if (interactable.Name != "Aparador")
                {
                    interactable.isInteractive = false;
                }
                else
                    interactable.isInteractive = true;
            }
        }
        public void TurnInteractablesOn()
        {
            foreach (var interactable in sceneInteractables)
            {
                interactable.isInteractive = true;
            }
        }
        private void GetFlowcharts()
        {
            gameFlowCharts.AddRange(FindObjectsOfType<Flowchart>());
            ItemFlowChart = gameFlowCharts.Select(ifc => ifc.GetComponent<Flowchart>())
                .Where(ifc => ifc.gameObject.name == "ItemFlowchart").First();
            FirstPhaseObjFlowchart = gameFlowCharts.Select(ifc => ifc.GetComponent<Flowchart>())
                .Where(ifc => ifc.gameObject.name == "ObjectsFirstPhaseFlowchart").First();
        }
        public void ChangeFlowchartBool(string name, bool value)
        {
            ItemFlowChart.SetBooleanVariable(name, value);
        }
        public void ActivateRedDoorObject()
        {
            foreach (var interac in sceneInteractables)
            {
                if(interac.Name == "EstanteLivrosComPortinhaPorta1")
                {
                    interac.TurnOnInteraction();
                    Debug.Log("Estante ativada");
                }
            }
        }
        public void LoadFirstScene()
        {
            SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        }

        public void PickupItem(string itemName, string interactableName)
        {
            foreach ( var interactable in sceneInteractables )
            {
                if( interactable.Name == interactableName )
                {
                    foreach (var item in interactable.GetItems())
                    {
                        if(item.Name == itemName)
                        {
                            GameEvents.onGetItemTest.Invoke(item);
                            Debug.Log("Item existe");
                        }
                    }
                }
            }
        }
    }
}