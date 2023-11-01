using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.Object
{
    [System.Serializable]
    public class Items
    {
        public string Name;
        [TextArea(3, 20)]
        public string Description;
        public Sprite ItemSprite;
    }
}
