using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RuntimeVisualScripting.Data
{
    public class VisualScript : SerializableObject
    {
        List<Node> nodes =new List<Node>();
        Blackboard blackboard = null;

        public void AddNode(Node newNode)
        {
            nodes.Add(newNode);
        }

        public void RemoveNode(Node oldNode)
        {
            nodes.Remove(oldNode);
        }

        public override void Deserialize(VisualScriptStream stream)
        {
            //generate node...

        }

        public override void Serialize(VisualScriptStream stream)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Serialize(stream);
            }
        }
    }
}
