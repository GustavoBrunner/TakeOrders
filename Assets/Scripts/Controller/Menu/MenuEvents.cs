using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Menu
{

    public class ChangeMenuScreen : UnityEvent<string> { }

    public class MenuEvents : MonoBehaviour
    {
        public static ChangeMenuScreen onChangeMenuScreen = new ChangeMenuScreen();
    }
}