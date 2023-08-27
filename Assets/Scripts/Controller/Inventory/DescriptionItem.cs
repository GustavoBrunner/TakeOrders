using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.Object
{
    [System.Serializable]
    public class DescriptionItem 
    {
        [TextArea(3,20)]
        public string Description;
        public Sprite Sprite;
        public string Name;
    }
}