using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class AddFloat : BinaryOperator<float,float,float>
    {
        public override string DisplayName => "Add Float";
        public override void Calculate()
        {
            output.Value = inputA.Value + inputB.Value;
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
