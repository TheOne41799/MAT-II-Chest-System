using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIManager : MonoBehaviour
    {
        // this script currently only manages the cursor graphic change
        // if needed this can be scaled to use something like UI Tween
        [SerializeField] private CursorDataSO cursorDataSO;
        private CursorManager cursorManager;

        private void Awake()
        {
            cursorManager = new CursorManager(cursorDataSO);
            CursorHoverHandler.Initialize(cursorManager);
        }
    }
}
