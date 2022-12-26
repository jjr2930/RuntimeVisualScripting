using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class DragReceiver : MonoBehaviour
    {
        public virtual void OnDragEnd(DraggableObject obj, Vector2 screenPoint) { }
    }
}
