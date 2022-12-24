using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class VisualScript : SerializableObject
    {
        List<ExecutionNode> executionNodes = new List<ExecutionNode>();
        List<ArithmeticNode> arithmeticNodes = new List<ArithmeticNode>();
        Blackboard blackboard = null;


        protected override void Deserialize(DeserializeStream stream)
        {
            throw new System.NotImplementedException();
        }

        protected override void Serialize(SerializeStream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
