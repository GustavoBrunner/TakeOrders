using Controller.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.Object
{
    public class Objects : TransformController, IInteractable
    {
        protected Outline Outline;
        private bool mouseIn;

        public Transform InteractablePosition => this.transform;

        public float radius => 2f;

        [SerializeField]
        public bool isInteractive { get; set; }

        public string Name => this.gameObject.name;

        public Sprite InventoryRender;
        public DescriptionItem Description;

        public List<Items> Itens = new List<Items>();
        protected override void Awake()
        {
            base.Awake();
            Outline = gameObject.AddComponent<Outline>();
            Outline.OutlineWidth = 0f;
            Outline.OutlineColor = Color.cyan;
            gameObject.tag = "Interactable";
        }
        private void OnDrawGizmos()
        {
            //Gizmos.DrawWireSphere(this.transform.position, this.radius);
        }
        protected override void Start()
        {
            base.Start();
            
        }
        protected override void Update()
        {
            base.Update();
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

        public void Interact()
        {
            if (this.isInteractive)
            {
                Debug.Log("Interagindo");
                //chamará a função no UiController que mostrará as opções de interação
                //PlayerController.Instance.CheckCanMove(false);
                GameController.Instance.isUiOpened = true;
                GameController.Instance.ItemFlowChart.SetStringVariable("InteractedObjName", this.Name);
                Debug.Log(GameController.Instance.ItemFlowChart.GetGameObjectVariable("InteractedObjName"));
                GameController.Instance.ItemFlowChart.ExecuteBlock("ObjChecker");
            }
        }

        public void HighLight()
        {
            if(this.isInteractive)
                StartCoroutine(OutLineHighLight(0.1f));
            //Debug.Log("Ligando outline");
        }

        public void DownLight()
        {
            if (this.gameObject.activeSelf)
            {
                StartCoroutine(OutLineDownLight(0.6f));
            }
            //Debug.Log("Desligando outline");
        }
        private IEnumerator OutLineHighLight(float speed)
        {
            mouseIn = true;
            while (mouseIn)
            {
                if(Outline.OutlineWidth < 8f)
                {
                    Outline.OutlineWidth += speed;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        IEnumerator OutLineDownLight(float speed)
        {
            mouseIn = false;
            while (!mouseIn)
            {
                if(Outline.OutlineWidth > 0f)
                {
                    Outline.OutlineWidth -= speed;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        IEnumerator StartTransition()
        {
            StartCoroutine(OutLineHighLight(0.8f));
            yield return new WaitForSeconds(3f);
            StartCoroutine(OutLineDownLight(0.8f));
        }
        public void TransitionHighLight()
        {
            Debug.Log("Transição de cenas");
            StartCoroutine(StartTransition());
        }

        public void TurnOnInteraction()
        {
            this.isInteractive = true; ;
        }
        public void TurnOffInteraction()
        {
            this.isInteractive = false;
        }
        public List<Items> GetItems()
        {
            return this.Itens;
        }
    }
}