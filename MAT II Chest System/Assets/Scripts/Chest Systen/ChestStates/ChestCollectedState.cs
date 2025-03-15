using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestCollectedState : IChestState
    {
        private ChestController chestController;
        public ChestState ChestState => ChestState.COLLECTED;


        public ChestCollectedState(ChestController chest) { this.chestController = chest; }


        public void EnterState() { Debug.Log("Chest has been Collected!"); }


        public void UpdateState() { }


        public void ExitState() { Debug.Log("Exiting Collected State."); }
    }
}