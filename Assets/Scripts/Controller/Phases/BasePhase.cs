using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Phase
{
    /// <summary>
    /// Classe que vai gerir as fases do jogo, ser�o instanciadas no GameController
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
        /// Respons�vel por dar o in�cio � fase, e tudo que precisa acontecer ao in�cio
        /// do game
        /// </summary>
        public abstract void EnterPhase();
        /// <summary>
        /// Far�, atrav�s do par�metro state, o update dentro da fase atual, 
        /// se o estado da fase mudar, o que est� dentro desse m�todo ser� executado
        /// </summary>
        /// <param name="state">Par�metro que define em qual estado da fase atual estamos</param>
        public abstract void UpdatePhaseState(PhaseState state);
        /// <summary>
        /// M�todo respons�vel por ir para a pr�xima fase. 
        /// </summary>
        public abstract void GoToNextPhase();
    }
}
