using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class NodeCanvasUI : MonoBehaviour
    {
        [SerializeField]
        ContextMenuBase contextMenu = null;

        [SerializeField]
        VisualScriptUI visualScriptUI = null;

        ContextMenuItemInfo[] infos = null;
        
        public void Reset()
        {
            visualScriptUI = GetComponentInParent<VisualScriptUI>();
        }

        public void AddNode(Node node, Vector2 screenPosition)
        {
            visualScriptUI.AddNode(node,screenPosition);
        }
    }
}
