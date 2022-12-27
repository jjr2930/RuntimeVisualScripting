using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class AddString : BinaryOperator<string, string, string>
    {
        public override string DisplayName => "Add String";
        public override void Calculate()
        {
            output.Value = inputA.Value + inputB.Value;
        }

        public override void Deserialize(VisualScriptStream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
