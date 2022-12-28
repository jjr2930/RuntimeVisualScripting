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

        [SerializeField]
        protected InputVariableUI originInputVariable;

        [SerializeField]
        protected Transform inputContentRoot = null;

        [SerializeField]
        protected OutputVariableUI originOutputVariable;

        [SerializeField]
        protected Transform outputContentRoot = null;

        [SerializeField]
        public List<InputVariableUI> inputVariableUIs = new List<InputVariableUI>();

        [SerializeField]
        public List<OutputVariableUI> outputVariableUIs = new List<OutputVariableUI>();

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
            node.Position = eventData.position;
            visualScriptUI.OnNodeMoved(this);
        }

        public virtual void BuildNode(Node node)
        {
            Clear();
            var outputs = node.GetOutputVariables();
            var inputs = node.GetInputVariables();

            if (null != inputs)
            {
                for (int i = 0; i < inputs.Count; i++)
                {
                    var newInput = Instantiate(originInputVariable, inputContentRoot);
                    newInput.gameObject.SetActive(true);
                    newInput.Variable = inputs[i];
                    newInput.SetName(inputs[i].Name);
                    inputVariableUIs.Add(newInput);
                }
            }

            if (null != outputs)
            {
                for (int i = 0; i < outputs.Count; i++)
                {
                    var newOutput = Instantiate(originOutputVariable, outputContentRoot);
                    newOutput.gameObject.SetActive(true);
                    newOutput.Variable = outputs[i];
                    newOutput.SetName(outputs[i].Name);
                    outputVariableUIs.Add(newOutput);
                }
            }
        }

        public void Clear()
        {
            while (0 < inputVariableUIs.Count)
            {
                Destroy(inputVariableUIs[0].gameObject);
                inputVariableUIs.RemoveAt(0);
            }

            while(0< outputVariableUIs.Count)
            {
                Destroy(outputVariableUIs[0].gameObject);
                outputVariableUIs.RemoveAt(0);
            }
        }

        public void OnClickedDeleteButton()
        {
            visualScriptUI.RemoveNode(this);
        }
    }
}
