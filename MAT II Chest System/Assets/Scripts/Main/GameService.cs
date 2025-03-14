using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chests;
using ChestSystem.UI;
using ChestSystem.Player;

namespace ChestSystem.Main
{
    public class GameService : MonoBehaviour
    {
        #region services variables
        private ChestService chestService;
        #endregion

        #region database variables
        [Header("Database variables")]
        [SerializeField] private ChestModelDatabaseSO chestModelDatabaseSO;
        #endregion


        private void Start()
        {
            chestService = new ChestService(chestModelDatabaseSO);
        }        
    }
}