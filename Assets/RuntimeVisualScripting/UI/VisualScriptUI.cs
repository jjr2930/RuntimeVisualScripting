using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class VisualScriptUI : MonoBehaviour
    {
        [SerializeField]
        RectTransform nodeCanvasContent = null;

        [SerializeField]
        List<LinkLineUI> linkLines = new List<LinkLineUI>();

        [Header("Reference for copy")]
        [SerializeField]
        NodeUI nodeForCopy = null;

        [SerializeField]
        LinkLineUI linkLineUIForCopy = null;

        VisualScript visualScript = null;

        public void AddNode(Node node, Vector2 screenPoint)
        {
            var newNodeUI = Instantiate(nodeForCopy, nodeCanvasContent);
            newNodeUI.transform.position = screenPoint;
            newNodeUI.Node = node;
            newNodeUI.gameObject.SetActive(true);
        }

        public void AddLinkLine(LinkLineUI newLineLink)
        {
            if (linkLines.Contains(newLineLink))
                return;
                
            linkLines.Add(newLineLink);
        }

        public void RemoveLinkLine(LinkLineUI oldLinkLine)
        {
            if (false == linkLines.Contains(oldLinkLine))
                return;

            linkLines.Remove(oldLinkLine);
        }

        public void NodeMoved(NodeUI movedNode)
        {
            for (int i = 0; i < linkLines.Count; i++)
            {
                linkLines[i].ReCalculate(movedNode);
            }
        }

        public void OnLinkUIBeginDrag(LinkUI pressedLinkUI)
        {
            LinkLineUI linkLineUI = null;

            if (pressedLinkUI is SingleLinkUI)
                linkLineUI = GetLinkLineUI(pressedLinkUI);

            else if (pressedLinkUI is MultiLinkUI)
                linkLineUI = GetNewLinkLineUI();

            if(null != linkLineUI.From && null != linkLineUI.To)
                LinkUI.DisconnectTwoWay(linkLineUI.From, linkLineUI.To);

            linkLineUI.From = pressedLinkUI;
            linkLineUI.To = null;
        }

        public void OnLinkUIDrag(LinkUI draggedLinkUI, Vector2 screenPoint)
        {
            var linkLineUI = GetLinkLineUI(draggedLinkUI);
            linkLineUI.SetToPosition(screenPoint);
        }

        public void OnLinkUIEndDrag(LinkUI releasedLinkUI, LinkUI hoveredLinkUI)
        {
            var linkLineUI = GetLinkLineUI(releasedLinkUI);
            if(null == hoveredLinkUI)
            {
                Destroy(linkLineUI.gameObject);
            }
            else
            {
                linkLineUI.To = hoveredLinkUI;
                LinkUI.ConnectTwoWay(releasedLinkUI, hoveredLinkUI);
            }
        }

        public LinkLineUI GetLinkLineUI(LinkUI target)
        {
            LinkLineUI found = null;
            for (int i = 0; i < linkLines.Count; i++)
            {
                if (target == linkLines[i].From)
                    found = linkLines[i];

                else if (target == linkLines[i].To)
                    found = linkLines[i];
            }

            if(null == found)
            {
                found = GetNewLinkLineUI();
            }

            return found;
        }

        public LinkLineUI GetNewLinkLineUI()
        {
            LinkLineUI result = null;
            result = Instantiate(linkLineUIForCopy, nodeCanvasContent);
            result.gameObject.SetActive(true);

            return result;
        }
    }
}
