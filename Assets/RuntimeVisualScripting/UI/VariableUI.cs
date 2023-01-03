using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class VariableUI : MonoBehaviour
    {
        [SerializeField]
        protected VariableLinkUI linkUI = null;

        [SerializeField]
        protected TextMeshProUGUI nameText = null;

        public VariableLinkUI LinkUI { get { return linkUI; } }

        Variable variable = null;
        public Variable Variable 
        {
            get => variable;
            set
            {
                variable = value;
                var visualScriptUI = GetComponentInParent<VisualScriptUI>();
                visualScriptUI.UpdateVariableLink(variable.Id, linkUI);
            }
        }

        public void SetName(string value)
        {
            nameText.text = value;
        }
    }
}
