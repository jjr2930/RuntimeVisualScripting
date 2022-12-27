using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    /// <summary>
    /// 단항 연산자들의 부모클래스
    /// </summary>
    /// <typeparam name="InputType"></typeparam>
    /// <typeparam name="OutputType"></typeparam>
    public abstract class UnaryOperator<InputType,OutputType> : ArithmeticNode
    {
        protected InputVariable<InputType> input = new InputVariable<InputType>();
        protected OutputVariable<OutputType> output = new OutputVariable<OutputType>();

        public InputVariable<InputType> Input { get => input; set => input = value; }
        public OutputVariable<OutputType> Output { get => output; set => output = value; }

        public UnaryOperator()
        {
            input.Parent = this;
            output .Parent = this;
        }

        public override List<Variable> GetOutputVariables()
        {
            return new List<Variable>() { input };
        }

        public override List<Variable> GetInputVariables()
        {
            return new List<Variable>() { output };
        }
        public override void Deserialize(VisualScriptStream stream)
        {
            throw new System.NotImplementedException();
        }

        public override void Serialize(VisualScriptStream stream)
        {
            stream.AddArithmeticNode(GUID, DisplayName, GetType(), Position,
                new Variable[1] { input }, new Variable[1] { output });

            input.Serialize(stream);
            output.Serialize(stream);
        }
    }
}
