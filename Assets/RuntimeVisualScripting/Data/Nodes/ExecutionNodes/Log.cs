using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
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

        protected override void Deserialize(DeserializeStream stream)
        {
            throw new System.NotImplementedException();
        }

        protected override void Serialize(SerializeStream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
