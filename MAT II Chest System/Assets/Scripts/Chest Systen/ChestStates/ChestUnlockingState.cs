using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChestSystem.Events;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace ChestSystem.Chests
{
    public class ChestUnlockingState : IChestState
    {
        private ChestController chestController;

        private int unlockTimeRemaining;
        public int UnlockTimeRemaining { get { return unlockTimeRemaining; } }

        private bool isChestUnlocked = false;
        public bool IsChestUnlocked { get { return isChestUnlocked; } }

        public ChestState ChestState => ChestState.UNLOCKING;

        private MonoBehaviour coroutineRunner;

        private Coroutine chestUnlockingCoroutine;
        public Coroutine ChestUnlockingCoroutine { get { return chestUnlockingCoroutine; } }


        public ChestUnlockingState(ChestController chest, MonoBehaviour coroutineRunner) 
        { 
            this.chestController = chest;
            this.unlockTimeRemaining = chest.ChestModel.TimeRequiredToUnlockChest;

            this.coroutineRunner = coroutineRunner;
        }


        public void EnterState() 
        {
            EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);

            chestUnlockingCoroutine = coroutineRunner.StartCoroutine(UnlockChestRoutine());
        }


        private IEnumerator UnlockChestRoutine()
        {
            while (unlockTimeRemaining > 0)
            {
                yield return new WaitForSeconds(1);
                unlockTimeRemaining--;

                EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);

                chestController.UpdateTimeAndGemsRequiredTextOnUIPopup();
            }

            isChestUnlocked = true;

            chestController.ChestUnlockedState();
        }

        public void ExitState() 
        { 
            StopChestUnlockingCoroutine();

        }

        private void StopChestUnlockingCoroutine()
        {
            coroutineRunner.StopCoroutine(chestUnlockingCoroutine);
        }


    }



}
