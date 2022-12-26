using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class OutputVariableLinkUI : MultiLinkUI
    {
        [SerializeField]
        VariableUI variableUI = null;

        public VariableUI VariableUI { get { return variableUI; } }
    }
}
