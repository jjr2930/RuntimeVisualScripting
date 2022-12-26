using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class NodeCanvasDragReceiver : DragReceiver
    {
        [SerializeField]
        NodeCanvasUI nodeCanvas = null;
        public override void OnDragEnd(DraggableObject obj, Vector2 screenPoint)
        {
            base.OnDragEnd(obj, screenPoint);
            var nodeDragUI = obj.GetComponent<NodeSelectMenuListItemUI>();
            nodeCanvas.AddNode(nodeDragUI.Node, screenPoint);
        }
    }
}
