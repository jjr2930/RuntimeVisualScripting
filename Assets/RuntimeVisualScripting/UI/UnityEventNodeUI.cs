using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class UnityEventNodeUI : NodeUI
    {
        [Header("Unity Event Node UI")]
        [SerializeField]
        UnityEventType eventType;

        public override void BuildNode(Node node)
        {
            base.BuildNode(node);
            if (false == node is UnityEventNode)
            {
                throw new NotImplementedException("wrong situation");
            }

            UnityEventNode unityEventNode = node as UnityEventNode;
            eventType = unityEventNode.EventType;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            visualScriptUI.OnNodeMoved(this);
        }
    }
}
