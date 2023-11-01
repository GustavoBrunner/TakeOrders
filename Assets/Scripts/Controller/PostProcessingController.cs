using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Controller.Postprocessing
{
    public class PostProcessingController : MonoBehaviour
    {
        [Header("Componente de pós processamento")]
        [SerializeField] private Volume _PostProcessing;

        [Header("Perfil processamento sem 'rage'")]
        [SerializeField] private VolumeProfile _NoRageProfile;

        [Header("Perfil processamento com 'rage'")]
        [SerializeField] private VolumeProfile RageProfile;

        [SerializeField] private bool isInRage;

        [SerializeField] private Vignette _Vignette;

        [Header("Animator que controlará todas as animações de post-process")]
        [SerializeField] private Animator _Animator;
        private void Awake()
        {
            _PostProcessing = GetComponent<Volume>();
            _Animator = GetComponentInParent<Animator>();
            isInRage = false;
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                EnterRageProfile();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitRageProfile();
            }
        }
        public void EnterRageProfile()
        {
            isInRage = true;
            _PostProcessing.profile = RageProfile;
            _PostProcessing.profile.TryGet(out _Vignette);
            this._Animator.SetBool("StartAnimation", true);
        }
        public void ExitRageProfile()
        {
            isInRage = false;
            _PostProcessing.profile = _NoRageProfile;
            this._Animator.SetBool("StartAnimation", false);
        }
    }
}