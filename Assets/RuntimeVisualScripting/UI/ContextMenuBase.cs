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
        public Type type;

        public ContextMenuItemInfo(string name, int index, Type type)
        {
            this.name = name;
            this.index = index;
            this.type = type;
        }
    }


    [RequireComponent(typeof(RectTransform))]
    public class ContextMenuBase : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTranform;

        [SerializeField]
        ContextMenuItemUI itemForCopy = null;

        [SerializeField]
        List<ContextMenuItemInfo> menuInfos = new List<ContextMenuItemInfo>();

        [SerializeField]
        List<ContextMenuItemUI> createdItems = new List<ContextMenuItemUI>();

        public Action<int> OnItemClicked { get; set; }

        public void Reset()
        {
            rectTranform = GetComponent<RectTransform>();
        }

        public void Toggle(Vector2 screenPoint, params ContextMenuItemInfo[] items)
        {
            if(false == gameObject.activeInHierarchy)
            {
                Open(screenPoint, items);
            }
            else
            {
                Close();
            }
        }
        public void Open(Vector2 screenPoint, params ContextMenuItemInfo[] items )
        {
            Debug.Log("Screen Point : " + screenPoint);
            menuInfos.Clear();
            menuInfos.AddRange(items);

            BuildItem(items);

            var spawnPosition = screenPoint;
            float width = rectTranform.rect.width;
            float height = rectTranform.rect.height;

            rectTranform.pivot = new Vector2(0, 1);

            if (spawnPosition.y - height < 0)
                spawnPosition.y = height;

            if (spawnPosition.x + width >= Screen.width)
                spawnPosition.x = Screen.width - width;

            rectTranform.position = spawnPosition;

            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        void BuildItem(params ContextMenuItemInfo[] itemInfos)
        {
            //matching count
            int count = itemInfos.Length - createdItems.Count;
            if(count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var newItem = Instantiate(itemForCopy, rectTranform);
                    newItem.gameObject.SetActive(true);
                    createdItems.Add(newItem);
                }
            }
            else if( count < 0)
            {
                for (int i = 0; i < createdItems.Count; i++)
                {
                    bool activation;
                    if (i >= itemInfos.Length)
                        activation = false;
                    else
                        activation = true;

                    createdItems[i].gameObject.SetActive(activation);
                }
            }

            for (int i = 0; i < itemInfos.Length; i++)
            {
                createdItems[i].SetMember(itemInfos[i].name, itemInfos[i].index);
            }
        }
    }
}
