using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.UI
{
    public class DragPanel : MonoBehaviour
    {
        static DragPanel panel = null;
        static DraggableObject draggableObject = null;
        static DraggableObject copiedDraggableObject = null;
        public static void OnBeginObjectDragged(DraggableObject draggableObject, Canvas targetCanvas)
        {
            if(null == panel)
            {
                BuildPanelToCanvas(targetCanvas);
            }

            DragPanel.draggableObject = draggableObject;
            var copied = Instantiate(draggableObject.gameObject, panel.transform);
            Destroy(copied.GetComponent<DraggableObject>());

            copied.AddComponent<DraggingObject>();
            copied.transform.position = draggableObject.transform.position;

            RectTransform copiedRectTransform = copied.transform as RectTransform;
            copiedRectTransform.localScale = draggableObject.LocalScale;
            copiedRectTransform.position = draggableObject.WorldPosition;
            copiedRectTransform.sizeDelta = draggableObject.SizeDelta;
        }

        public static void BuildPanelToCanvas(Canvas targetCanvas)
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
    }
}
