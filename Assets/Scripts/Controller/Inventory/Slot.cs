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
        public Items item { get; protected set; }
        private GameObject placeHolder;
        private Sprite originalSprite;
        public Image PlaceHolder { get; protected set; }
        private void Awake()
        {
            PlaceHolder = GetComponent<Image>();
            originalSprite = PlaceHolder.sprite;
            _isOccupied = false;
        }

        public void ClearSlot()
        {
            //enabled = false;
            this._isOccupied = false;
            this.PlaceHolder.sprite = originalSprite;
            this.item = null;
        }
        public void FillSlot(Items item)
        {
            Debug.Log("Dentro da função FillSlot");
            //this.PlaceHolder.enabled = true;
            this._isOccupied = true;
            this.item = item;
            PlaceHolder.sprite = item.ItemSprite;
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
            GameEvents.onSelectUtilitySlot.Invoke(slot);
        }
    }
}