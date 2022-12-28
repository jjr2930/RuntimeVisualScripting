using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RuntimeVisualScripting.Data
{
    /// <summary>
    /// 단항 연산자들의 부모클래스
    /// </summary>
    /// <typeparam name="InputType"></typeparam>
    /// <typeparam name="OutputType"></typeparam>
    [Serializable]
    public abstract class UnaryOperator<InputType,OutputType> : ArithmeticNode
    {
        [SerializeField]
        long inputId = 0;

        [SerializeField]
        long outputId = 0;

        public InputVariable<InputType> Input { get; set; }
        public OutputVariable<OutputType> Output { get; set; }

        public UnaryOperator()
        {
            Input = new InputVariable<InputType>();
            Output = new OutputVariable<OutputType>();

            Input.Parent = this;
            Output.Parent = this;
        }

        public override List<Variable> GetOutputVariables()
        {
            return new List<Variable>() { Input };
        }

        public override List<Variable> GetInputVariables()
        {
            return new List<Variable>() { Output };
        }
        public override void Serialize(VisualScriptStream stream)
        {
            inputId = Input.Id;
            outputId = Output.Id;

            base.Serialize(stream);

            Input.Serialize(stream);
            Output.Serialize(stream);
        }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            Input = objectMap[inputId] as InputVariable<InputType>;
            Output = objectMap[outputId] as OutputVariable<OutputType>;
        }
    }
}
