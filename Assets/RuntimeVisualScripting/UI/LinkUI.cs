using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.UI
{
    public abstract class LinkUI : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
    {        
        //[SerializeField]
        //protected VariableUI variableUI = null;
        
        protected VisualScriptUI visualScriptUI = null;

        //public long VariableId { get { return variableUI.Variable.Id; } }
        public virtual void Start()
        {
            visualScriptUI = GetComponentInParent<VisualScriptUI>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            visualScriptUI.OnLinkUIBeginDrag(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            visualScriptUI.OnLinkUIDrag(this, eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var rayCaster = GetComponentInParent<GraphicRaycaster>();
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            rayCaster.Raycast(ped, results);

            if (results.Count <= 0) return;

            for (int i = 0; i < results.Count; i++)
            {
                var findLinkUI = results[i].gameObject.GetComponent<LinkUI>();
                bool isMine = this == findLinkUI;
                bool isNoLink = null == findLinkUI;

                if (isMine || isNoLink)
                    continue;

                if (false == findLinkUI.CanConnection(this))
                    continue;

                //found...
                visualScriptUI.OnLinkUIEndDrag(this, findLinkUI);
                return;
            }

            //not found
            visualScriptUI.OnLinkUIEndDrag(this, null);
        }

        //public virtual void SetOrAddTargetLink(LinkUI newLink) { }

        //public virtual void RemoveTargetLink(LinkUI oldLink) { }

        //public static void ConnectTwoWay(LinkUI a, LinkUI b)
        //{
        //    a.SetOrAddTargetLink(b);
        //    b.SetOrAddTargetLink(a);
        //}

        //public static void DisconnectTwoWay(LinkUI a, LinkUI b)
        //{
        //    a.RemoveTargetLink(b);
        //    b.RemoveTargetLink(a);
        //}

        public virtual void LinkOneWay(LinkUI other) { }
        public virtual void LinkTwoWay(LinkUI other) { }
        public virtual void UnlinkOneWay(LinkUI other) { }
        public virtual void UnlinkTwoWay(LinkUI other) { }
        public abstract bool CanConnection(LinkUI checkingTarget);
    }
}
