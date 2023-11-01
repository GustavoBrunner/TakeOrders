using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Controller.Object;
using TMPro;
using System.Linq;

namespace Controller
{
    public class InventoryManager : MonoBehaviour
    {
        public List<Slot> inventorySlots = new List<Slot>();
        private List<UtilitySlot> utilitySlots = new List<UtilitySlot>();

        private List<GameObject> prefabs = new List<GameObject>();
        private RectTransform tf;
        private bool inventoryOpened;

        private GameObject objectDescriptions;
        private TMP_Text[] descriptionTxt;
        private TMP_Text description;
        private TMP_Text name;
        private GameObject actionsGo;
        private Transform[] actionsGos;
        private Transform equipTf;
        private Transform unequipTf;
        private List<Items> equipedItems = new List<Items>();
        private Items selectedObject;

        private Slot selectedSlot;

        private GameObject inventoryGo;

        
        private void Awake()
        {
            inventorySlots.AddRange(GameObject.Find("ItemSlots").GetComponentsInChildren<Slot>());
            utilitySlots.AddRange(GameObject.Find("UtilitySlots").GetComponentsInChildren<UtilitySlot>());
            actionsGo = GameObject.Find("Actions");
            actionsGos = GameObject.Find("Actions").GetComponentsInChildren<Transform>();
            equipTf = actionsGos.Select(ag => ag.GetComponent<Transform>())
                .Where(ag => ag.gameObject.name == "Equip")
                .First();
            unequipTf = actionsGos.Select(ag => ag.GetComponent<Transform>())
                .Where(ag => ag.gameObject.name == "Unequip")
                .First();

            tf = GameObject.Find("Render").GetComponent<RectTransform>();

            GameEvents.onGetItemTest.AddListener(GetItem);
            GameEvents.onShowDescription.AddListener(CheckSlot);
            GameEvents.onSelectUtilitySlot.AddListener(GetSlot);

            objectDescriptions = GameObject.Find("Descriptions");
            descriptionTxt = GetComponentsInChildren<TMP_Text>();
            CloseDescription();

            description = this.descriptionTxt.Select(dt => dt.GetComponent<TMP_Text>())
                      .Where(dt => dt.text == "Description").First();
            name = this.descriptionTxt.Select(dt => dt.GetComponent<TMP_Text>())
                     .Where(dt => dt.text == "Name").First();

            inventoryOpened = false;
            inventoryGo = GameObject.Find("Render");
        }
        private void Start()
        {
            CloseInventory();
            
        }
        private void Update()
        {
            
        }
        private void GetItem(Items item)
        {
            var slots = inventorySlots.Select(i => i.GetComponent<Slot>())
                .Where(i => !i.IsOccupied)
                .First();
            Debug.Log(slots.name);
            if(item != null && !slots.IsOccupied)
            {
                slots.FillSlot(item);
                return;
            }
        }

        public void OpenInventory()
        {
            if (!inventoryOpened && !GameController.Instance.sayDialogue.activeSelf)
            {
                inventoryGo.SetActive(true);
                this.inventoryOpened = true;
                CloseActionsWindow();
                GameController.Instance.isUiOpened = true;
                GameEvents.onTurnInventoryIconOn.Invoke(false);
            }
            else
            {
                CloseInventory();
                GameEvents.onTurnInventoryIconOn.Invoke(true);
            }
        }
        public void CloseInventory()
        {
            inventoryGo.SetActive(false);
            this.inventoryOpened = false;
            GameController.Instance.isUiOpened = false;
        }
        private void OpenDescription()
        {
            objectDescriptions.transform.localScale = Vector2.one;
        }
        private void CloseDescription()
        {
            objectDescriptions.transform.localScale = Vector2.zero;
        }
        private void CheckSlot(Items item)
        {
            if(inventoryOpened)
            {
                selectedObject = item;
                OpenDescription();
                OpenEquipWindow();
                description.text = item.Description;
                name.text = item.Name;
            }
        }
        public void EquipItem()
        {
            if (!equipedItems.Contains(selectedObject))
            {
                var freeSlot = utilitySlots.Select(us => us.GetComponent<UtilitySlot>())
                    .Where(us => !us.IsOccupied)
                    .First();
                freeSlot.FillSlot(selectedObject);
                equipedItems.Add(selectedObject);
                GameController.Instance.ChangeFlowchartBool("Equip"+selectedObject.Name.Replace(" ", ""), true);
                //Debug.Log(selectedObject.gameObject.name);
                UiController.Instance.FillUiSlots(selectedObject.ItemSprite);
                Debug.Log($"Itens equipados {equipedItems.Count}");
            }
            CloseActionsWindow();
        }
        private void CloseActionsWindow()
        {
            actionsGo.transform.localScale = Vector2.zero;
        }
        private void OpenActionWindow()
        {
            actionsGo.transform.localScale = Vector2.one;
        }
        public void OpenUnequipWindow()
        {
            OpenActionWindow();
            unequipTf.localScale = Vector2.one;
            CloseEquipWindow();
        }
        private void CloseUnequipWindow()
        {
            unequipTf.localScale = Vector2.zero;
        }
        public void OpenEquipWindow()
        {
            OpenActionWindow();
            CloseUnequipWindow();
            equipTf.localScale = Vector2.one;
        }
        private void CloseEquipWindow()
        {
            equipTf.localScale = Vector2.zero;
        } 
        public void UnequipItem()
        {
            GameController.Instance.ChangeFlowchartBool("Equip" + selectedObject.Name.Replace(" ", ""), false);
            equipedItems.Remove(selectedSlot.item);
            UiController.Instance.ClearUiSlots(selectedSlot.item.ItemSprite);
            this.selectedSlot.ClearSlot();
            Debug.Log($"Itens equipados {equipedItems.Count}");
            this.selectedSlot = null;
            CloseActionsWindow();
        }
        private void GetSlot(Slot slot)
        {
            this.selectedSlot = slot;
        }   
    }
}