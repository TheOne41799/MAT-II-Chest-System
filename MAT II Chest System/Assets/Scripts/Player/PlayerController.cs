using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerController
    {
        private PlayerModel playerModel;

        public PlayerController()
        {
            playerModel = new PlayerModel();
        }

        public void ChestCollected(int coins, int gems)
        {
            playerModel.UpdatePlayerStats(coins, gems);
        }
    }
}
