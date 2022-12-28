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

        public override void Serialize(VisualScriptStream stream)
        {
            base.Serialize(stream);

            input.Serialize(stream);
        }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            throw new NotImplementedException();
        }

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
    }
}
