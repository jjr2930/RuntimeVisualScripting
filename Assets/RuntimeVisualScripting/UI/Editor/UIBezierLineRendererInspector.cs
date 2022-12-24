using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RuntimeVisualScripting.UI.Editor
{
    [CustomEditor(typeof(UIBezierLineRenderer))]
    public class UIBezierLineRendererInspector : UnityEditor.Editor
    {
        UIBezierLineRenderer Script { get { return (UIBezierLineRenderer)target; } }

        //public override void OnInspectorGUI()
        //{
        //    using (var checkScope = new EditorGUI.ChangeCheckScope())
        //    {
        //        base.OnInspectorGUI();
        //        return;
        //        if(checkScope.changed)
        //        {
        //            Script.Refresh();
        //        }
        //    }
        //}
    }
}
