using RuntimeVisualScripting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class VariableLinkUI : LinkUI
    {
        /// <summary>
        /// true : inputVariable, false : outputVariable
        /// </summary>
        [SerializeField]
        bool isInput = false;
        public bool IsInput { get => isInput; }

        [SerializeField]
        protected VariableUI variableUI;
        public VariableUI VariableUI 
        {
            get => variableUI;
            set
            {
                variableUI = value;
                visualScriptUI.UpdateVariableLink(VariableId, this);
            }
        }
        public long VariableId { get { return VariableUI.Variable.Id; } }

        protected Variable Variable { get => variableUI.Variable; }
        public void OnDestroy()
        {
            visualScriptUI.RemoveVariableLink(this);
        }

        public override bool CanConnection(LinkUI checkingTarget)
        {
            if (checkingTarget == this)
                return false;

            if (false == checkingTarget is VariableLinkUI)
                return false;

            var targetVariableLinkUI = checkingTarget as VariableLinkUI;
            if (isInput == targetVariableLinkUI.isInput)
                return false;

            var thisValueType = VariableUI.Variable.ValueType;
            var targetValueType = targetVariableLinkUI.VariableUI.Variable.ValueType;

            if (thisValueType != targetValueType)
                return false;

            var thisLinkable = Variable as ILinkable;
            var targetLinkable = targetVariableLinkUI.Variable as ILinkable;

            if (thisLinkable.AlreadyConnected(targetLinkable))
            {
                Debug.Log("already connected");
                return false;
            }

            return true;
        }

        public override void LinkOneWay(LinkUI other)
        {
            base.LinkOneWay(other);

            var otherVariableLinkUI = other as VariableLinkUI;

            ILinkable fromLink = VariableUI.Variable as ILinkable;
            ILinkable otherLink = otherVariableLinkUI.VariableUI.Variable as ILinkable;

            fromLink.LinkOneWay(otherLink);
        }

        public override void LinkTwoWay(LinkUI other)
        {
            base.LinkTwoWay(other);

            var otherVariableLinkUI = other as VariableLinkUI;

            ILinkable fromLink = VariableUI.Variable as ILinkable;
            ILinkable otherLink = otherVariableLinkUI.VariableUI.Variable as ILinkable;

            fromLink.LinkTwoWay(otherLink);
        }
    }
}
