using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    // using an enum to check which method (which button is pressed) is used to unlock chests - timer or gems
    public enum ChestUnlockMethod
    {
        NONE,
        WITH_TIMER,
        WITH_GEMS
    }
}
