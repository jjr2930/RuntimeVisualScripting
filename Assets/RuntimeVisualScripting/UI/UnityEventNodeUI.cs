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

        public override Node Node
        {
            get => node;
            set
            {
                if (false == value is UnityEventNode)
                    throw new NotImplementedException("value is not UnityEventNode");

                node = value;
                UnityEventNode unityEventNode = node as UnityEventNode;
                eventType = unityEventNode.EventType;
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            visualScriptUI.OnNodeMoved(this);
        }
    }
}
