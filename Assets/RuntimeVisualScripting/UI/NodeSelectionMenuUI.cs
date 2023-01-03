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
        NodeListItemUI nodeUIToCopy = null;

        [SerializeField]
        Transform nodeContentRoot = null;

        [SerializeField]
        List<NodeListItemUI> generatedNodes = new List<NodeListItemUI>();

        [SerializeField]
        List<UnityEventNodeUI> generatedUnityEventNodes = new List<UnityEventNodeUI>();  

        public Action<string> onSearchInputChanged = null;

        public void BuildNodes(List<Type> nodes)
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
                    generatedNodes[i].NodeType = nodes[i];
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
                bool onOff = generatedNodes[i].NodeType.ToString().ToUpper().Contains(text.ToUpper());
                generatedNodes[i].gameObject.SetActive(onOff);
            }
        }
    }
}