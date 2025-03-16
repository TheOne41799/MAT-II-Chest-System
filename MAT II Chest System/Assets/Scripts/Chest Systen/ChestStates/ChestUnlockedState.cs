using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestUnlockedState : IChestState
    {
        private ChestController chestController;
        public ChestState ChestState => ChestState.UNLOCKED;

        public ChestUnlockedState(ChestController chest) { this.chestController = chest; }


        public void EnterState() 
        { 
            //Debug.Log("Chest is Unlocked!"); 


            EventService.Instance.OnChestUnlocked.InvokeEvent(chestController);
        }


        public void ExitState() 
        { 
            //Debug.Log("Exiting Unlocked State."); 
        }
    }
}