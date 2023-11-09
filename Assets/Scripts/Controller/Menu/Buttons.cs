using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class Buttons : MonoBehaviour
    {
        [Header("Nome da tela atual")]
        [SerializeField] private string m_ScreenName;
        private void Awake()
        {
            m_ScreenName = gameObject.name;
        }
        public void ButtonPressed()
        {
            MenuEvents.onChangeMenuScreen.Invoke(m_ScreenName);
        }
    }
}
