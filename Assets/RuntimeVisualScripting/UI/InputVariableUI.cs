using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RuntimeVisualScripting.UI
{
    public class InputVariableUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI nameText = null;

        public void SetName(string name)
        {
            nameText.text = name;
        }
    }
}
