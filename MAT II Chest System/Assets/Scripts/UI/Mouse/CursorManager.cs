using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class CursorManager
    {
        // to change the cursor graphic when mouse is hovering over certain ui elements
        private CursorDataSO cursorData;

        public CursorManager(CursorDataSO cursorData)
        {
            this.cursorData = cursorData;
            SetDefaultCursor();
        }

        public void SetDefaultCursor()
        {
            Cursor.SetCursor(cursorData.defaultCursor, cursorData.defaultHotspot, CursorMode.Auto);
        }

        public void SetHoverCursor()
        {
            Cursor.SetCursor(cursorData.hoverCursor, cursorData.defaultHotspot, CursorMode.Auto);
        }
    }
}