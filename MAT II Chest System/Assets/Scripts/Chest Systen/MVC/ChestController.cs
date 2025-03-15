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


        public ChestController(ChestModelSO chestModelSO)
        {
            ChestID = GenerateUniqueID();

            ChestModel = new ChestModel(chestModelSO, GenerateRandomCoinsInChest(chestModelSO), GenerateRandomGemsInChest(chestModelSO));

            InitializeVariables();
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

        private void InitializeVariables()
        {
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
        }


        public void UnlockChestWithTimer()
        {
            //Debug.Log("Unlock with Timer");

            //Debug.Log($"Matching chest found with key value of {ChestID}");

            ChestUnlockingState();
        }

        public void UnlockChestWithGems()
        {
            //Debug.Log("Unlock with Gems");

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

        private void ChestCollectedState()
        {
            chestStateMachine.ChangeState(new ChestCollectedState(this));
        }
    }
}
