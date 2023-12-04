using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Cams = new List<GameObject>();

        public CameraManager Instance;
        private void Awake()
        {
            if(Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        private void Start()
        {
        }
    }
}