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
        private RectTransform uiTransform;
        private int index;
        private Button nextButton 
        { 
            get 
            {
                if (index < uiBtns.Count)
                {
                    Debug.Log(index);
                    index++;
                }
                else
                    index = 0;
                return uiBtns[index];
            } 
        }

        private void Awake()
        {
            CreateSingleton();
            uiBtns = new List<Button>();
            index = 0;
        }
        private void Start()
        {
            uiBtns.AddRange(FindObjectsOfType<Button>());
            Debug.Log(uiBtns.Count);
            uiTransform = GameObject.Find("Panel").GetComponent<RectTransform>();
            CloseUi();
        }

        private void Update()
        {
            if (InputState.SelectChoice3)
            {
                OpenUi();
            }
            if (InputState.SelectChoice4)
            {
                CloseUi();
            }
            if(InputState.DownChoiceBtn || InputState.DownChoiceBtn2)
            {
                ChangeChoiceBtn();
            }
            if (InputState.UpChoiceBtn || InputState.UpChoiceBtn2)
            {
                ChangeChoiceBtn();
            }
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
        private void OpenUi()
        {
            uiTransform.localScale = Vector2.one;
            uiBtns[0].Select();
        }
        private void CloseUi()
        {
            uiTransform.localScale = Vector2.zero;
        }
        public void ChangeChoiceBtn()
        {
            if(index < uiBtns.Count)
            {
                nextButton?.Select();
            }
        }
        public void FirstOption()
        {
            Debug.Log("Primeira opção");
        }
        public void SecondOption()
        {
            Debug.Log("Segunda opção");
        }
        public void ThirdOption()
        {
            Debug.Log("Terceira opção");
        }
    }
}