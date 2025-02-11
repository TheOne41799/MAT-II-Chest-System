using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public interface IChestState
    {
        public ChestController chestController { get; set; }
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}