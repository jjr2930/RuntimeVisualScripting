using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class AddInt : BinaryOperator<int, int, int>
    {
        public override string DisplayName => "Add Int";
        public override void Calculate()
        {
            Output.Value = InputA.Value + InputB.Value;
        }
    }
}
