using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeVisualScripting.Data;
using TMPro;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class NodeUI : DragableUI
    {
        [Header("References")]
        [SerializeField]
        protected TextMeshProUGUI title = null;

        [SerializeField]
        protected VisualScriptUI visualScriptUI = null;

        private Node node = null;
        public Node Node 
        {
            get => node;
            set
            {
                node = value;
                title.text = node.DisplayName;

                BuildNode(value);
            }
        }

        public void Start()
        {
            visualScriptUI = GetComponentInParent<VisualScriptUI>();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            visualScriptUI.NodeMoved(this);
        }

        public virtual void BuildNode(Node node) { }
    }
}
