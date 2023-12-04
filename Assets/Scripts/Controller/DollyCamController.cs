using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Controller
{

    public class DollyCamController : CameraController
    {
        private CinemachineVirtualCamera cam;
        protected override void Start()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            Camera.SetupCurrent(this.GetComponent<Camera>());
        }
        protected override void Update()
        {
            
        }
    }
}
