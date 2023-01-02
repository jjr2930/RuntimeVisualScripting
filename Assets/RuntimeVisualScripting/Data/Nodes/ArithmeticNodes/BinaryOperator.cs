using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace RuntimeVisualScripting.Data
{
    /// <summary>
    /// 모든 이항 연산자의 부모 클래스
    /// </summary>
    /// <typeparam name="InputType1"></typeparam>
    /// <typeparam name="InputType2"></typeparam>
    /// <typeparam name="OutputType"></typeparam>
    [Serializable]
    public abstract class BinaryOperator<InputType1, InputType2, OutputType> : ArithmeticNode
    {
        [SerializeField]
        protected long inputAId = 0;

        [SerializeField]
        protected long inputBId = 0;

        [SerializeField]
        protected long outputId = 0;

        public InputVariable<InputType1> InputA { get; set; }
        public InputVariable<InputType2> InputB { get; set; }
        public OutputVariable<OutputType> Output { get; set; }

        public BinaryOperator() { }

        public override void Initialize()
        {
            InputA = new InputVariable<InputType1>();
            InputB = new InputVariable<InputType2>();
            Output = new OutputVariable<OutputType>();

            InputA.Parent = this;
            InputB.Parent = this;
            Output.Parent = this;

            InputA.Name = "A";
            InputB.Name = "B";
            Output.Name = "Out";
        }

        public override List<Variable> GetInputVariables()
        {
            return new List<Variable>() { InputA, InputB };
        }

        public override List<Variable> GetOutputVariables()
        {
            return new List<Variable>() { Output };
        }

        public override void Serialize(VisualScriptStream stream)
        {
            inputAId = InputA.Id;
            inputBId = InputB.Id;
            outputId = Output.Id;

            base.Serialize(stream);
            
            InputA.Serialize(stream);
            InputB.Serialize(stream);
            Output.Serialize(stream);
        }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            InputA = objectMap[inputAId] as InputVariable<InputType1>;
            InputB = objectMap[inputBId] as InputVariable<InputType2>;
            Output = objectMap[outputId] as OutputVariable<OutputType>;
        }
    }
}
