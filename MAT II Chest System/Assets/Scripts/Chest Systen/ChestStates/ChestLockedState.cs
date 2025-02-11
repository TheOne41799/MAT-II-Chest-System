using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestLockedState<T> : IChestState where T : ChestController
    {
        public ChestController chestController { get ; set ; }
        private GenericStateMachine<T> stateMachine;

        public ChestLockedState(GenericStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void EnterState()
        {
            chestController.chestView.ChestLockedStateUI();
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
            
        }
    }
}
