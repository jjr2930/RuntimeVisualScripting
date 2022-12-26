using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class InputVariableLinkUI : SingleLinkUI
    {
        [SerializeField]
        VariableUI variableUI = null;

        public override bool CanConnection(LinkUI checkingTarget)
        {
            base.CanConnection(checkingTarget);
            var outVariableLink = checkingTarget as OutputVariableLinkUI;
            if (null == outVariableLink)
                return false;
            
            var myValueType = variableUI.Variable.ValueType;
            var targetValueType = outVariableLink.VariableUI.Variable.ValueType;
            var isOtherType = myValueType != targetValueType;

            return isOtherType;
        }
    }
}