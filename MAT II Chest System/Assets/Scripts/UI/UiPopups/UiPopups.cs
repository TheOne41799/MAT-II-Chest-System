using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public enum UIPopups
    {
        NONE,
        UI_CHEST_UNLOCK_POPUP,
        UI_CHEST_SLOTS_FULL_POPUP,
        UI_CHEST_ALREADY_UNLOCKING,
        UI_CHEST_ADDED_TO_QUEUE,
        UI_CHEST_ALREADY_QUEUED,
        UI_CHEST_UNLOCKED_WITH_GEMS,
        UI_CHEST_ALREADY_UNLOCKED,
        UI_PLAYER_HAS_INSUFFICIENT_GEMS,
        UI_COLLECT_REWARDS_OR_UNDO_CHEST_UNLOCK,
        UI_UNDO_CHEST_UNLOCK_WITH_GEMS,
        UI_CANT_UNDO_CHEST_UNLOCKED_WITH_TIMER
    }
}
