using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class UiController : MonoBehaviour
    {
        private static UiController _instance;
        public static UiController Instance { get => _instance; }

        private void Awake()
        {
            CreateSingleton();
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
    }
}