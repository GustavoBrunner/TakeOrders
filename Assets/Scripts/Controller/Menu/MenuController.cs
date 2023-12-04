using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Controller;
using Controller.Postprocessing;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]private GameObject initialMenu, optionsMenu, creditMenu, pauseMenu, CreditInGameMenu;

        [SerializeField]private List<GameObject> screens = new List<GameObject>();
        private GameObject actualOpenedScreen;
        
        [SerializeField]private bool m_gameStarted;
        
        private void Awake()
        {
            GetMenus();
            
                
            optionsMenu?.SetActive(false);
            creditMenu?.SetActive(false);
        }
        private void Start()
        {
            MenuEvents.onChangeMenuScreen.AddListener(OpenNewScreen);
        }
        private void Update()
        {
            if (m_gameStarted)
                if (Input.GetKeyDown(KeyCode.Escape))
                    OpenPauseMenu();
        }
        private void GetMenus()
        {
            initialMenu = GameObject.Find("InitialMenu");
            optionsMenu = GameObject.Find("OptionsMenu");
            creditMenu = GameObject.Find("CreditsMenu");
            if(!m_gameStarted)
            {
                screens.Add(optionsMenu);
                screens.Add(initialMenu);
                screens.Add(creditMenu);
                actualOpenedScreen = initialMenu;
            }
        }
        private void OpenNewScreen(string screenName)
        {
            var nextScreen = screenName + "Menu";
            Debug.Log(nextScreen);
            foreach (var screen in screens)
            {
                if(screen.gameObject.name == nextScreen)
                {
                    actualOpenedScreen?.SetActive(false);
                    actualOpenedScreen = screen;
                    actualOpenedScreen?.SetActive(true);
                }
            }
        }
        public void StartFadeIn()
        {
            PostProcessingController.PPController.BlackEffect(true);
            m_gameStarted = true;
        }
        public void CloseApp()
        {
            Application.Quit();
        }
        public void RestartGame()
        {
            SceneManager.LoadScene("MenuScreen");
        }
        private void OpenPauseMenu()
        {
            if (!GameController.Instance.isUiOpened)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                actualOpenedScreen = pauseMenu;
                GameEvents.onOpenPauseMenu.Invoke(false);
            }
        }
        public void ClosePauseMenu()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            actualOpenedScreen = null;
            GameEvents.onOpenPauseMenu.Invoke(true);
        }
    }
}