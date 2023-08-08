using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Observer
{
    /// <summary>
    /// Classe base Observable, todas as classes que terão observadores devem 
    /// estender ela.
    /// </summary>
    public abstract class Observable : MonoBehaviour
    {
        /// <summary>
        /// Lista de observadores que esse objeto terá
        /// </summary>
        List<IObserver> observers;
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void Awake() 
        {
            observers = new List<IObserver>();
        }

        /// <summary>
        /// Método responsável por adicionar um observador ao objeto. Esse método
        /// sempre será chamado de fora da classe, através de um singleton.
        /// </summary>
        /// <param name="observer"></param>
        public abstract void AddObserver(IObserver observer);
        /// <summary>
        /// Notificação dos observadores. Método genérico para aceitar qualquer valor,
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