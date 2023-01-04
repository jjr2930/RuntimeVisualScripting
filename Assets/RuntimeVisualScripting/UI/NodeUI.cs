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
        [Header("NodeUI")]
        [SerializeField]
        protected TextMeshProUGUI title = null;

        [SerializeField]
        protected VisualScriptUI visualScriptUI = null;


        protected Node node = null;
        public virtual Node Node 
        {
            get => node;
            set
            {
                node = value;
                title.text = node.DisplayName;
            }
        }

        public void Start()
        {
            visualScriptUI = GetComponentInParent<VisualScriptUI>();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            node.Position = transform.position;
            visualScriptUI.OnNodeMoved(this);
        }

       

        public void OnClickedDeleteButton()
        {
            visualScriptUI.RemoveNode(this);
        }
    }
}
