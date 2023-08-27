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
        private void Awake()
        {
            PlaceHolder = GetComponent<Image>();
            PlaceHolder.transform.localScale = Vector2.zero;
            _isOccupied = false;
        }

        public void ClearSlot()
        {
            this._isOccupied = false;
            PlaceHolder = null;
        }
        public void FillSlot(Objects item)
        {
            Debug.Log("Dentro da função FillSlot");
            this._isOccupied = true;
            this.item = item;
            PlaceHolder.sprite = item.InventoryRender;
            PlaceHolder.transform.localScale = Vector2.one;
            Debug.Log(PlaceHolder.sprite);
        }
        public void ShowDescription()
        {
            if (this.IsOccupied)
            {
                GameEvents.onShowDescription.Invoke(this.item);
            }
        }
    }
}