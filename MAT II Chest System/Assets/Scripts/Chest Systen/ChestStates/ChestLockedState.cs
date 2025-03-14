using ChestSystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestLockedState : IChestState
    {
        private ChestController chestController;


        public ChestLockedState(ChestController chest) { this.chestController = chest; }


        public void EnterState() 
        {
            
        }


        public void UpdateState() { }


        public void ExitState() { Debug.Log("Exiting Locked State."); }

        
    }
}
