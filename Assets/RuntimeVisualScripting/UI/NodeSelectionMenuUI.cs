using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using RuntimeVisualScripting.Data;

namespace RuntimeVisualScripting.UI
{
    public class NodeSelectionMenuUI : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField searchInput = null;

        [SerializeField]
        NodeSelectMenuListItemUI nodeUIToCopy = null;

        [SerializeField]
        Transform nodeContentRoot = null;

        [SerializeField]
        List<NodeSelectMenuListItemUI> generatedNodes = new List<NodeSelectMenuListItemUI>();

        [SerializeField]
        List<UnityEventNodeUI> generatedUnityEventNodes = new List<UnityEventNodeUI>();  

        public Action<string> onSearchInputChanged = null;

        public void BuildNodes(List<Node> nodes)
        {
            int count = nodes.Count - generatedNodes.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    generatedNodes.Add(Instantiate(nodeUIToCopy, nodeContentRoot));
                }
            }

            for (int i = 0; i < generatedNodes.Count; i++)
            {
                if (i < nodes.Count)
                {
                    generatedNodes[i].gameObject.SetActive(true);
                    generatedNodes[i].Node = nodes[i];
                }
                else
                    generatedNodes[i].gameObject.SetActive(false);
            }
        }

        public void OnSearchTextChanged(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                for (int i = 0; i < generatedNodes.Count; i++)
                {
                    generatedNodes[i].gameObject.SetActive(true);
                }
            }

            for (int i = 0; i < generatedNodes.Count; i++)
            {
                bool onOff = generatedNodes[i].Node.DisplayName.ToUpper().Contains(text.ToUpper());
                generatedNodes[i].gameObject.SetActive(onOff);
            }
        }
    }
}