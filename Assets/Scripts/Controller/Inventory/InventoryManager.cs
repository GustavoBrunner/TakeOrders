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
        private List<Objects> equipedItems = new List<Objects>();
        private Objects selectedObject;

        private Slot selectedSlot;
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
        }
        private void Start()
        {
            CloseInventory();
            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventory();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseInventory();
            }
        }
        private void GetItem(GameObject item)
        {
            var slots = inventorySlots.Select(i => i.GetComponent<Slot>())
                .Where(i => !i.IsOccupied)
                .First();
            Debug.Log(slots.name);
            var objects = item.GetComponent<Objects>();
            if(objects != null && !slots.IsOccupied)
            {

                slots.FillSlot(objects);
            }
        }

        private void OpenInventory()
        {
            tf.localScale = Vector2.one;
            this.inventoryOpened = true;
            GameController.Instance.isUiOpened = true;
        }
        public void CloseInventory()
        {
            tf.localScale = Vector2.zero;
            CloseDescription();
            CloseActionsWindow();
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
        private void CheckSlot(Objects item)
        {
            if(inventoryOpened)
            {
                selectedObject = item;
                OpenDescription();
                OpenEquipWindow();
                description.text = item.Description.Description;
                name.text = item.Description.Name;
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
                UiController.Instance.FillUiSlots(selectedObject.InventoryRender);
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
            equipedItems.Remove(selectedSlot.item);
            UiController.Instance.ClearUiSlots(selectedSlot.item.InventoryRender);
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