using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Controller.Postprocessing
{
    public class PostProcessingController : MonoBehaviour
    {
        public static PostProcessingController PPController { get; private set; }
        
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
            if(PPController != null)
            {
                DestroyImmediate(gameObject.transform.parent);
            }
            else
            {
                PPController = this;
                DontDestroyOnLoad(gameObject.transform.parent);
            }
            GameEvents.onTurnBlackEffect.AddListener(BlackEffect);
            GameEvents.onTurnWhiteEffect.AddListener(WhiteEffect);
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    WhiteEffect(true);
            //}
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    WhiteEffect(false);
            //}
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
        public void BlackEffect(bool status)
        {
            this._Animator.SetBool("BlackEffect", status);
        }
        public void WhiteEffect(bool status)
        {
            this._Animator.SetBool("WhiteEffect", status);
        }
        private void BlackEffectOff()
        {
            this._Animator.SetBool("BlackEffect", false);
        }
        private void WhiteEffectOff()
        {
            this._Animator.SetBool("WhiteEffect", false);
        }
        public void BlackEffectBedroom(bool status)
        {
            this._Animator.SetBool("BedroomEntered", status);
        }
        public void FinalWhiteEffectBedroom(bool status)
        {
            this._Animator.SetBool("FinalGame", status);
        }
    }
}