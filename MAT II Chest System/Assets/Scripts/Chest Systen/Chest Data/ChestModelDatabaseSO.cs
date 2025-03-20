using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    [CreateAssetMenu(fileName = "Chest Model Database", menuName = "Chest System/ Chest Model Database")]
    public class ChestModelDatabaseSO : ScriptableObject
    {
        // useful if you want to add more chests of different types
        [SerializeField] private List<ChestModelSO> chestModelSOsList;
        public List<ChestModelSO> ChestModelSOsList { get { return chestModelSOsList; } }
    }
}
