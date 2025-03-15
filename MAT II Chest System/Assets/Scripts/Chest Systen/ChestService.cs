using ChestSystem.Events;
using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private ChestPool chestPool;
        private Dictionary<int, ChestController> activeChests;


        public ChestService(ChestModelDatabaseSO chestModelDatabaseSO)
        {
            chestPool = new ChestPool(chestModelDatabaseSO);

            activeChests = new Dictionary<int, ChestController>();

            EventService.Instance.OnGenerateChestButtonClicked.AddListener(GetChestFromPool);
            EventService.Instance.OnChestUnlockButtonClicked.AddListener(UnlockChest);
        }

        private void GetChestFromPool()
        {
            ChestController controller = chestPool.GetChest();

            if (!activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Add(controller.ChestID, controller);
            }

            EventService.Instance.OnChestAdded.InvokeEvent(controller);
        }

        private void UnlockChest(int chestID)
        {
            if (activeChests.ContainsKey(chestID))
            {
                ChestController controller = activeChests[chestID];
                EventService.Instance.OnUIPopupChestUnlockActivate.InvokeEvent(controller, UIPopups.UI_CHEST_UNLOCK_POPUP);
                


                //Test - Important value
                //activeChests[chestID].UnlockChest();
            }
        }

        private void ReturnChestToPool(ChestController controller)
        { 
            if (activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Remove(controller.ChestID);
            }

            chestPool.ReturnChest(controller);
        }

        private bool IsAnyChestUnlocking()
        {
            return false;
        }

        // command pattern

    }
}