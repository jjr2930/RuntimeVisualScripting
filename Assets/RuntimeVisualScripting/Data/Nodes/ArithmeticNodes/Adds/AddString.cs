using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class AddString : BinaryOperator<string, string, string>
    {
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
