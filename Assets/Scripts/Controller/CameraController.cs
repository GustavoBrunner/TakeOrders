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
        private void Awake()
        {
            camTf = GameObject.Find("Main Camera").GetComponent<Transform>();
            GameEvents.onShowFadeWall.AddListener(ShowWalls);
        }
        private void Start()
        {
            playerTransform = PlayerController.Instance._Tf;
        }
        private void Update()
        {
            dir = playerTransform.position - this.camTf.position;
            ray = new Ray(camTf.position, dir);
            
            //Debug.DrawRay(camTf.position, dir, Color.black, 1f);
            if(Physics.Raycast(ray, out hit))
            {
                var fader = hit.collider.GetComponent<FadeWall>();
                if (fader != null ) 
                {
                    fader.FadeOut();
                    fadeWalls.Add(fader);
                }
            }
        }
        private void ShowWalls()
        {
            if(fadeWalls.Count > 0)
            {
                StartCoroutine(ShowHidenWalls());
            }
        }
        IEnumerator ShowHidenWalls()
        {
            while (fadeWalls.Count != 0)
            {
                foreach (var wall in fadeWalls)
                {
                    wall?.FadeIn();
                    yield return new WaitForSeconds(0.1f);
                }
                fadeWalls.Clear();
                StopAllCoroutines();
                break;
            }
        }
    }
}