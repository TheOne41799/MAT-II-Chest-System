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
        private UIService uiService;
        private PlayerService playerService;
        #endregion

        #region database variables
        [Header("Database variables")]
        [SerializeField] private ChestModelDatabaseSO chestModelDatabaseSO;
        #endregion

        #region prefab variables
        [Header("Prefab varibales")]
        [SerializeField] private ChestView chestViewPrefab;
        [SerializeField] private UIChestSystemViewController chestSystemViewControllerPrefab;
        [SerializeField] private UIGameplayViewController gameplayViewControllerPrefab;
        #endregion

        [SerializeField] private Canvas canvas;

        private void Start()
        {
            uiService = new UIService(chestSystemViewControllerPrefab, gameplayViewControllerPrefab, canvas);
            chestService = new ChestService(chestModelDatabaseSO, chestViewPrefab, canvas, uiService);
            playerService = new PlayerService();
        }

        private void Update()
        {
            chestService?.Update();
        }
    }
}