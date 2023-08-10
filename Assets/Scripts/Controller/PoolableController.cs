using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Object;
using Controller.Phase;
namespace Controller
{
    /// <summary>
    /// Object pool, responsável por, ao iniciar o jogo, criar uma série de game objects
    /// que surgirão ao longo do jogo. Ele também ligará e desligará todos eles, de acordo
    /// com o necessário.
    /// </summary>
    public class PoolableController 
    {
        private static List<GameObject> prefabs = new List<GameObject>();
        private static List<IPoolable> poolables = new List<IPoolable>();

        /// <summary>
        /// Responsável por receber um prefab qualquer, e adicionar ele à lista
        /// dentro da função será verificado se esse prefab é um poolable, se for, ele será adicionado à lista
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
        //Mesma coisa que o anterior, porém ele recebe um array de gameobject, caso seja necessário.
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
        /// Responsável por desligar os objetos
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
        /// Verifica se, o atributo phase dos objetos é igual ao recebido pela função, se sim, ligará os objetos referentes àquela phase
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