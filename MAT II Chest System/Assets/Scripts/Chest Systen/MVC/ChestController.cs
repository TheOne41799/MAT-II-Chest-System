using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Events;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        public ChestModel ChestModel { get; private set; }

        private ChestStateMachine chestStateMachine;
        public ChestStateMachine ChestStateMachine {  get { return chestStateMachine; } }

        public int ChestID { get; private set; }
        private static int nextChestID = 0;

        public MonoBehaviour CoroutineRunner { get; set; }

        private ChestModelSO chestModelSO;

        public bool IsChestUnlocking;


        public ChestController(ChestModelSO chestModelSO)
        {
            ChestID = GenerateUniqueID();

            this.chestModelSO = chestModelSO;

            //ChestModel = new ChestModel(chestModelSO, GenerateRandomCoinsInChest(chestModelSO), GenerateRandomGemsInChest(chestModelSO));

            //InitializeVariables();
        }

        private int GenerateUniqueID()
        {
            return nextChestID++;
        }

        private int GenerateRandomCoinsInChest(ChestModelSO chestModelSO)
        {
            int randCoins = Random.Range(chestModelSO.MinimumCoinsInChest, chestModelSO.MaximumCoinsInChest);
            return randCoins;
        }

        private int GenerateRandomGemsInChest(ChestModelSO chestModelSO)
        {
            int randGems = Random.Range(chestModelSO.MinimuGemsInChest, chestModelSO.MaximumGemsInChest);
            return randGems;
        }

        public void InitializeVariables()
        {
            ChestModel = new ChestModel(chestModelSO, GenerateRandomCoinsInChest(chestModelSO), GenerateRandomGemsInChest(chestModelSO));

            CreateStateMachine();

            ChestLockedState();

            IsChestUnlocking = false;
        }

        private void CreateStateMachine()
        {
            chestStateMachine = new ChestStateMachine();
        }

        private void ChestLockedState()
        {
            chestStateMachine.Initialize(new ChestLockedState(this));
        }


        public void UnlockChestWithTimer()
        {
            //Debug.Log("Unlock with Timer");

            //Debug.Log($"Matching chest found with key value of {ChestID}");

            ChestUnlockingState();

            IsChestUnlocking = true;
        }

        public void UnlockChestWithGems()
        {
            //Debug.Log("Unlock with Gems");
            //Debug.Log("Chest state is locked or unlocking");

            //plan
            //- send a message from here to the player

            /*if (chestStateMachine.CurrentState is ChestUnlockingState unlockingState)
            {
                int remainingTime = unlockingState.UnlockTimeRemaining;

                Debug.Log($"Remaining unlock time: {remainingTime} seconds");

                // Now you can use `remainingTime` to calculate gem cost or implement unlocking logic
            }*/

            // first if check if timer is active, if yes then calculate the required number of gems
            //- check if he has enough gems
            //- if yes, unlock the chest - also check the timer value
            //- if no, create a method for UIPop indicating not enough gems

            //ChestUnlockedState();
        }

        private void ChestUnlockingState()
        {
            //chestStateMachine.ChangeState(new ChestUnlockingState(this));

            chestStateMachine.ChangeState(new ChestUnlockingState(this, CoroutineRunner));
        }

        public void ChestUnlockedState()
        {
            
            chestStateMachine.ChangeState(new ChestUnlockedState(this));
        }

        public void ChestCollectedState()
        {
            chestStateMachine.ChangeState(new ChestCollectedState(this));
        }
    }
}
