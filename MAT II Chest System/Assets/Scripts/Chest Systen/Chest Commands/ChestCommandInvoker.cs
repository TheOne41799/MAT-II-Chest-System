using System.Collections.Generic;
using ChestSystem.Commands;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestCommandInvoker
    {
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

