using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeVisualScripting.Data;
using System;
using System.Diagnostics;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    TextAsset serializedVisualScript = null;

    // Start is called before the first frame update
    void Start()
    {
        #region Some test codes
        //AddInt newAddNode1 = new AddInt();
        //AddInt newAddNode2 = new AddInt();
        //IntToString newIntToStringNode = new IntToString();
        //Log newLogNode = new Log();

        //newAddNode1.InputA.Value = 1;
        //newAddNode1.InputB.Value = 2;

        //newAddNode2.InputA.SetLinkTwoWay(newAddNode1.Output);
        //newAddNode2.InputB.Value = 7;
        //newAddNode2.Output.AddLinkTwoWay(newIntToStringNode.Input);

        //newLogNode.Input.SetLinkTwoWay(newIntToStringNode.Output);

        //newLogNode.Run();

        //int testCount = 10000;
        //Type[] types = new Type[testCount];
        //string[] typeNames = new string[5]
        //{
        //    typeof(string).FullName,
        //    typeof(int).FullName,
        //    typeof(float).FullName,
        //    typeof(long).FullName,
        //    typeof(double).FullName,
        //};

        //Stopwatch stopwatch = new Stopwatch();
        //stopwatch.Start();
        //for (int i = 0; i < testCount; i++)
        //{
        //    types[i] = Type.GetType(typeNames[i % 5]);
        //}
        //stopwatch.Stop();
        //UnityEngine.Debug.Log($"Type.GetType : {stopwatch.ElapsedMilliseconds}");

        //NameTypeTable.Instance.GetType(typeNames[1]);
        //stopwatch.Restart();
        //for (int i = 0; i < testCount; i++)
        //{
        //    types[i] = NameTypeTable.Instance.GetType(typeNames[i % 5]);
        //}
        //stopwatch.Stop();
        //UnityEngine.Debug.Log($"NameTypeTable.Instance.GetType : {stopwatch.ElapsedMilliseconds}"); 
        #endregion

        //var stream = JsonUtility.FromJson<VisualScriptStream>(serializedVisualScript.text);
        //VisualScript newVisualScript = new VisualScript();
        //newVisualScript.Deserialize(stream);

        //stream.Clear();
        //newVisualScript.Serialize(stream);
        //UnityEngine.Debug.Log(JsonUtility.ToJson(stream, true));
    }
}
