using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class AddString : BinaryOperator<string, string, string>
    {
        public override string DisplayName => "Add String";
        public override void Calculate()
        {
            Output.Value = InputA.Value + InputB.Value;
        }
    }
}
