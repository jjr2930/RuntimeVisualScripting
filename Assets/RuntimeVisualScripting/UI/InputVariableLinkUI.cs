using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class InputVariableLinkUI : SingleLinkUI
    {
        public override bool CanConnection(LinkUI checkingTarget)
        {
            base.CanConnection(checkingTarget);
            var outVariableLink = checkingTarget as OutputVariableLinkUI;
            if (null == outVariableLink)
                return false;
            
            var myValueType = variableUI.Variable.ValueType;
            var targetValueType = outVariableLink.VariableUI.Variable.ValueType;
            var isSameType = myValueType == targetValueType;

            return isSameType;
        }

        //public override void SetOrAddTargetLink(LinkUI newLink)
        //{
        //    base.SetOrAddTargetLink(newLink);

        //    if (false == newLink is OutputVariableLinkUI)
        //    {
        //        Debug.Log("new link is not outputvariable link ui");
        //        return;
        //    }
        //} 
    }
}
