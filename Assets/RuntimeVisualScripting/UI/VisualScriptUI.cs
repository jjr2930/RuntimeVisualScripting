using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions.Must;
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

        [SerializeField]
        TextAsset serializedVisualScript = null;

        VisualScript visualScript = null;

        Dictionary<long, LinkUI> linkByVariables = new Dictionary<long, LinkUI>();
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

        public void Awake()
        {
            visualScript = new VisualScript();
            visualScript.onNodeDeserialized +=
                (newNode) =>
                {
                    AddNodeUI(newNode, newNode.Position);
                };

            visualScript.onInputVariableDeserialized +=
                (a) =>
                {
                    StartCoroutine(OnInputVariableDeserializedDelayed(a));
                };

        }

        public void Start()
        {
            List<Type> nodes = new List<Type>()
            {
                typeof(AddInt),
                typeof(AddFloat)
            };

            nodeSelectionMenuUI.BuildNodes(nodes);
            //return;
            //VisualScriptStream stream
            //    = JsonUtility.FromJson<VisualScriptStream>(serializedVisualScript.text);

            //visualScript.Deserialize(stream);
        }

        IEnumerator OnInputVariableDeserializedDelayed(Variable a)
        {
            yield return new WaitForEndOfFrame();
            ILinkable aLink = a as ILinkable;
            if (false == aLink.HasLink)
                yield break;

            ILinkable bLink = aLink.GetTarget(0);

            Variable b = bLink as Variable;
            OnLinkUIBeginDrag(linkByVariables[a.Id]);
            OnLinkUIEndDrag(linkByVariables[a.Id], linkByVariables[b.Id]);
        }
        public void AddNode(Node node, Vector2 screenPosition)
        {
            node.Position = screenPosition;
            visualScript.AddNode(node);

            AddNodeUI(node, screenPosition);
        }

        private void AddNodeUI(Node node, Vector2 screenPosition)
        {
            NodeUI newNodeUI = null;
            if (node is ArithmeticNode)
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

            //if(null != linkLineUI.From && null != linkLineUI.To)
            //    LinkUI.DisconnectTwoWay(linkLineUI.From, linkLineUI.To);

            linkLineUI.From = pressedLinkUI;
            linkLineUI.To = null;
        }

        public void OnLinkUIDrag(LinkUI draggedLinkUI, Vector2 screenPoint)
        {
            var linkLineUI = GetLinkLineUI(draggedLinkUI);
            linkLineUI.SetToPosition(screenPoint);
        }

        public void OnLinkUIEndDrag(LinkUI from, LinkUI hovered)
        {
            var linkLineUI = GetLinkLineUI(from);
            if(null == hovered)
            {
                Destroy(linkLineUI.gameObject);
            }
            else
            {
                linkLineUI.To = hovered;
                ILinkable fromLinkable = from.VariableUI.Variable as ILinkable;
                ILinkable hoveredLinkable = hovered.VariableUI.Variable as ILinkable;

                fromLinkable.LinkTwoWay(hoveredLinkable);
                //LinkUI.ConnectTwoWay(from, hovered);
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

        public void AddLinkUI(LinkUI newLinkUI)
        {
            linkByVariables.Add(newLinkUI.VariableId, newLinkUI);
        }

        public void RemoveLinkUI(LinkUI oldLinkUI)
        {
            linkByVariables.Remove(oldLinkUI.VariableId);
        }

        public void UpdateLinkUIMap(LinkUI source)
        {
            foreach (var item in linkByVariables)
            {
                if(item.Value == source)
                {
                    linkByVariables.Remove(item.Key);
                    break;
                }
            }

            linkByVariables.Add(source.VariableId, source);
        }

        public void Clear()
        {
            while (0 < generatedNodeUIs.Count)
            {
                var oldNodeUI = generatedNodeUIs[0];
                generatedNodeUIs.RemoveAt(0);
                Destroy(oldNodeUI.gameObject);
            }

            while ( 0 < linkLines.Count)
            {
                var oldLine = linkLines[0];
                linkLines.RemoveAt(0);
                Destroy(oldLine.gameObject);
            }

            visualScript.Clear();
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

            if (GUILayout.Button("Deserializaion"))
            {
                Clear();

                VisualScriptStream stream 
                    = JsonUtility.FromJson<VisualScriptStream>(serializedVisualScript.text);

                visualScript.Clear();
                visualScript.Deserialize(stream);
            }
        }
    }
}
