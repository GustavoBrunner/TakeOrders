using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller.Commands;
namespace Controller
{
    public class UiController : MonoBehaviour
    {
        private static UiController _instance;
        public static UiController Instance { get => _instance; }
        private List<Button> uiBtns;
        

        private void Awake()
        {
            CreateSingleton();
            uiBtns = new List<Button>();
        }
        private void Start()
        {
            uiBtns.AddRange(FindObjectsOfType<Button>());
            Debug.Log(uiBtns.Count);
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
        
    }
}