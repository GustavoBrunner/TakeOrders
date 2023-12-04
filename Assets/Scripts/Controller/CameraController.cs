using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Object;
using UnityEngine.EventSystems;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        private Transform playerTransform;
        public Transform camTf { get; private set; }
        private Vector3 dir;
        private Ray ray;
        RaycastHit hit;
        private List<FadeWall> fadeWalls = new List<FadeWall>();
        private CinemachineFreeLook virtualCam;
        private bool isMousePressed;
        private void Awake()
        {
            camTf = GameObject.Find("Camera").GetComponent<Transform>();
            GameEvents.onShowFadeWall.AddListener(ShowWalls);
            virtualCam = GetComponent<CinemachineFreeLook>();
            isMousePressed = false;
            GameEvents.onUpdateCamPos.AddListener(UpdateCameraPosition);
        }
        protected virtual void Start()
        {
            playerTransform = PlayerController.Instance._Tf;
            virtualCam.Follow = playerTransform.transform;
            virtualCam.LookAt = playerTransform.transform;
        }
        protected virtual void Update()
        {
            dir = playerTransform.position - this.camTf.position;
            ray = new Ray(camTf.position, dir);
            
            Debug.DrawRay(camTf.position, dir, Color.black, 1f);
            if(Physics.Raycast(ray, out hit))
            {
                var fader = hit.collider.GetComponent<FadeWall>();
                if (fader != null ) 
                {
                    fader.FadeOut();
                    fadeWalls.Add(fader);
                }
                else
                {
                    ShowWalls();
                }
            }
            CheckIfMousePressed();
        }
        private void CheckIfMousePressed()
        {
            if (!GameController.Instance.isInCorridor)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    virtualCam.m_XAxis.m_InputAxisName = "Mouse X";
                    virtualCam.m_XAxis.m_MaxSpeed = 500f;
                }
                else if(Input.GetMouseButtonUp(1))
                {
                    virtualCam.m_XAxis.m_InputAxisName = "";
                    virtualCam.m_XAxis.m_MaxSpeed = 0f;
                }
            }
            
        }
        private void ShowWalls()
        {
            if(fadeWalls.Count > 0)
            {
                foreach (var wall in fadeWalls)
                {
                    wall?.FadeIn();
                }
                fadeWalls.Clear();
            }
        }
        private void UpdateCameraPosition(Vector3 newPos)
        {
            Debug.Log($"Position Updated to: {newPos}");
            this.camTf.position = newPos;
        }
    }
}