using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public interface IChestState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}