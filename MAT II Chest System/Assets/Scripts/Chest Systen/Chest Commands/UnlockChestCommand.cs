using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chests;

namespace ChestSystem.Commands
{
    public class UnlockChestCommand : IChestCommand
    {
        public ChestController ChestController { get; private set; }

        private IChestState previousState;

        public UnlockChestCommand(ChestController chest)
        {
            this.ChestController = chest;
            this.previousState = chest.ChestStateMachine.CurrentState;
        }

        public void Execute()
        {
            ChestController.UnlockChestWithGems();
        }

        public void Undo()
        {
            ChestController.ChestStateMachine.ChangeState(previousState);
        }
    }
}



