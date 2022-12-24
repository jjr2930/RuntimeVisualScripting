using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeVisualScripting.UI
{
    public class DragableUI : MonoBehaviour, IDragHandler
    {

        public virtual void OnDrag(PointerEventData eventData)
        {
            var nextLocalPosition = transform.localPosition;
            nextLocalPosition.x += eventData.delta.x;
            nextLocalPosition.y += eventData.delta.y;

            transform.localPosition = nextLocalPosition;
        }
    }
}
