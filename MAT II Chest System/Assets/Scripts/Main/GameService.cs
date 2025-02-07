using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chests;
using ChestSystem.UI;

namespace ChestSystem.Main
{
    public class GameService : MonoBehaviour
    {
        #region services variables
        private ChestService chestService;
        private UIService uiService;
        #endregion

        #region database variables
        [SerializeField] private ChestModelDatabaseSO chestModelDatabaseSO;
        #endregion

        #region prefab variables
        [SerializeField] private ChestView chestViewPrefab;
        [SerializeField] private UIChestSystemViewController chestSystemViewControllerPrefab;
        [SerializeField] private UIGameplayViewController gameplayViewControllerPrefab;
        #endregion

        private void Start()
        {
            chestService = new ChestService(chestModelDatabaseSO, chestViewPrefab);
            uiService = new UIService(chestSystemViewControllerPrefab, gameplayViewControllerPrefab);
        }
    }
}