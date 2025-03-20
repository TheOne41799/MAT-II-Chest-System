using System.Collections.Generic;
using ChestSystem.Commands;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestCommandInvoker
    {
        // Used a list instead of stack to make sure that data can be easily accessed in a list
        // because chestControllers can be unlocked in any order and undo can also be done in any order

        private List<IChestCommand> commandHistory = new List<IChestCommand>();

        public void ExecuteCommand(IChestCommand command)
        {
            command.Execute();
            commandHistory.Add(command);
        }

        public void UndoCommandForChest(ChestController targetChest)
        {
            for (int i = 0; i < commandHistory.Count; i++)
            {
                if (commandHistory[i] is UnlockChestCommand unlockCommand &&
                    unlockCommand.ChestController == targetChest)
                {
                    commandHistory[i].Undo();
                    commandHistory.RemoveAt(i);
                    return;
                }
            }
        }
    }
}

