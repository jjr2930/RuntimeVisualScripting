using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    /// <summary>
    /// ���� �����ڵ��� �θ�Ŭ����
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
