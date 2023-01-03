using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Properties;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public abstract class ListUIBase<ItemType,InfoType> : MonoBehaviour
        where ItemType : MonoBehaviour
    {
        [Header("ListUI Base")]
        [SerializeField]
        protected Transform contentRoot = null;

        [SerializeField]
        protected ItemType itemForCopy = null;

        List<ItemType> generatedItems = new List<ItemType>();

        public void SetInfo(List<InfoType> infos)
        {
            int count = infos.Count - generatedItems.Count;
            if(count > 0)
            {
                for (int i = 0; i < count; ++i)
                {
                    var newItem = Instantiate(itemForCopy, contentRoot);
                    generatedItems.Add(newItem);
                }
            }

            for (int i = 0; i < generatedItems.Count; i++)
            {
                var item = generatedItems[i];
                if (i < infos.Count)
                {
                    item.gameObject.SetActive(true);
                    SetMember(infos[i], i, item);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }

            OnSetInfo();
        }

        protected virtual void OnSetInfo() { }
        protected abstract void SetMember(InfoType info, int index, ItemType item);
    }
}
