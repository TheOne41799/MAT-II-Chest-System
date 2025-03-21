using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chests;
using ChestSystem.UI;
using ChestSystem.Player;
using ChestSystem.Audio;

namespace ChestSystem.Main
{
    // Note - Hi, I just used different assets to check the UX
    // The chest assets I am finding are not really that good
    // I just replaced them with character busts
    public class GameService : MonoBehaviour
    {
        #region services variables
        private ChestService chestService;
        private PlayerService playerService;
        private AudioService audioService;
        #endregion

        #region database variables
        [Header("Database variables")]
        [SerializeField] private ChestModelDatabaseSO chestModelDatabaseSO;
        [SerializeField] private AudioDatabaseSO audioDatabase;
        #endregion

        #region manager variables
        [Header("Prefabs")]
        [SerializeField] private UIManager uiManager;
        [SerializeField] private AudioView audioViewPrefab;
        #endregion

        private void Start()
        {
            playerService = new PlayerService();
            chestService = new ChestService(chestModelDatabaseSO, playerService, this);
            uiManager = GameObject.Instantiate(uiManager);
            audioService = new AudioService(audioViewPrefab, audioDatabase);

            playerService.PlayerController.PlayerModel.InitializePlayerStats();
        }
    }
}