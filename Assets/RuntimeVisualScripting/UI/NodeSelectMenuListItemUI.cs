using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class NodeSelectMenuListItemUI : DraggableObject
    {
        [SerializeField]
        Type nodeType = null;
        public Type NodeType 
        {
            get { return nodeType; }
            set
            {
                nodeType = value;
                nameText.text = NodeType.ToString();
            }
        }

        [SerializeField]
        TextMeshProUGUI nameText = null;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            var nodeUI = copiedTransform.GetComponent<NodeSelectMenuListItemUI>();
            nodeUI.nodeType = this.nodeType;
        }
    }
}
