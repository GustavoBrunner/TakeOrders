using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.Postprocessing
{
    public class AnimationEvents : MonoBehaviour
    {
        [SerializeField] private PostProcessingController Controller;
        public void StartGame()
        {
            SceneManager.LoadScene("House");
            SceneManager.LoadSceneAsync("RoomKitchen", LoadSceneMode.Additive);
        }
        public void LoadBedroom()
        {
            SceneManager.LoadScene("Quarto");
            Controller.BlackEffectBedroom(false);
        }
    }
}
