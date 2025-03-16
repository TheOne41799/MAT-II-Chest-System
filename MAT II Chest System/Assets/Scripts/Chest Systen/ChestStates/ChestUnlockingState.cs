using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChestSystem.Events;
using UnityEngine.UIElements;

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


        public ChestUnlockingState(ChestController chest, MonoBehaviour coroutineRunner) 
        { 
            this.chestController = chest;
            this.unlockTimeRemaining = chest.ChestModel.TimeRequiredToUnlockChest;

            this.coroutineRunner = coroutineRunner;
        }


        public void EnterState() 
        {
            EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);

            coroutineRunner.StartCoroutine(UnlockChestRoutine());
        }


        

        private IEnumerator UnlockChestRoutine()
        {
            while (unlockTimeRemaining > 0)
            {
                yield return new WaitForSeconds(1);
                unlockTimeRemaining--;

                EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);

                chestController.UpdateTimeAndGemsRequiredText();
            }

            isChestUnlocked = true;

            chestController.ChestUnlockedState();
        }

        public void ExitState() 
        { 
            //Debug.Log("Exiting Unlocking State."); 
        }


    }



}
