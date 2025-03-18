using ChestSystem.Chests;
using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerService
    {
        private PlayerController playerController;
        public PlayerController PlayerController { get { return playerController; } }


        public PlayerService()
        {
            this.playerController = new PlayerController();

            EventService.Instance.OnChestCollected.AddListener(ChestCollected);
            EventService.Instance.OnUndoChestUnlockWithGemsAddBackPlayerGems.AddListener(AddBackPlayerGemsOnUndoChestUnlock);
        }

        private void ChestCollected(int coins, int gems)
        {
            playerController.ChestCollected(coins, gems);
        }

        private void AddBackPlayerGemsOnUndoChestUnlock(ChestController controller)
        {
            playerController.PlayerModel.AddBackPlayerGemsOnUndoChestUnlock(controller.UpdatedGemsRequiredToUnlockChest);
        }
    }
}