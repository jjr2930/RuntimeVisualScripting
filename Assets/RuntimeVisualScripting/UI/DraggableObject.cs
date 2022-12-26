using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeVisualScripting.UI
{
    public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        static DragPanel panel = null;
        [SerializeField]
        protected RectTransform copiedTransform = null;

        [SerializeField]
        protected RectTransform rectTransform = null;
        
        public void Reset()
        {
            rectTransform = transform as RectTransform;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (null == panel)
                BuildPanelToCanvas(GetComponentInParent<Canvas>());

            var copied = Instantiate(gameObject, panel.transform);
            Destroy(copied.GetComponent<DraggableObject>());

            copied.GetComponent<NodeSelectMenuListItemUI>();

            copiedTransform = copied.transform as RectTransform;
            copiedTransform.localScale = transform.localScale;
            copiedTransform.position = transform.position;
            copiedTransform.sizeDelta = rectTransform.sizeDelta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            copiedTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GraphicRaycaster raycaster
                = GetComponentInParent<GraphicRaycaster>();

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);
            if (results.Count == 0)
            {
                Destroy(copiedTransform.gameObject);
                return;
            }

            for (int i = 0; i < results.Count; i++)
            {
                var receiver = results[i].gameObject.GetComponent<DragReceiver>();
                if (null != receiver)
                {
                    receiver.OnDragEnd(this, eventData.position);
                    Destroy(copiedTransform.gameObject);
                    return;
                }
            }

            Destroy(copiedTransform.gameObject);
        }

        public void BuildPanelToCanvas(Canvas targetCanvas)
        {
            GameObject go = new GameObject("DragPanel", typeof(RectTransform));
            go.transform.SetParent(targetCanvas.transform);
            RectTransform rectTransform = go.transform as RectTransform;
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchoredPosition = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);

            panel = go.AddComponent<DragPanel>();
        }


        public Vector3 WorldPosition { get { return rectTransform.position; } }
        public Vector3 LocalScale { get { return rectTransform.localScale; } }
        public Vector2 SizeDelta { get { return rectTransform.sizeDelta; } }
    }
}
