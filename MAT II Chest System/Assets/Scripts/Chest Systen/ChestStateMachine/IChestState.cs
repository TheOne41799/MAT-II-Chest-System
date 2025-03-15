using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public interface IChestState
    {
        ChestState ChestState { get; }
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}