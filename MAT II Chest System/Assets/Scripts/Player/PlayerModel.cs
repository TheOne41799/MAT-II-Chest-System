using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerModel
    {
        private int playerCoins = 0;
        private int playerGems = 100;
        public int PlayerGems { get { return playerGems; } }

        public PlayerModel()
        {
            InitializePlayerStats();
        }

        private void InitializePlayerStats()
        {
            EventService.Instance.OnPlayerStatsUpdated.InvokeEvent(playerCoins, playerGems);
        }

        public void UpdatePlayerStats(int coins, int gems)
        {
            playerCoins += coins;
            playerGems += gems;

            EventService.Instance.OnPlayerStatsUpdated.InvokeEvent(playerCoins, playerGems);
        }

        public void DeductPlayerGemsOnChestPurchase(int gems)
        {
            playerGems -= gems;

            EventService.Instance.OnPlayerStatsUpdated.InvokeEvent(playerCoins, playerGems);
        }

        public void AddBackPlayerGemsOnUndoChestUnlock(int gems)
        {
            playerGems += gems;

            EventService.Instance.OnPlayerStatsUpdated.InvokeEvent(playerCoins, playerGems);
        }
    }
}