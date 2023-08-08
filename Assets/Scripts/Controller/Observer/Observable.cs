using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Observer
{
    /// <summary>
    /// Classe base Observable, todas as classes que ter�o observadores devem 
    /// estender ela.
    /// </summary>
    public abstract class Observable : MonoBehaviour
    {
        /// <summary>
        /// Lista de observadores que esse objeto ter�
        /// </summary>
        List<IObserver> observers;
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void Awake() 
        {
            observers = new List<IObserver>();
        }

        /// <summary>
        /// M�todo respons�vel por adicionar um observador ao objeto. Esse m�todo
        /// sempre ser� chamado de fora da classe, atrav�s de um singleton.
        /// </summary>
        /// <param name="observer"></param>
        public abstract void AddObserver(IObserver observer);
        /// <summary>
        /// Notifica��o dos observadores. M�todo gen�rico para aceitar qualquer valor,
        /// junto com um notification type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        protected abstract void NotifyObservers<T>(NotificationTypes type, T value);
        /// <summary>
        /// Remove algum observador da lista de observadores
        /// </summary>
        /// <param name="observer"></param>
        protected abstract void RemoveObserver(IObserver observer);
    }
}