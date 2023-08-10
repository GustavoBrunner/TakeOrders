using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Object;
using Controller.Phase;
namespace Controller
{
    /// <summary>
    /// Object pool, respons�vel por, ao iniciar o jogo, criar uma s�rie de game objects
    /// que surgir�o ao longo do jogo. Ele tamb�m ligar� e desligar� todos eles, de acordo
    /// com o necess�rio.
    /// </summary>
    public class PoolableController 
    {
        private static List<GameObject> prefabs = new List<GameObject>();
        private static List<IPoolable> poolables = new List<IPoolable>();

        /// <summary>
        /// Respons�vel por receber um prefab qualquer, e adicionar ele � lista
        /// dentro da fun��o ser� verificado se esse prefab � um poolable, se for, ele ser� adicionado � lista
        /// de poolables
        /// </summary>
        /// <param name="prefab"></param>
        public static void AddPrefab(GameObject prefab)
        {
            prefabs.Add(prefab);
            Debug.Log($"Prefabs list: {prefabs.Count}");
            var pool = prefab.GetComponent<IPoolable>();
            if(pool != null)
            {
                poolables.Add(pool);
                Debug.Log($"Pool list: {poolables.Count}");
            }
        }
        //Mesma coisa que o anterior, por�m ele recebe um array de gameobject, caso seja necess�rio.
        public static void AddPrefab(GameObject[] prefab)
        {
            prefabs.AddRange(prefab);
        }
        /// <summary>
        /// Instancia os objetos poolable, e desliga eles.
        /// </summary>
        public static void InstantiateObjects()
        {
            foreach (var prefab in prefabs)
            {
                GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            }
            TurnObjectsOff();
        }
        /// <summary>
        /// Respons�vel por desligar os objetos
        /// </summary>
        private static void TurnObjectsOff()
        {
            foreach (var pool in poolables)
            {
                pool.TurnOff();
                Debug.Log("Pool objects turned off");
            }
        }
        /// <summary>
        /// Verifica se, o atributo phase dos objetos � igual ao recebido pela fun��o, se sim, ligar� os objetos referentes �quela phase
        /// </summary>
        /// <param name="phase"></param>
        public static void TurnObjectsOn(GamePhases phase)
        {
            foreach (var pool in poolables)
            {
                if (pool.Phase == phase)
                    pool.TurnOn();
            }
        }
    }
}