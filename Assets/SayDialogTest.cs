using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class SayDialogTest : MonoBehaviour
    {
        void Start()
        {
        }

        private void OnDisable()
        {
            GameEvents.onTurnAllUi.Invoke(true);
        }
        private void OnEnable()
        {
            GameEvents.onTurnAllUi.Invoke(false);
            //UiController.Instance.CheckUiOpened(false);
        }
    }
}
