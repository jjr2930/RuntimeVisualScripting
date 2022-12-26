using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RuntimeVisualScripting.UI
{
    public struct ContextMenuItemInfo
    {
        public string name;
        public int index;
        public Action<int> callback;

        public ContextMenuItemInfo(string name, int index, Action<int> callback)
        {
            this.name = name;
            this.callback = callback;
            this.index = index;
        }
    }

    [RequireComponent(typeof(RectTransform))]
    public class ContextMenuBase : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTranform;

        [SerializeField]
        Button itemForCopy = null;

        [SerializeField]
        List<ContextMenuItemInfo> menuInfos = new List<ContextMenuItemInfo>();

        public void Reset()
        {
            rectTranform = GetComponent<RectTransform>();
        }
        public void Open(Vector2 screenPoint, params ContextMenuItemInfo[] items )
        {
            menuInfos.Clear();
            menuInfos.AddRange(items);
            //buld Items
            for (int i = 0; i < menuInfos.Count; i++)
            {
                var newItem = Instantiate(itemForCopy, rectTranform);
                newItem.gameObject.SetActive(true);
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = menuInfos[i].name;
                newItem.onClick.AddListener(() =>
                {
                    menuInfos[i].callback?.Invoke(i);
                    Close();
                });
            }

            var spawnPosition = screenPoint;
            float width = rectTranform.rect.width;
            float height = rectTranform.rect.height;

            rectTranform.pivot = new Vector2(0, 1);

            if(spawnPosition.y + height >= Screen.height)
                spawnPosition.y = Screen.height - height;

            if(spawnPosition.x + width >= Screen.height)
                spawnPosition.x = Screen.width - width;

            rectTranform.anchoredPosition = spawnPosition;

            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
