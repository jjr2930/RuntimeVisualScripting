using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField]
        protected VisualScriptUI root = null;
        public abstract void OnNodeMoved(NodeUI movedNode);

        public void Reset()
        {
            root = GetComponentInParent<VisualScriptUI>();
        }

        public void Start()
        {
            if (null == root)
            {
                root = GetComponentInParent<VisualScriptUI>();
            }

            //root.AddLinkLine(this);
        }

        public void OnDestroy()
        {
            //root.RemoveLinkLine(this);
        }
    }
}
