using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CategoryInfo = RuntimeVisualScripting.UI.NodeList.Category;
namespace RuntimeVisualScripting.UI
{
    public class NodeCategoryListUI : ListUIBase<NodeCategoryUI, CategoryInfo>
    {
        public void Start()
        {
            SetInfo(NodeList.Categories);
        }

        protected override void SetMember(CategoryInfo info, int index, NodeCategoryUI item)
        {
            item.CategoryInfo = info;
        }
    }
}
