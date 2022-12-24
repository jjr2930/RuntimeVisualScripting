using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace RuntimeVisualScripting.UI
{
    public class UIBezierLineRenderer : MonoBehaviour
    {
        public struct BezierLinePoint
        {
            public Vector2 position;
            public Vector2 tangent;
            public BezierLinePoint(Vector2 position, Vector2 tangent)
            {
                this.position = position;
                this.tangent = tangent;
            }
        }

        [SerializeField]
        BezierLinePoint[] sourcePoints = new BezierLinePoint[2];

        //[SerializeField]
        Vector4[] points = new Vector4[1000];

        [Header("UIBezierLineRenderer")]
        [SerializeField]
        Color lineColor = Color.white;

        [SerializeField]
        float lineThickness = 1f;

        [Range(0f, 0.1f)]
        [SerializeField]
        float pixelPerDot = 0.01f; 

        [SerializeField]
        protected Image image;

        [SerializeField]
        int resolution = 5;

        public void Reset()
        {
            image = GetComponent<Image>();
        }
        protected Vector2 min;
        protected Vector2 max;

        protected Vector2 start;
        protected Vector2 end;
        public void Resize()
        {
            //Debug.Log("Start : " + start + ", end : " + end);
            min.x = (start.x < end.x) ? start.x : end.x;
            min.y = (start.y < end.y) ? start.y : end.y;

            max.x = (start.x > end.x) ? start.x : end.x;
            max.y = (start.y > end.y) ? start.y : end.y;

            RectTransform rectTransform = transform as RectTransform;
            
            //rectTransform.offsetMin = min;
            //rectTransform.offsetMax = max;

            rectTransform.position = min + ((max - min) / 2f);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, max.x - min.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, max.y - min.y);
        }

        public void SetPoint(Vector2 point1, Vector2 point1Tangent, Vector2 point2, Vector2 point2Tangent)
        {
            sourcePoints[0] = new BezierLinePoint(point1, point1Tangent);
            sourcePoints[1] = new BezierLinePoint(point2, point2Tangent);

            //3차 베지어 곡선 https://namu.wiki/w/%EB%B2%A0%EC%A7%80%EC%97%90%20%EA%B3%A1%EC%84%A0
            //B(t) = (1-t)^3 * P0 + 3(1-t)^2 * tP1 + 3(1-t) * t^2 * P2 + t^3 *P3
            Vector2 p0 = point1;
            Vector2 p1 = point1 + point1Tangent;
            Vector2 p2 = point2 + point2Tangent;
            Vector2 p3 = point2;

            Bounds bound = CalculateBound(p0, p1, p2, p3);

            int xCount = (int)(bound.size.x * pixelPerDot) ;
            int yCount = (int)(bound.size.y * pixelPerDot);

            resolution = (int)(xCount * yCount);
            resolution = Math.Clamp(resolution, 0, 1000);

            for (int i = 0; i < resolution; i++)
            {
                float t = i / (float)(resolution - 1);
                float _1_t = (1 - t);
                Vector2 a = Mathf.Pow(_1_t, 3) * p0;
                Vector2 b = 3 * Mathf.Pow(_1_t, 2) * t * p1;
                Vector2 c = 3 * _1_t * (t * t) * p2;
                Vector2 d = (t * t * t) * p3;

                Vector2 point = a + b + c + d;

                points[i] = point;
            }

            ApplyToMaterial();
        }

        protected void ApplyToMaterial()
        {
            var material = image.material;
            //Debug.Log("Points Count : " + resolution);
            material.SetInteger("pointsCount", resolution);
            material.SetVectorArray("points", points);
            material.SetColor("_Color", lineColor);
            material.SetFloat("Thickness", lineThickness);
            material.SetVector("ScreenSize", new Vector4(Screen.width, Screen.height, 0, 0));

        }

        public Bounds CalculateBound(params Vector2[] points)
        {
            Bounds result = new Bounds();
            var min = result.min;
            var max = result.max;

            for (int i = 0; i < points.Length; i++)
            {
                if (min.x > points[i].x)
                    min.x = points[i].x;
                else if(max.x < points[i].x)
                    max.x = points[i].x;

                if (min.y > points[i].y)
                    min.y = points[i].y;
                else if (max.y < points[i].y)
                    max.y = points[i].y;
            }

            result.SetMinMax(min, max);
            return result;
        }
    }
}
