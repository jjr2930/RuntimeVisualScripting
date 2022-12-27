using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    /// <summary>
    /// 모든 이항 연산자의 부모 클래스
    /// </summary>
    /// <typeparam name="InputType1"></typeparam>
    /// <typeparam name="InputType2"></typeparam>
    /// <typeparam name="OutputType"></typeparam>
    public abstract class BinaryOperator<InputType1, InputType2, OutputType> : ArithmeticNode
    {
        protected InputVariable<InputType1> inputA = new InputVariable<InputType1>();
        protected InputVariable<InputType2> inputB = new InputVariable<InputType2>();
        protected OutputVariable<OutputType> output = new OutputVariable<OutputType>();

        public InputVariable<InputType1> InputA { get => inputA; set => inputA = value; }
        public InputVariable<InputType2> InputB { get => inputB; set => inputB = value; }
        public OutputVariable<OutputType> Output { get => output; set => output = value; }

        public BinaryOperator()
        {
            inputA.Parent = this;
            inputB.Parent = this;
            output.Parent = this;

            inputA.Name = "A";
            inputB.Name = "B";
            output.Name = "Out";
        }

        public override List<Variable> GetInputVariables()
        {
            return new List<Variable>() { inputA, inputB };
        }

        public override List<Variable> GetOutputVariables()
        {
            return new List<Variable>() { output };
        }

        public override void Serialize(VisualScriptStream stream)
        {
            stream.AddArithmeticNode(GUID, DisplayName, this.GetType(), Position,
                new Variable[2] { InputA, InputB }, new Variable[1] { output });

            inputA.Serialize(stream);
            inputB.Serialize(stream);
            output.Serialize(stream);
        }
    }
}
