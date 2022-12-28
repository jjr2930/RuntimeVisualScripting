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
            Output.Value = InputA.Value + InputB.Value;
        }
    }
}
