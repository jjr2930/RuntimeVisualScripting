using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RuntimeVisualScripting.UI
{
    public class OutputVariableUI : VariableUI
    {
        [SerializeField]
        TextMeshProUGUI nameText = null;

        public void SetName(string value)
        {
            nameText.text = value;
        }
    }
}
