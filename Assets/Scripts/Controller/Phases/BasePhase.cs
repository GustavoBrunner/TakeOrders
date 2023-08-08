using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Phase
{
    /// <summary>
    /// Classe que vai gerir as fases do jogo, serão instanciadas no GameController
    /// </summary>
    public abstract class BasePhase
    {
        protected GamePhases Phase;
        protected BasePhase NextPhase;
        protected PhaseState State;
        protected WorldCreationDTO Dto;

        public BasePhase(GamePhases phase, BasePhase nextPhase, PhaseState state, WorldCreationDTO dto)
        {
            this.Phase = phase;
            this.NextPhase = nextPhase;
            this.State = state;
            this.Dto = dto;
        }

        /// <summary>
        /// Responsável por dar o início à fase, e tudo que precisa acontecer ao início
        /// do game
        /// </summary>
        public abstract void EnterPhase();
        /// <summary>
        /// Fará, através do parâmetro state, o update dentro da fase atual, 
        /// se o estado da fase mudar, o que está dentro desse método será executado
        /// </summary>
        /// <param name="state">Parâmetro que define em qual estado da fase atual estamos</param>
        public abstract void UpdatePhaseState(PhaseState state);
        /// <summary>
        /// Método responsável por ir para a próxima fase. 
        /// </summary>
        public abstract void GoToNextPhase();
    }
}
