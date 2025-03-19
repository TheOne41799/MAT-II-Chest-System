using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CursorDataSO cursorDataSO;
        private CursorManager cursorManager;

        private void Awake()
        {
            cursorManager = new CursorManager(cursorDataSO);
            CursorHoverHandler.Initialize(cursorManager);
        }
    }
}
