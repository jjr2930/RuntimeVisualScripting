using RuntimeVisualScripting.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.Android.Types;
using UnityEditor.Graphs;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{
    public class LinkLineUI : UIBezierLineRenderer
    {
        [Header("LinkLineUI")]
        [SerializeField]
        LinkUI from;

        [SerializeField]
        LinkUI to;

        [SerializeField]
        VisualScriptUI visualScriptUI = null;

        public LinkUI From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
                
                if(null != from )
                    start = from.transform.position;

                ReCalculate();
            }
        }

        public LinkUI To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
               
                if(null != to )
                    end = to.transform.position;

                ReCalculate();
            }
        }
                
        public void Awake()
        {
            image.material = Instantiate(image.material);

            visualScriptUI = GetComponentInParent<VisualScriptUI>();

            visualScriptUI.AddLinkLine(this);
        }

        public void OnDestroy()
        {
            visualScriptUI.RemoveLinkLine(this);
        }

        public void SetToPosition(Vector2 toPosition)
        {
            this.end = toPosition;

            ReCalculate();
        }

        public void ReCalculate(NodeUI movedNode)
        {
            bool hasFrom = null != from;
            bool hasTo = null != to;
            bool isMine = false;

            //nothing to have
            if (false == (hasFrom || hasTo))
            {
                return;
            }

            if(false == hasFrom)
            {
                isMine = (movedNode == to.ParentNodeUI);
            }
            else if( false == hasTo)
            {
                isMine = movedNode == from.ParentNodeUI;
            }
            else
            {
                isMine = (movedNode == from.ParentNodeUI) || (movedNode == to.ParentNodeUI);
            }

            if (false == isMine)
                return;

            ReCalculate();
        }

        public void ReCalculate()
        {
            if(null != from)
                start = from.transform.position;

            if(null != to)
                end = to.transform.position;

            Resize();

            RectTransform rectTransform = transform as RectTransform;
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector2[] p = new Vector2[4];
            p[0] = start;

            p[1] = end - start;
            p[1].y = 0;

            p[2] = end;

            p[3] = start - end;
            p[3].y = 0;

            SetPoint(p[0], p[1], p[2], p[3]);
        }
    }
}
