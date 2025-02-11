using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class GenericStateMachine<T> where T: ChestController
    {
        protected T chestController;
        protected IChestState currentState;
        protected Dictionary<ChestState, IChestState> States = new Dictionary<ChestState, IChestState> ();

        public GenericStateMachine(T controller)
        {
            this.chestController = controller;
        }        

        public void ChangeState(ChestState newState)
        {
            ChangeState(States[newState]);
        }

        protected void ChangeState(IChestState newState)
        {
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState();
        }

        public void Update()
        {
            currentState?.UpdateState();
        }

        protected void SetOwner()
        {
            foreach (IChestState state in States.Values)
            {
                state.chestController = chestController;
            }
        }
    }
}