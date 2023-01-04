using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class ParameterizedNodeUI : NodeUI
    {
        [SerializeField]
        protected VariableUI originInputVariable;

        [SerializeField]
        protected Transform inputContentRoot = null;

        [SerializeField]
        protected VariableUI originOutputVariable;

        [SerializeField]
        protected Transform outputContentRoot = null;

        [SerializeField]
        public List<VariableUI> inputVariableUIs = new List<VariableUI>();

        [SerializeField]
        public List<VariableUI> outputVariableUIs = new List<VariableUI>();

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

            while (0 < outputVariableUIs.Count)
            {
                Destroy(outputVariableUIs[0].gameObject);
                outputVariableUIs.RemoveAt(0);
            }
        }
    }
}
