using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Events;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        public ChestModel chestModel {  get; private set; }
        public ChestView chestView { get; private set; }

        private UIService uiService;

        private ChestModelSO chestModelSO;

        private ChestStateMachine chestStateMachine;

        public ChestController(ChestModelSO chestModelSO, ChestView chestViewPrefab, UIService uiService)
        {
            this.chestModelSO = chestModelSO;
            this.uiService = uiService;

            chestModel = new ChestModel(chestModelSO);
            chestView = GameObject.Instantiate(chestViewPrefab);

            EventService.Instance.OnChestCreated.InvokeEvent(chestView);

            InitializeVariables();
        }

        private void InitializeVariables()
        {        
            chestModel.SetChestController(this);
            chestView.SetChestController(this);            

            CreateStateMachine();
            chestView.currentChestState = ChestState.LOCKED;
            chestStateMachine.ChangeState(ChestState.LOCKED);            

            chestView.InitializeVariables();
        }

        public void Update()
        {
            chestStateMachine.Update();
        }

        private void CreateStateMachine()
        {
            chestStateMachine = new ChestStateMachine(this);
        }

        public void UnlockingChestWithTimer()
        {            
            chestStateMachine.ChangeState(ChestState.UNLOCKING);
            chestView.currentChestState = ChestState.UNLOCKING;
        }

        public void UnlockingChestWithGems()
        {
            // check for conditions

            
            chestStateMachine.ChangeState(ChestState.UNLOCKED);
            chestView.currentChestState = ChestState.UNLOCKED;
        }

        public void UnlockedChest()
        {
            chestStateMachine.ChangeState(ChestState.UNLOCKED);
            chestView.currentChestState = ChestState.UNLOCKED;
        }
    }
}
