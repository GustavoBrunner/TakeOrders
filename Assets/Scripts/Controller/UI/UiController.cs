using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller.Commands;
using Controller.Object;
using System.Linq;

namespace Controller
{
    public class UiController : MonoBehaviour
    {
        private static UiController _instance;
        public static UiController Instance { get => _instance; }

        private List<SlotUi> equipedItems = new List<SlotUi>();

        private void Awake()
        {
            CreateSingleton();
            equipedItems.AddRange(GameObject.Find("InventoryFix").GetComponentsInChildren<SlotUi>());
        }
        private void Start()
        {
            
        }

        private void Update()
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
        public void FillUiSlots(Sprite image)
        {
            var freeSlot = equipedItems.Select(fs => fs.GetComponent<SlotUi>())
                .Where(fs => !fs.isOccupied)
                .First();
            freeSlot.FillSlot(image);
        }
        public void ClearUiSlots(Sprite sprite)
        {
            foreach (var equipedItem in equipedItems)
            {
                if(equipedItem._image.sprite == sprite)
                {
                    equipedItem.ClearSlot();
                }
            }
        }
    }
}