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

        private int updatedGemsRequiredToUnlockChest;
        public int UpdatedGemsRequiredToUnlockChest { get { return updatedGemsRequiredToUnlockChest; } }

        private int updatedTimeRemainingToUnlockChest;
        public int UpdatedTimeRemainingToUnlockChest { get { return updatedTimeRemainingToUnlockChest; }}

        //public bool IsChestUnlocking;


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

            //IsChestUnlocking = false;
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

            //IsChestUnlocking = true;
        }


        public void UnlockChestWithGems()
        {
            //int gemsRequired = CalculateGemsRequiredToUnlockTheChest();

            
            
            
            //updatedGemsRequiredToUnlockChest = CalculateGemsRequiredToUnlockTheChest();

            // you need a new place to send this message from
            // EventService.Instance.OnUpdateGemsAndTimeRequiredToUnlockChest.InvokeEvent(this);




            Debug.Log($"Gems Required to Unlock: {updatedGemsRequiredToUnlockChest}");
        }

        /*private int CalculateGemsRequiredToUnlockTheChest()
        {
            int remainingTime = 0;

            if (chestStateMachine.CurrentState is ChestLockedState lockedState)
            {
                remainingTime = ChestModel.TimeRequiredToUnlockChest;
            }
            else if (chestStateMachine.CurrentState is ChestUnlockingState unlockingState)
            {
                remainingTime = unlockingState.UnlockTimeRemaining;
            }

            return Mathf.CeilToInt(remainingTime / 10f) + ChestModel.MinimumGemsRequiredToUnlockChest;
        }*/

        /*private int CalculateGemsRequiredToUnlockTheChest()
        {
            if (chestStateMachine.CurrentState is ChestLockedState lockedState)
            {
                updatedTimeRemainingToUnlockChest = ChestModel.TimeRequiredToUnlockChest;
            }
            else if (chestStateMachine.CurrentState is ChestUnlockingState unlockingState)
            {
                updatedTimeRemainingToUnlockChest = unlockingState.UnlockTimeRemaining;
            }

            return Mathf.CeilToInt(updatedTimeRemainingToUnlockChest / 10f) + ChestModel.MinimumGemsRequiredToUnlockChest;
        }*/

        private void CalculateGemsRequiredToUnlockTheChest()
        {
            if (chestStateMachine.CurrentState is ChestLockedState lockedState)
            {
                updatedTimeRemainingToUnlockChest = ChestModel.TimeRequiredToUnlockChest;
            }
            else if (chestStateMachine.CurrentState is ChestUnlockingState unlockingState)
            {
                updatedTimeRemainingToUnlockChest = unlockingState.UnlockTimeRemaining;
            }

            updatedGemsRequiredToUnlockChest = Mathf.CeilToInt(updatedTimeRemainingToUnlockChest / 10f) + ChestModel.MinimumGemsRequiredToUnlockChest;
        }

        public void UpdateTimeAndGemsRequiredText()
        {
            //Debug.Log("asasdasd");

            CalculateGemsRequiredToUnlockTheChest();

            EventService.Instance.OnUpdateGemsAndTimeRequiredToUnlockChest.InvokeEvent(this);
        }

        /*private void CalculateGemsRequiredToUnlockTheChest()
        {
            if (chestStateMachine.CurrentState is ChestLockedState lockedState)
            {
                int remainingTime = ChestModel.TimeRequiredToUnlockChest;

                int gemsRequired = CalculateGemsRequiredToUnlockTheChest(remainingTime);

                Debug.Log($"Gems Required to Unlock: {gemsRequired}");
            }
            else if (chestStateMachine.CurrentState is ChestUnlockingState unlockingState)
            {
                int remainingTime = unlockingState.UnlockTimeRemaining;

                int gemsRequired = CalculateGemsRequiredToUnlockTheChest(remainingTime);

                Debug.Log($"Gems Required to Unlock: {gemsRequired}");
            }
        }

        private int CalculateGemsRequiredToUnlockTheChest(int remainingTimeInSeconds)
        {
            int gemsRequiredBasedOnTimeLeft = Mathf.CeilToInt(remainingTimeInSeconds / 10f);

            int totalGemsRequiredBasedOnTimeLeft = gemsRequiredBasedOnTimeLeft + ChestModel.MinimumGemsRequiredToUnlockChest;

            return totalGemsRequiredBasedOnTimeLeft;
        }*/

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
