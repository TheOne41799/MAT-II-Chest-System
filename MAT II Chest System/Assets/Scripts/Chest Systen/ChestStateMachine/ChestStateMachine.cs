using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestStateMachine : GenericStateMachine<ChestController>
    {
        public ChestStateMachine(ChestController controller) : base(controller)
        {
            this.chestController = controller;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(ChestSystem.Chests.ChestState.LOCKED, new ChestLockedState<ChestController>(this));
            States.Add(ChestSystem.Chests.ChestState.UNLOCKING, new ChestUnlockingState<ChestController>(this));
            States.Add(ChestSystem.Chests.ChestState.UNLOCKED, new ChestUnlockedState<ChestController>(this));
            States.Add(ChestSystem.Chests.ChestState.COLLECTED, new ChestCollectedState<ChestController>(this));
        }
    }
}