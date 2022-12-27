using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.UI
{
    public class VisualScriptUI : MonoBehaviour
    {
        [SerializeField]
        RectTransform nodeCanvasContent = null;

        [SerializeField]
        List<NodeUI> generatedNodeUIs = new List<NodeUI>();

        [SerializeField]
        List<LinkLineUI> linkLines = new List<LinkLineUI>();

        [SerializeField]
        NodeUI nodeForCopy = null;

        [SerializeField]
        NodeUI arithmeticUI = null; 

        [SerializeField]
        LinkLineUI linkLineUIForCopy = null;

        [SerializeField]
        NodeSelectionMenuUI nodeSelectionMenuUI = null;

        [SerializeField]
        UnityEventNodeUI unityEventNodeForCopy = null;

        VisualScript visualScript = new VisualScript();

        public VisualScript VisualScript
        {
            get
            {
                return visualScript;
            }

            set
            {
                visualScript = value;
            }
        }

        public void Start()
        {
            List<Node> testNodes = new List<Node>()
            {
                new AddInt(),
                new AddString(),
            };
            nodeSelectionMenuUI.BuildNodes(testNodes);

            //create start node
            var startNode = Instantiate(unityEventNodeForCopy, nodeCanvasContent);
            startNode.gameObject.SetActive(true);
            startNode.Node = new UnityEventNode() { EventType = UnityEventType.Start };

            //create update node
            var updateNode = Instantiate(unityEventNodeForCopy, nodeCanvasContent);
            updateNode.gameObject.SetActive(true);
            updateNode.Node = new UnityEventNode() { EventType = UnityEventType.Update };
        }

        public void AddNode(Node node, Vector2 screenPosition)
        {
            node.Position = screenPosition;
            visualScript.AddNode(node);

            NodeUI newNodeUI = null;
            if(node is ArithmeticNode)
                newNodeUI = Instantiate(arithmeticUI, nodeCanvasContent);
            else
                newNodeUI = Instantiate(nodeForCopy, nodeCanvasContent);

            newNodeUI.Node = node;
            newNodeUI.transform.position = screenPosition;
            newNodeUI.gameObject.SetActive(true);

            generatedNodeUIs.Add(newNodeUI);
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

        public void OnNodeMoved(NodeUI movedNode)
        {
            for (int i = 0; i < linkLines.Count; i++)
            {
                linkLines[i].ReCalculate(movedNode);
            }
        }

        public void OnNodeCanvasScrollMoved( Vector2 scroll)
        {
            for (int i = 0; i < linkLines.Count; i++)
            {
                linkLines[i].ReCalculate();
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

        public void RemoveNode(NodeUI oldNodeUI)
        {
            for (int i = 0; i < linkLines.Count; i++)
            {
                if(linkLines[i].From.ParentNodeUI == oldNodeUI)
                {
                    Destroy(linkLines[i].gameObject);
                    linkLines.RemoveAt(i);
                    i--;
                }
                else if (linkLines[i].To.ParentNodeUI == oldNodeUI)
                {
                    Destroy(linkLines[i].gameObject);
                    linkLines.RemoveAt(i);
                    i--;
                }
            }

            visualScript.RemoveNode(oldNodeUI.Node);
            generatedNodeUIs.Remove(oldNodeUI);
            Destroy(oldNodeUI.gameObject);
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Serialization"))
            {
                VisualScriptStream stream = new VisualScriptStream();
                visualScript.Serialize(stream);

                string json = JsonUtility.ToJson(stream, true);
                Debug.Log(json);
            }
        }
    }
}
