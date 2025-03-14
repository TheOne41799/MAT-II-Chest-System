using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChestSystem.Events;

namespace ChestSystem.Chests
{
    public class ChestUnlockingState : IChestState
    {
        private ChestController chestController;
        public ChestUnlockingState(ChestController chest) { this.chestController = chest; }


        public void EnterState() { Debug.Log("Chest is Unlocking."); }


        public void UpdateState() { }


        public void ExitState() { Debug.Log("Exiting Unlocking State."); }
    }



}
