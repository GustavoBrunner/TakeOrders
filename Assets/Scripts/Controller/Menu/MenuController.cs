using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        private GameObject initialMenu, optionsMenu, creditMenu;

        private List<GameObject> screens = new List<GameObject>();
        private GameObject actualOpenedScreen;
        private bool m_gameStarted;
        public bool gameStarted 
        {
            get => m_gameStarted;
            set
            {
                if (value)
                {

                }
            }
        }
        private void Awake()
        {
            GetMenus();
            actualOpenedScreen = initialMenu;
            optionsMenu.SetActive(false);
            creditMenu.SetActive(false);
        }
        private void Start()
        {
            MenuEvents.onChangeMenuScreen.AddListener(OpenNewScreen);
        }
        private void GetMenus()
        {
            initialMenu = GameObject.Find("InitialMenu");
            screens.Add(initialMenu);
            optionsMenu = GameObject.Find("OptionsMenu");
            screens.Add(optionsMenu);
            creditMenu = GameObject.Find("CreditsMenu");
            screens.Add(creditMenu);
        }
        private void OpenNewScreen(string screenName)
        {
            var nextScreen = screenName + "Menu";
            Debug.Log(nextScreen);
            foreach (var screen in screens)
            {
                if(screen.gameObject.name == nextScreen)
                {
                    actualOpenedScreen.SetActive(false);
                    actualOpenedScreen = screen;
                    actualOpenedScreen.SetActive(true);
                }
            }
        }
        public void StartGame()
        {
            SceneManager.LoadScene("House");
            SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        }
    }
}