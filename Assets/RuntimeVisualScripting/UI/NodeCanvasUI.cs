using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeVisualScripting.UI
{
    public class NodeCanvasUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]
        ContextMenuBase contextMenu = null;

        [SerializeField]
        VisualScriptUI visualScriptUI = null;

        ContextMenuItemInfo[] infos = null;

        Vector2 lastClickedPoint = Vector2.zero;

        GraphicRaycaster raycaster = null;

        public void Reset()
        {
            visualScriptUI = GetComponentInParent<VisualScriptUI>();
        }

        public void Start()
        {
            raycaster = GetComponentInParent<GraphicRaycaster>();

            infos = new ContextMenuItemInfo[1]
            {
                new ContextMenuItemInfo("AddInt", 0, typeof(AddInt))
            };
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("Hello "+ this.name);
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);
            bool nodeHovered = false;
            for (int i = 0; i < results.Count; i++)
            {
                var node = results[i].gameObject.GetComponent<IgnoringContextMenuItem>();
                if (null != node)
                {
                    nodeHovered = true;
                    break;
                }
            }

            if (nodeHovered)
                return;

            lastClickedPoint = eventData.position;
            contextMenu.Toggle(eventData.position, infos);
            contextMenu.OnItemClicked =
                (index) =>
                {
                    visualScriptUI.AddNode(Activator.CreateInstance(infos[index].type) as Node, lastClickedPoint);
                    contextMenu.Close();
                };
        }

        /// <summary>
        /// 아무것도 하는 일이 없지만 이것을 넣지 않으면 onPointUp이 작동하지 않는다
        /// 뭔가 이상하다
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("Hello " + this.name);
        }
    }
}
