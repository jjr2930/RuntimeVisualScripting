using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeVisualScripting.UI
{
    public class DraggingObject : MonoBehaviour, 
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        RectTransform rectTransform = null;

        public void Awake()
        {
            rectTransform = transform as RectTransform;    
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();

        }

        public void OnDrag(PointerEventData eventData)
        {
            var halfSize = rectTransform.sizeDelta / 2f;
            var point = eventData.position;
            point.x -= halfSize.x;
            point.y += halfSize.y;

            rectTransform.position = point;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        
        }
    }
}
