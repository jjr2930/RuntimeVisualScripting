using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class ExecutionNodeUI : NodeUI
    {
        [Header("ExecutionNodeUI")]
        [SerializeField]
        InputVariableUI originInputVariable;

        [SerializeField]
        Transform inputContentRoot = null;

        [SerializeField]
        OutputVariableUI originOutputVariable;

        [SerializeField]
        Transform outputContentRoot = null;

        public override void BuildNode(Node node)
        {
            base.BuildNode(node);

            var outputs = node.GetOutputVariables();
            var inputs = node.GetInputVariables();

            for (int i = 0; i < inputs.Count; i++)
            {
                var newInput = Instantiate(originInputVariable, inputContentRoot);
                newInput.gameObject.SetActive(true);
                newInput.Variable = inputs[i];
                newInput.SetName(inputs[i].Name);
            }

            for (int i = 0; i < outputs.Count; i++)
            {
                var newOutput = Instantiate(originOutputVariable, outputContentRoot);
                newOutput.gameObject.SetActive(true);
                newOutput.Variable = outputs[i];
                newOutput.SetName(outputs[i].Name);
            }
        }
    }
}
