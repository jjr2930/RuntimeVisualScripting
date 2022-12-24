using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeVisualScripting.Data;
using System;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddInt newAddNode1 = new AddInt();
        AddInt newAddNode2 = new AddInt();
        IntToString newIntToStringNode = new IntToString();
        Log newLogNode = new Log();

        newAddNode1.InputA.Value = 1;
        newAddNode1.InputB.Value = 2;

        newAddNode2.InputA.SetLinkTwoWay(newAddNode1.Output);
        newAddNode2.InputB.Value = 7;
        newAddNode2.Output.AddLinkTwoWay(newIntToStringNode.Input);

        newLogNode.Input.SetLinkTwoWay(newIntToStringNode.Output);

        newLogNode.Run();
    }
}
