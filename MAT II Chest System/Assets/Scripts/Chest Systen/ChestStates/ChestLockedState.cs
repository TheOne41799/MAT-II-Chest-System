using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestLockedState : IChestState
    {
        private ChestController chestController;
        public ChestState ChestState => ChestState.LOCKED;

        public ChestLockedState(ChestController chest) { this.chestController = chest; }


        public void EnterState() 
        {
            EventService.Instance.OnChestAdded.InvokeEvent(chestController);
        }

        public void ExitState() 
        {
            
        }
    }
}
