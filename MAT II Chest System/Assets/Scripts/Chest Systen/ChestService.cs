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


        //test
        /*private Queue<ChestController> chestUnlockQueue;
        private ChestController currentlyUnlockingChest;*/
        //


        private MonoBehaviour coroutineRunner;


        public ChestService(ChestModelDatabaseSO chestModelDatabaseSO, PlayerService playerService, MonoBehaviour coroutineRunner)
        {
            chestPool = new ChestPool(chestModelDatabaseSO, playerService);

            activeChests = new Dictionary<int, ChestController>();



            //test
            //chestUnlockQueue = new Queue<ChestController>();
            //



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

        //working
        private void UnlockChest(ChestController controller, ChestUnlockMethod chestUnlockMethod)
        {
            if (chestUnlockMethod == ChestUnlockMethod.WITH_TIMER)
            {
                controller.UnlockChestWithTimer();
            }
            else if (chestUnlockMethod == ChestUnlockMethod.WITH_GEMS)
            {
                controller.UnlockChestWithGems();
            }
        }
        //



        //test

        // Chest Queue

        /*private void UnlockChest(ChestController controller, ChestUnlockMethod chestUnlockMethod)
        {
            if (chestUnlockMethod == ChestUnlockMethod.WITH_TIMER)
            {
                EnqueueChestForUnlock(controller);
            }
            else if (chestUnlockMethod == ChestUnlockMethod.WITH_GEMS)
            {
                controller.UnlockChestWithGems();
            }
        }*/

        /*private void EnqueueChestForUnlock(ChestController controller)
        {
            if (!chestUnlockQueue.Contains(controller) && controller.ChestStateMachine.CurrentState is ChestLockedState)
            {
                chestUnlockQueue.Enqueue(controller);
            }

            if (currentlyUnlockingChest == null)
            {
                StartNextChestUnlock();
            }
        }*/

        /*private void StartNextChestUnlock()
        {
            if (chestUnlockQueue.Count > 0)
            {
                currentlyUnlockingChest = chestUnlockQueue.Dequeue();
                currentlyUnlockingChest.UnlockChestWithTimer();

                // Listen for when the chest is fully unlocked
                currentlyUnlockingChest.OnChestUnlocked += HandleChestUnlocked;
            }
        }*/


        /*private void HandleChestUnlocked(ChestController chest)
        {
            if (currentlyUnlockingChest == chest)
            {
                currentlyUnlockingChest.OnChestUnlocked -= HandleChestUnlocked;
                currentlyUnlockingChest = null;

                // Start unlocking the next chest in queue
                StartNextChestUnlock();
            }
        }*/


        /*private void ReturnChestToPool(ChestController controller)
        {
            if (activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Remove(controller.ChestID);
                controller.ChestCollectedState();
            }

            // If the chest being unlocked is removed, clear it
            if (currentlyUnlockingChest == controller)
            {
                currentlyUnlockingChest = null;
                StartNextChestUnlock();
            }

            chestPool.ReturnChest(controller);
        }*/


        // command pattern

        ///////////////////


        // working
        private void ReturnChestToPool(ChestController controller)
        {
            if (activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Remove(controller.ChestID);

                controller.ChestCollectedState();
            }

            chestPool.ReturnChest(controller);
        }
    }
}