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
        private bool isChestUnlocked = false;

        public ChestState ChestState => ChestState.UNLOCKING;

        private MonoBehaviour coroutineRunner;


        public ChestUnlockingState(ChestController chest, MonoBehaviour coroutineRunner) 
        { 
            this.chestController = chest;
            this.unlockTimeRemaining = chest.ChestModel.TimeRequiredToUnlockChest;

            this.coroutineRunner = coroutineRunner;

            //Debug.Log(this.unlockTimeRemaining);
        }


        public void EnterState() 
        {
            //Debug.Log("Chest is Unlocking."); 

            //EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);

            coroutineRunner.StartCoroutine(UnlockChestRoutine());
        }


        //test
        public void UpdateState()
        {


            /*if (!isChestUnlocked)
            {
                if (unlockTimeRemaining > 0)
                {
                    unlockTimeRemaining -= (int)Time.deltaTime;

                    Debug.Log("Unlock time: " + unlockTimeRemaining);
                }
                else
                {
                    unlockTimeRemaining = 0;
                    isChestUnlocked = true;
                }

                EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);
            }*/
        }
        //

        private IEnumerator UnlockChestRoutine()
        {
            while (unlockTimeRemaining > 0)
            {
                yield return new WaitForSeconds(1);
                unlockTimeRemaining--;

                EventService.Instance.OnUnlockingChest.InvokeEvent(chestController, unlockTimeRemaining);
            }

            isChestUnlocked = true;


        }

        public void ExitState() { Debug.Log("Exiting Unlocking State."); }


    }



}
