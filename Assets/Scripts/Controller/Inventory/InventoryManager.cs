using UnityEngine.EventSystems;
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
        private List<Slot> inventorySlots = new List<Slot>();
        private List<UtilitySlot> utilitySlots = new List<UtilitySlot>();
        private List<Objects> equipedObjects = new List<Objects>();
        private List<GameObject> prefabs = new List<GameObject>();
        private RectTransform tf;
        private bool inventoryOpened;

        private GameObject objectDescriptions;
        private TMP_Text[] descriptionTxt;
        private TMP_Text description;
        private TMP_Text name;
        private Transform[] actionsGo;
        private Transform _actionsGo;
        private Transform equipBtn;
        private Transform unequipBtn;

        private Objects selectedObject;

        private RaycastHit hit;

        private Slot selectedSlotObj;
        private void Awake()
        {
            inventorySlots.AddRange(GameObject.Find("ItemSlots").GetComponentsInChildren<Slot>());
            utilitySlots.AddRange(GameObject.Find("UtilitySlots").GetComponentsInChildren<UtilitySlot>());
            _actionsGo = GameObject.Find("Actions").transform;
            actionsGo = GameObject.Find("Actions").GetComponentsInChildren<Transform>();
            equipBtn = actionsGo.Select(eb => eb.GetComponent<Transform>())
                .Where(eb => eb.gameObject.name == "Equip").First();
            unequipBtn = actionsGo.Select(ub => ub.GetComponent<Transform>())
                .Where(ub => ub.gameObject.name == "Unequip").First();

            tf = GameObject.Find("Render").GetComponent<RectTransform>();

            GameEvents.onGetItemTest.AddListener(GetItem);
            GameEvents.onShowDescription.AddListener(CheckSlot);
            GameEvents.onUnequipItem.AddListener(GetSlot);

            objectDescriptions = GameObject.Find("Descriptions");
            descriptionTxt = GetComponentsInChildren<TMP_Text>();
            CloseDescription();

            description = this.descriptionTxt.Select(dt => dt.GetComponent<TMP_Text>())
                      .Where(dt => dt.text == "Description").First();
            name = this.descriptionTxt.Select(dt => dt.GetComponent<TMP_Text>())
                     .Where(dt => dt.text == "Name").First();
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
        }
        public void CloseInventory()
        {
            tf.localScale = Vector2.zero;
            CloseDescription();
            CloseActionsWindow();
            this.inventoryOpened = false;
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
                OpenActionWindowEquip();
                description.text = item.Description.Description;
                name.text = item.Description.Name;
            }
        }
        public void EquipItem()
        {
            if(utilitySlots.Count <= 4)
            {
                var freeSlot = utilitySlots.Select(us => us.GetComponent<UtilitySlot>())
                    .Where(us => !us.IsOccupied)
                    .First();
                freeSlot.FillSlot(selectedObject);
                AddItemToInventory(selectedObject);
                Debug.Log(equipedObjects.Count);
                CloseActionsWindow();
            }
            else
            {
                Debug.LogError("Capacidade máxima de itens equipáveis atingida");
            }
        }
        
        private void CloseActionsWindow()
        {
            _actionsGo.transform.localScale = Vector2.zero;
        }
        private void OpenActionWindowEquip()
        {
            _actionsGo.transform.localScale = Vector2.one;
            equipBtn.transform.localScale = Vector2.one;
            unequipBtn.transform.localScale = Vector2.zero;
        }
        public void OpenActionWindowUnequip()
        {
            _actionsGo.transform.localScale = Vector2.one;
            unequipBtn.transform.localScale = Vector2.one;
            equipBtn.transform.localScale = Vector2.zero;
        }
        private void AddItemToInventory(Objects obj)
        {
            this.equipedObjects.Add(obj);
            Debug.Log(equipedObjects.Count);
        }

        private void GetSlot(Slot slot)
        {
            selectedSlotObj = slot;
        }

        public void RemoveItemFromInventory()
        {
            selectedSlotObj.ClearSlot();
            equipedObjects.Remove(selectedSlotObj.item);
            Debug.Log(equipedObjects.Count);
            CloseActionsWindow();
        }
    }
}