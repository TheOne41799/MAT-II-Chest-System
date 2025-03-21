using ChestSystem.Commands;
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

        private Queue<ChestController> chestUnlockQueue;
        private ChestController currentlyUnlockingChest;

        private MonoBehaviour coroutineRunner;

        private ChestCommandInvoker commandInvoker = new ChestCommandInvoker();
        
        //These are only chests which are unlocked with gems
        private List<ChestController> unlockedChestsWithGemsHistory = new List<ChestController>();

        public ChestService(ChestModelDatabaseSO chestModelDatabaseSO, PlayerService playerService, MonoBehaviour coroutineRunner)
        {
            chestPool = new ChestPool(chestModelDatabaseSO, playerService);

            activeChests = new Dictionary<int, ChestController>();

            chestUnlockQueue = new Queue<ChestController>();

            this.coroutineRunner = coroutineRunner;

            EventService.Instance.OnGenerateChestButtonClicked.AddListener(GetChestFromPool);
            EventService.Instance.OnChestUnlockButtonClicked.AddListener(UnlockChestButtonClicked);
            EventService.Instance.OnUnlockChest.AddListener(UnlockChest);
            EventService.Instance.OnChestRemoved.AddListener(ReturnChestToPool);
            EventService.Instance.OnQueuedChestUnlocked.AddListener(HandleChestUnlocked);
            EventService.Instance.OnQueuedChestUnlockedWithGems.AddListener(RemoveChestUnlockedWithGemsFromTimerQueue);

            EventService.Instance.OnUndoChestUnlockWithGems.AddListener(UndoLastChestAction);
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

        // check how the chest is being unlocked - through timer or by using gems
        private void UnlockChest(ChestController controller, ChestUnlockMethod chestUnlockMethod)
        {
            if (chestUnlockMethod == ChestUnlockMethod.WITH_TIMER)
            {
                if (controller.IsChestQueuedToUnlockWithTimer)
                {
                    EventService.Instance.OnUIPopupActivate.InvokeEvent(UIPopups.UI_CHEST_ALREADY_QUEUED);
                    return;
                }

                EnqueueChestForUnlock(controller);
            }
            else if (chestUnlockMethod == ChestUnlockMethod.WITH_GEMS)
            {
                if (chestUnlockMethod == ChestUnlockMethod.WITH_GEMS)
                {
                    UnlockChestCommand unlockCommand = new UnlockChestCommand(controller);
                    commandInvoker.ExecuteCommand(unlockCommand);

                    unlockedChestsWithGemsHistory.Add(controller);
                }
            }
        }

        private void EnqueueChestForUnlock(ChestController controller)
        {
            if (!chestUnlockQueue.Contains(controller) && controller.ChestStateMachine.CurrentState is ChestLockedState)
            {
                chestUnlockQueue.Enqueue(controller);

                controller.IsChestQueuedToUnlockWithTimer = true;
                
                if (chestUnlockQueue.Count > 0 && currentlyUnlockingChest != null)
                {
                    EventService.Instance.OnChestQueuedToUnlock.InvokeEvent(controller);
                }
            }

            if (currentlyUnlockingChest == null)
            {
                StartNextChestUnlock();
            }
        }

        private void StartNextChestUnlock()
        {
            if (chestUnlockQueue.Count > 0)
            {
                currentlyUnlockingChest = chestUnlockQueue.Dequeue();
                currentlyUnlockingChest.UnlockChestWithTimer();
            }
        }

        private void HandleChestUnlocked(ChestController chest)
        {
            if (currentlyUnlockingChest == chest)
            {
                currentlyUnlockingChest = null;

                StartNextChestUnlock();
            }
        }

        private void RemoveChestUnlockedWithGemsFromTimerQueue(ChestController controller)
        {
            if (chestUnlockQueue.Count == 0) return;

            Queue<ChestController> tempQueue = new Queue<ChestController>();
            bool chestFound = false;

            while (chestUnlockQueue.Count > 0)
            {
                ChestController chest = chestUnlockQueue.Dequeue();

                if (chest == controller && !chestFound)
                {
                    chestFound = true;
                    continue;
                }

                tempQueue.Enqueue(chest);
            }

            chestUnlockQueue = tempQueue;
        }

        private void ReturnChestToPool(ChestController controller)
        {
            if (activeChests.ContainsKey(controller.ChestID))
            {
                activeChests.Remove(controller.ChestID);
                controller.ChestCollectedState();
            }

            if (currentlyUnlockingChest == controller)
            {
                currentlyUnlockingChest = null;
                StartNextChestUnlock();
            }

            chestPool.ReturnChest(controller);
        }

        // using command pattern for undo
        public void UndoLastChestAction(ChestController controller)
        {
            if (unlockedChestsWithGemsHistory.Contains(controller))
            {
                unlockedChestsWithGemsHistory.Remove(controller);
                commandInvoker.UndoCommandForChest(controller);

                EventService.Instance.OnUIPopupActivate.InvokeEvent(UIPopups.UI_UNDO_CHEST_UNLOCK_WITH_GEMS);
                EventService.Instance.OnUndoChestUnlockWithGemsAddBackPlayerGems.InvokeEvent(controller);
            }
            else
            {
                EventService.Instance.OnUIPopupActivate.InvokeEvent(UIPopups.UI_CANT_UNDO_CHEST_UNLOCKED_WITH_TIMER);
            }
        }        
    }
}