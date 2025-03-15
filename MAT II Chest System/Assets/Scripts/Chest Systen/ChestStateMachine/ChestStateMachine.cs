using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestStateMachine
    {
        private IChestState currentState;
        public IChestState CurrentState { get { return currentState; } }


        public void Initialize(IChestState initialState)
        {
            currentState = initialState;
            currentState.EnterState();            
        }

        public void ChangeState(IChestState newState)
        {
            currentState?.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
    }
}