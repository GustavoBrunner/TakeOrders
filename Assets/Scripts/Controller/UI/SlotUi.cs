using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class SlotUi : MonoBehaviour
    {
        public Sprite image { get; protected set; }
        public Image _image { get; protected set; }

        public bool isOccupied { get; private set; }

        private void Awake()
        {
            _image = GetComponent<Image>();
            isOccupied = false;
        }

        public void FillSlot(Sprite image)
        {
            this._image.sprite = image;
            isOccupied = true;
        }
        public void ClearSlot()
        {
            this._image.sprite = null;
            isOccupied = false;
        }

    }
}