using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller.Object;
using System.Linq;
namespace Controller
{
    public class Slot : MonoBehaviour
    {
        private bool _isOccupied;
        public bool IsOccupied { get => _isOccupied; }
        public Objects item { get; protected set; }
        private GameObject placeHolder;
        public Image PlaceHolder { get; protected set; }
        protected virtual void Awake()
        {
            this.PlaceHolder = GetComponent<Image>();
            this.PlaceHolder.transform.localScale = Vector2.zero;
            _isOccupied = false;
        }

        public void ClearSlot()
        {
            this._isOccupied = false;
            this.PlaceHolder.enabled = false;
            this.PlaceHolder.sprite = null;
            this.item = null;
        }
        public void FillSlot(Objects item)
        {
            Debug.Log("Dentro da função FillSlot");
            this._isOccupied = true;
            this.PlaceHolder.enabled = true;
            this.item = item;
            this.PlaceHolder.sprite = item.InventoryRender;
            this.PlaceHolder.transform.localScale = Vector2.one;
            Debug.Log(PlaceHolder.sprite);
        }
        public void ShowDescription()
        {
            if (this.IsOccupied)
            {
                GameEvents.onShowDescription.Invoke(this.item);
            }
        }
        public void SendSlot(Slot slot)
        {
            GameEvents.onUnequipItem.Invoke(slot);
            Debug.Log($"Item enviado {slot.item}");
        }
    }
}