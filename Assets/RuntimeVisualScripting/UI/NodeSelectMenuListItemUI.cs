using RuntimeVisualScripting.Data;
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
        Node node = null;
        public Node Node 
        {
            get { return node; }
            set
            {
                node = value;
                nameText.text = Node.DisplayName;
            }
        }

        [SerializeField]
        TextMeshProUGUI nameText = null;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            var nodeUI = copiedTransform.GetComponent<NodeSelectMenuListItemUI>();
            nodeUI.node = this.node;
        }
    }
}
