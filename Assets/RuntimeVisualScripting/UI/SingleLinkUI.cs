using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class SingleLinkUI : LinkUI
    {
        [SerializeField]
        protected LinkUI targetLink = null;

        [SerializeField]
        public override void SetOrAddTargetLink(LinkUI newLink)
        {
            targetLink = newLink;
        }

        public override void RemoveTargetLink(LinkUI oldLink)
        {
            if (targetLink != oldLink)
            {
                Debug.LogWarning("target link != oldLink");
                return;
            }

            targetLink = null;
        }
    }
}
