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
        private PlayerService playerService;
        #endregion

        #region database variables
        [Header("Database variables")]
        [SerializeField] private ChestModelDatabaseSO chestModelDatabaseSO;
        #endregion


        private void Start()
        {
            playerService = new PlayerService();
            chestService = new ChestService(chestModelDatabaseSO, this);
        }
    }
}