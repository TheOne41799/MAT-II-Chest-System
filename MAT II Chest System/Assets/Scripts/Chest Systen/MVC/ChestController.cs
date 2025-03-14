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


        public ChestController(ChestModelSO chestModelSO)
        {
            ChestModel = new ChestModel(chestModelSO, GenerateRandomCoinsInChest(chestModelSO), GenerateRandomGemsInChest(chestModelSO));
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



        /* public int ChestID { get; private set; }
         public ChestModel chestModel {  get; private set; }
         public ChestView chestView { get; private set; }

         private static int chestCounter = 0;

         private UIService uiService;

         private ChestModelSO chestModelSO;

         public ChestStateMachine chestStateMachine { get; private set; }

         public ChestController(ChestModelSO chestModelSO, ChestView chestViewPrefab, UIService uiService)
         {
             ChestID = chestCounter++;

             this.chestModelSO = chestModelSO;
             this.uiService = uiService;

             chestModel = new ChestModel(chestModelSO);
             chestView = GameObject.Instantiate(chestViewPrefab);

             EventService.Instance.OnChestCreated.InvokeEvent(this);

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
         }*/
    }
}
