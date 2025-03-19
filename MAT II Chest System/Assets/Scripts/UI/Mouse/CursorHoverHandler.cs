using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChestSystem.UI
{
    public class CursorHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static CursorManager cursorManager;

        public static void Initialize(CursorManager manager)
        {
            cursorManager = manager;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (cursorManager != null)
                cursorManager.SetHoverCursor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (cursorManager != null)
                cursorManager.SetDefaultCursor();
        }
    }
}