using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Events;
using ChestSystem.Player;

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

        private PlayerService playerService;

        public bool IsChestQueuedToUnlockWithTimer;


        public ChestController(ChestModelSO chestModelSO, PlayerService playerService)
        {
            ChestID = GenerateUniqueID();

            this.playerService = playerService;

            this.chestModelSO = chestModelSO;
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
            
            IsChestQueuedToUnlockWithTimer = false;
            
            CreateStateMachine();

            ChestLockedState();
        }

        private void CreateStateMachine()
        {
            chestStateMachine = new ChestStateMachine();
        }

        private void ChestLockedState()
        {
            chestStateMachine.Initialize(new ChestLockedState(this));

            CalculateTimeAndGemsRequiredToUnlockTheChest();
        }


        public void UnlockChestWithTimer()
        {
            ChestUnlockingState();
        }


        public void UnlockChestWithGems()
        {
            if(playerService.PlayerController.PlayerModel.PlayerGems >= updatedGemsRequiredToUnlockChest)
            {

                // i think this is the receiver

                playerService.PlayerController.PlayerModel.DeductPlayerGemsOnChestPurchase(updatedGemsRequiredToUnlockChest);

                ChestUnlockedState();
            }
            else
            {
                EventService.Instance.OnUIPopupActivate.InvokeEvent(UIPopups.UI_PLAYER_HAS_INSUFFICIENT_GEMS);
            }
        }


        private void CalculateTimeAndGemsRequiredToUnlockTheChest()
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

        public void UpdateTimeAndGemsRequiredTextOnUIPopup()
        {
            CalculateTimeAndGemsRequiredToUnlockTheChest();

            EventService.Instance.OnUpdateGemsAndTimeRequiredToUnlockChest.InvokeEvent(this);
        }


        private void ChestUnlockingState()
        {
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
