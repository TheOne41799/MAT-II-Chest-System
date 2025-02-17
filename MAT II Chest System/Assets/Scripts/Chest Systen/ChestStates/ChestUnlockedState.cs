using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestUnlockedState<T> : IChestState where T : ChestController
    {
        public ChestController chestController { get; set; }
        private GenericStateMachine<T> stateMachine;

        public ChestUnlockedState(GenericStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void EnterState()
        {
            chestController.chestView.ChestUnlockedStateUI();
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
            
        }
    }
}