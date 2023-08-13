using UnityEngine;

namespace Controller.Phase
{
    [CreateAssetMenu(fileName ="WorldDTO", menuName ="DTO/WorldDTO")]
    /// <summary>
    /// Vai definir os dados necess�rios para o in�cio do mundo, seja em teste,
    /// ou na jogabilidade padr�o.
    /// </summary>
    public class WorldCreationDTO : ScriptableObject
    {
        public Vector3 PlayerPosition;
        public string Name;
    }
}