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
        public Flowchart ItemFlowChart { get; set; }
        public Flowchart FirstPhaseObjFlowchart { get; private set; }
        private static GameController _instance;
        public static GameController Instance { get => _instance; }
        [SerializeField]
        private GameObject player;
        private RaycastHit hitInfo;
        public bool IsTestMode { get; private set; }

        public IInteractable[] sceneInteractables;
        private IInteractable lastInteractable;

        private InputController inpCtllr;

        public Vector3[] CamPositions;
        public int Tendency { get; private set; }

        public GameObject sayDialogue;

        public bool isInCorridor;

        [SerializeField] private bool _isUiOpened;
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
        [SerializeField]
        private int interactableLayer;
        [SerializeField]
        private bool testPhase;
        protected override void Awake()
        {
            base.Awake();
            
            CreateSingleton();
            GetPrefabs();
            IsTestMode = true;
            PoolableController.TurnObjectsOn(GamePhases.first);
            //LoadFirstScene();
            Debug.Log(gameFlowCharts.Count);

            isInCorridor = false;
            interactableLayer = 1 << LayerMask.NameToLayer("Interactables");
            testPhase = true;
            GameEvents.onUpdateCamPos.Invoke(CamPositions[0]);
        }
        protected override void Start()
        {
            base.Start();
            GetSceneInteractables();
            inpCtllr = this.gameObject.AddComponent<InputController>();
            this.gameObject.AddComponent<InputManager>();
            GetFlowcharts();
            sayDialogue = FindObjectOfType<SayDialog>().gameObject;
            GameEvents.onChangeCam.Invoke("RoomKitchen");
            
            _isUiOpened = true;
        }
        protected override void Update()
        {
            base.Update();
            CheckInteractableObject();
            if (testPhase && Input.GetKeyDown(KeyCode.DownArrow))
                TurnInteractablesOn();
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            }

        }
        

        public void TurnBlackEffect()
        {
            GameEvents.onTurnBlackEffect.Invoke(true);
        }
        public void TurnBlackEffectOff()
        {
            GameEvents.onTurnBlackEffect.Invoke(false);
        }

        public void TurnWhiteEffect()
        {
            GameEvents.onTurnWhiteEffect.Invoke(true);

        }
        public void TurnWhiteEffectOff()
        {
            GameEvents.onTurnWhiteEffect.Invoke(false);

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
            }
        }
        public void UiOpened(bool b)
        {
            this.isUiOpened = b;
        }
        //Fun��o para pegar os prefabs na pasta resources
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
        //checa se o mouse est� em cima de um objeto interativo
        public void CheckInteractableObject()
        {
            //try
            //{
                if (Physics.Raycast(MouseInfos.MousePosition(), out hitInfo, Mathf.Infinity, interactableLayer ) && !isUiOpened)
                {
                    var interactable = hitInfo.collider.GetComponent<IInteractable>();
                    //verifica se onde o mouse est� tem um objeto com componente interactable
                    //Debug.Log(interactable.Name);
                    
                    if (interactable != null)
                    {
                        //se tiver, vai chamar a fun��o highlight do objeto
                        //o objeto � guardado em uma vari�vel, vari�vel de apoio para poder desligar um objeto ap�s o mouse sair do item
                        interactable?.HighLight();
                        lastInteractable = interactable;
                        if (InputState.InteractionBtn)
                        {
                            InputManager.Instance.interactable = lastInteractable;
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
                //momentaneamente est� como um input, mas posteriormente esses c�digos
                //ser�o chamados quando trocar o c�modo
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    //todos os objetos interativos ser�o armazenados num array de gameobjets
                    foreach (var interact in sceneInteractables)
                    {
                        //E chamamos o m�todo de piscar o objeto
                        if(interact.isInteractive)
                            interact?.TransitionHighLight();
                    }
                }
            //}
            //catch (Exception e)
            //{
            //    Debug.Log(e.Message);
            //    Debug.Break();
            //}
        }
        /// <summary>
        /// Fun��o que verifica a dist�ncia entre um objeto interativo, e o personagem
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
                    interactable.isInteractive = false;
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
        public void TurnInteractablesOff()
        {
            foreach (var interactable in sceneInteractables)
            {
                interactable.isInteractive = false;
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
        public void ActivateObjectInteraction(string objName, bool b)
        {
            foreach (var interac in sceneInteractables)
            {
                if(interac.Name == objName)
                {
                    if(b)
                    {
                        interac.TurnOnInteraction();
                        Debug.Log("Objeto ativado");
                    }
                    else
                    {
                        interac.TurnOffInteraction();
                        Debug.Log("Objeto desativado");
                    }
                }
            }
        }
        public void DestroyInteractable(string objName)
        {
            foreach (var interac in sceneInteractables)
            {
                if (interac.Name == objName)
                {
                    interac.SetGoActive(false);
                }
            }
        }
        public void LoadCorridorScene()
        {
            SceneManager.LoadSceneAsync("CORREDOR");
            isInCorridor = true;
            GameEvents.onUpdateCamPos.Invoke(CamPositions[1]);
            TurnWhiteEffectOff();
        }
        public void LoadBedroomScene()
        {
            isInCorridor = false;
            SceneManager.LoadSceneAsync("Quarto");
            GameEvents.onUpdateCamPos.Invoke(CamPositions[3]);
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene("MenuScreen");
            
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