using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class ContextMenuItemUI : MonoBehaviour
    {
        [SerializeField]
        ContextMenuBase contextMenu = null;

        [SerializeField]
        TextMeshProUGUI text = null;

        int index;

        public void SetMember(string value, int index)
        {
            this.index = index;
            text.text = value;
        }

        public void OnClicked()
        {
            //contextMenu.OnItemClicked?.Invoke(this.index);
        }
    }
}
