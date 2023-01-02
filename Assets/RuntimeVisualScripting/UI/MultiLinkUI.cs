using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class MultiLinkUI : LinkUI
    {
        [Header("Multi Link UI")]
        [SerializeField]
        List<LinkUI> targetLinks = new List<LinkUI>();

        

        //public override void SetOrAddTargetLink(LinkUI newLink)
        //{
        //    if (targetLinks.Contains(newLink))
        //        return;

        //    targetLinks.Add(newLink);
        //}

        //public override void RemoveTargetLink(LinkUI oldLink)
        //{
        //    if (false == targetLinks.Contains(oldLink))
        //        return;

        //    targetLinks.Remove(oldLink);
        //}

        //public LinkUI GetLink(int index)
        //{
        //    if (index < 0 || index >= targetLinks.Count)
        //        throw new System.IndexOutOfRangeException();

        //    return targetLinks[index];
        //}

        //public int GetLinkCount()
        //{
        //    return targetLinks.Count;
        //}
    }
}
