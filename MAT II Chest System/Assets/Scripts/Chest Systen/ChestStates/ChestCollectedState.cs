using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestCollectedState<T> : IChestState where T : ChestController
    {
        public ChestController chestController { get; set; }
        private GenericStateMachine<T> stateMachine;

        public ChestCollectedState(GenericStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void EnterState()
        {
            
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
            
        }
    }
}