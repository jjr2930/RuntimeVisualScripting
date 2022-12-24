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
        InputVariableUI originInputVariable;

        [SerializeField]
        Transform inputContentRoot = null;

        [SerializeField]
        OutputVariableUI originOutputVariable;

        [SerializeField]
        Transform outputContentRoot = null;

        [SerializeField]
        TextMeshProUGUI title = null;

        [SerializeField]
        VisualScriptUI visualScriptUI = null;

        private Node node = null;
        public Node Node 
        {
            get => node;
            set
            {
                node = value;

                var outputs = node.GetOutputVariables();
                var inputs = node.GetInputVariables();

                for (int i = 0; i < inputs.Count; i++)
                {
                    var newInput = Instantiate(originInputVariable, inputContentRoot);
                    newInput.gameObject.SetActive(true);
                    newInput.SetName(inputs[i].Name);
                }

                for (int i = 0; i < outputs.Count; i++)
                {
                    var newOutput = Instantiate(originOutputVariable, outputContentRoot);
                    newOutput.gameObject.SetActive(true);
                    newOutput.SetName(outputs[i].Name);
                }

                title.text = node.Name;
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
    }
}
