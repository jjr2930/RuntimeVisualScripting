using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class Log : ExecutionNode
    {
        InputVariable<string> input = new InputVariable<string>();
        public InputVariable<string> Input { get { return input; } set { input = value; } }

        public override List<Variable> GetInputVariables()
        {
            return new List<Variable>() { input };
        }

        public override List<Variable> GetOutputVariables()
        {
            return null;
        }

        public override void Run()
        {
            Debug.Log(input.Value);
        }

        public override void Deserialize(VisualScriptStream stream)
        {
            throw new System.NotImplementedException();
        }

        public override void Serialize(VisualScriptStream stream)
        {
            stream.AddExecuteNode(GUID, DisplayName, GetType(), Position, previousNodeId, nextNodeId,
                new Variable[1] { input }, null);

            input.Serialize(stream);
        }
    }
}
