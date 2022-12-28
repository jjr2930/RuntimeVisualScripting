using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class VisualScript : SerializableObject
    {
        List<Node> nodes = new List<Node>();
        Blackboard blackboard = null;

        public int GetNodeCount()
        {
            return nodes.Count;
        }

        public Node GetNode(int index)
        {
            return nodes[index];
        }

        public void AddNode(Node newNode)
        {
            nodes.Add(newNode);
        }

        public void RemoveNode(Node oldNode)
        {
            nodes.Remove(oldNode);
        }

        public void Deserialize(VisualScriptStream stream)
        {
            Dictionary<long, SerializableObject> serializableObjectMap = new Dictionary<long, SerializableObject>();

            //generate variable
            while (stream.HasNext())
            {
                var serializedObject = stream.GetNext();
                Type type = NameTypeTable.Instance.GetType(serializedObject.typeName);
                SerializableObject deserializedObject =
                    JsonUtility.FromJson(serializedObject.json, type) as SerializableObject;

                serializableObjectMap.Add(deserializedObject.Id, deserializedObject);
            }

            
            //link!!
            foreach (var item in serializableObjectMap)
            {
                if (item.Value is Node)
                    AddNode(item.Value as Node);

                item.Value.Deserialize(serializableObjectMap);
            }
        }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            throw new NotImplementedException();
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
