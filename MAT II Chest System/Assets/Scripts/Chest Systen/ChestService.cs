using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private ChestPool chestPool;
        private Dictionary<int, ChestController> activeChests;


        private MonoBehaviour coroutineRunner;


        public ChestService(ChestModelDatabaseSO chestModelDatabaseSO, PlayerService playerService, MonoBehaviour coroutineRunner)
        {
            chestPool = new ChestPool(chestModelDatabaseSO, playerService);

            activeChests = new Dictionary<int, ChestController>();

            this.coroutineRunner = coroutineRunner;

            EventService.Instance.OnGenerateChestButtonClicked.AddListener(GetChestFromPool);
            EventService.Instance.OnChestUnlockButtonClicked.AddListener(UnlockChestButtonClicked);
            EventService.Instance.OnUnlockChest.AddListener(UnlockChest);
            EventService.Instance.OnChestRemoved.AddListener(ReturnChestToPool);
        }

        private void GetChestFromPool()
        {
            ChestController controller = chestPool.GetChest();

            controller.CoroutineRunner = coroutineRunner;

            if (!activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Add(controller.ChestID, controller);

                //Debug.Log("Active chest after addition: " + activeChests.Count);
            }

            EventService.Instance.OnChestAdded.InvokeEvent(controller);
        }

        private void UnlockChestButtonClicked(int chestID)
        {
            if (activeChests.ContainsKey(chestID))
            {
                ChestController controller = activeChests[chestID];
                EventService.Instance.OnUIPopupChestUnlockActivate.InvokeEvent(controller, UIPopups.UI_CHEST_UNLOCK_POPUP);
            }
        }

        private void UnlockChest(ChestController controller, ChestUnlockMethod chestUnlockMethod)
        {
            if (chestUnlockMethod == ChestUnlockMethod.WITH_TIMER)
            {
                controller.UnlockChestWithTimer();
            }
            else if(chestUnlockMethod == ChestUnlockMethod.WITH_GEMS)
            {
                controller.UnlockChestWithGems();
            }
        }

        private void ReturnChestToPool(ChestController controller)
        { 
            if (activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Remove(controller.ChestID);

                controller.ChestCollectedState();


                //Debug.Log("Active chest after removal: " + activeChests.Count);

                

            }


            chestPool.ReturnChest(controller);

            

        }        

        /*private bool IsAnyChestUnlocking()
        {
            return false;
        }*/

        /*private bool IsAnyChestUnlocking()
        {
            return activeChests.Values.Any(chest => chest.CurrentState == ChestState.UNLOCKING);
        }*/

        // command pattern

    }
}