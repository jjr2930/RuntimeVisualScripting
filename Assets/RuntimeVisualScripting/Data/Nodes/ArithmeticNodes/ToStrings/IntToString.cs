using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class IntToString : UnaryOperator<int, string>
    {
        public override string DisplayName => "ToString(int)";
        public override void Calculate()
        {
            Output.Value = Input.Value.ToString();
        }
    }
}
