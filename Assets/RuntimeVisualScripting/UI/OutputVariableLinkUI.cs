using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class OutputVariableLinkUI : MultiLinkUI
    {
        [Header("Ouput Variable link UI")]
        [SerializeField]
        VariableUI variableUI = null;

        public VariableUI VariableUI { get { return variableUI; } }
    }
}
