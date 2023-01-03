using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CategoryInfo = RuntimeVisualScripting.UI.NodeList.Category;
using CategoryItemInfo = RuntimeVisualScripting.UI.NodeList.CategoryItem;

namespace RuntimeVisualScripting.UI
{
    public class NodeCategoryUI : ListUIBase<NodeListItemUI, CategoryItemInfo>
    {
        [Header("Node Category UI")]
        [SerializeField]
        TextMeshProUGUI title = null;

        [SerializeField]
        CategoryInfo categoryInfo = null;

        public CategoryInfo CategoryInfo 
        {
            get => categoryInfo;
            set
            {
                categoryInfo = value;
                SetInfo(categoryInfo.items);
            }
        }

        protected override void SetMember(CategoryItemInfo info, int index, NodeListItemUI item)
        {
            item.NodeType = info.type;
            item.SetNameText(info.name);
        }

        protected override void OnSetInfo()
        {
            base.OnSetInfo();
            title.text = categoryInfo.title;
        }
    }
}
