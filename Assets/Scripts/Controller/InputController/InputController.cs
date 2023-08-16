using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Commands
{
    public class InputController : MonoBehaviour
    {

        public bool AlternateChoiceBtn { get; private set; }

        public int InteractionBtn { get; private set; }
        public int UpChoiceBtn { get; private set; }
        public int DownChoiceBtn { get; private set; }
        public int UpChoiceBtn2 { get; private set; }
        public int DownChoiceBtn2 { get; private set; }

        public int SelectChoice1 { get; private set; }
        public int SelectChoice2 { get; private set; }
        public int SelectChoice3 { get; private set; }
        public int SelectChoice4 { get; private set; }


        private void Awake()
        {
            InteractionBtn = 0;

            UpChoiceBtn = (int)KeyCode.W;
            DownChoiceBtn = (int)KeyCode.S;

            UpChoiceBtn2 = (int)KeyCode.UpArrow;
            DownChoiceBtn2 = (int)KeyCode.DownArrow;

            SelectChoice1 = (int)KeyCode.Alpha1;
            SelectChoice2 = (int)KeyCode.Alpha2;
            SelectChoice3 = (int)KeyCode.Alpha3;
            SelectChoice4 = (int)KeyCode.Alpha4;
        }

        private void Update()
        {
            //Passamos todas as bools do input state para falsas
            InputState.DownChoiceBtn = false;
            InputState.UpChoiceBtn = false;
            InputState.InteractionBtn = false;
            InputState.SelectChoice1 = false;
            InputState.SelectChoice2 = false;
            InputState.SelectChoice3 = false;
            InputState.SelectChoice4 = false;

            //checagem se as teclas em questão foram apertadas.
            if (Input.GetMouseButtonDown(InteractionBtn))
            {
                InputState.InteractionBtn = true;
            }
            if (Input.GetMouseButtonUp(InteractionBtn))
            {
                InputState.InteractionBtn = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)UpChoiceBtn))
            {
                InputState.UpChoiceBtn = true;
            }
            if (Input.GetKeyUp((KeyCode)UpChoiceBtn))
            {
                InputState.UpChoiceBtn = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)UpChoiceBtn2))
            {
                InputState.UpChoiceBtn2 = true;
            }
            if (Input.GetKeyUp((KeyCode)UpChoiceBtn2))
            {
                InputState.UpChoiceBtn2 = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)DownChoiceBtn))
            {
                InputState.DownChoiceBtn = true;
            }
            if (Input.GetKeyUp((KeyCode)DownChoiceBtn))
            {
                InputState.DownChoiceBtn = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)DownChoiceBtn2))
            {
                InputState.DownChoiceBtn2 = true;
            }
            if (Input.GetKeyUp((KeyCode)DownChoiceBtn2))
            {
                InputState.DownChoiceBtn2 = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)SelectChoice1))
            {
                InputState.SelectChoice1 = true;
            }
            if (Input.GetKeyUp((KeyCode)SelectChoice1))
            {
                InputState.SelectChoice1 = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)SelectChoice2))
            {
                InputState.SelectChoice2 = true;
            }
            if (Input.GetKeyUp((KeyCode)SelectChoice2))
            {
                InputState.SelectChoice2 = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)SelectChoice3))
            {
                InputState.SelectChoice3 = true;
            }
            if (Input.GetKeyUp((KeyCode)SelectChoice3))
            {
                InputState.SelectChoice3 = false;
            }
            //---------------------------------------------------------------------
            if (Input.GetKeyDown((KeyCode)SelectChoice4))
            {
                InputState.SelectChoice4 = true;
            }
            if (Input.GetKeyUp((KeyCode)SelectChoice4))
            {
                InputState.SelectChoice4 = false;
            }
        }
        
    }
}