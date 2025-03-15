using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerModel
    {
        private int playerCoins = 0;
        private int playerGems = 0;

        public PlayerModel()
        {

        }

        public void UpdatePlayerStats(int coins, int gems)
        {
            playerCoins += coins;
            playerGems += gems;

            EventService.Instance.OnPlayerStatsUpdated.InvokeEvent(playerCoins, playerGems);
        }
    }
}