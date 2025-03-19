using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChestSystem.UI
{
    [CreateAssetMenu(fileName = "CursorData", menuName = "Cursor/Cursor Data")]
    public class CursorDataSO : ScriptableObject
    {
        public Texture2D defaultCursor;
        public Vector2 defaultHotspot;

        public Texture2D hoverCursor;
        public Vector2 hoverHotspot;
    }
}