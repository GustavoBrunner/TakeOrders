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

        private List<GameObject> prefabs = new List<GameObject>();
        private RectTransform tf;
        private bool inventoryOpened;

        private GameObject objectDescriptions;
        private TMP_Text[] descriptionTxt;
        private TMP_Text description;
        private TMP_Text name;
        private GameObject actionsGo;

        private Objects selectedObject;

        private RaycastHit hit;
        private void Awake()
        {
            inventorySlots.AddRange(GameObject.Find("ItemSlots").GetComponentsInChildren<Slot>());
            utilitySlots.AddRange(GameObject.Find("UtilitySlots").GetComponentsInChildren<UtilitySlot>());
            actionsGo = GameObject.Find("Actions");

            tf = GameObject.Find("Render").GetComponent<RectTransform>();

            GameEvents.onGetItemTest.AddListener(GetItem);
            GameEvents.onShowDescription.AddListener(CheckSlot);

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
                OpenActionWindow();
                description.text = item.Description.Description;
                name.text = item.Description.Name;
            }
        }
        public void EquipItem()
        {
            var freeSlot = utilitySlots.Select(us => us.GetComponent<UtilitySlot>())
                .Where(us => !us.IsOccupied)
                .First();
            freeSlot.FillSlot(selectedObject);
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
    }
}