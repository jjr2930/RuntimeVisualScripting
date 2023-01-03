using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class NodeListItemUI : DraggableObject
    {
        [SerializeField]
        Type nodeType = null;
        public Type NodeType 
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        [SerializeField]
        TextMeshProUGUI nameText = null;

        public void SetNameText(string text)
        {
            nameText.text = text;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            var nodeUI = copiedTransform.GetComponent<NodeListItemUI>();
            nodeUI.nodeType = this.nodeType;
        }
    }
}
