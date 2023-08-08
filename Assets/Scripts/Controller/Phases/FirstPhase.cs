using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.Phase
{
    public class FirstPhase : BasePhase
    {
        public FirstPhase(GamePhases phase, BasePhase nextPhase, PhaseState state, WorldCreationDTO dto) : base(phase, nextPhase, state, dto)
        {
        }

        public override void EnterPhase()
        {
            
        }

        public override void GoToNextPhase()
        {
            this.NextPhase.EnterPhase();
        }

        public override void UpdatePhaseState(PhaseState state)
        {
            
        }       
    }
}
