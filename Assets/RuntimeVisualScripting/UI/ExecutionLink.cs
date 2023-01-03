using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class ExecutionLink : LinkUI
    {
        public override bool CanConnection(LinkUI checkingTarget)
        {
            return checkingTarget is ExecutionLink;
        }
    }
}
