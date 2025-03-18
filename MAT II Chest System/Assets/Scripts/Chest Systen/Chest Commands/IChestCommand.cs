using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Commands
{
    public interface IChestCommand
    {
        void Execute();
        void Undo();
    }
}
