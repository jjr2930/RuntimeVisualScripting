using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class VisualScript : SerializableObject
    {
        #region Delegates
        public delegate void OnNodeDeserialized(Node newNode);
        public delegate void OnNodeRemoved(Node oldNode);
        public delegate void OnNodeMoved(Node movedNode, Vector2 currentPosition);
        public delegate void OnExecutionConnected(Node a, Node b);
        public delegate void OnExecutionDisconnected(Node a, Node b);
        public delegate void OnVariableConnected(Variable a, Variable b);
        public delegate void OnVariableDisconnected(Variable a, Variable b);

        public delegate void OnInputVariableDiserialized(Variable a);
        #endregion

        List<Node> nodes = new List<Node>();
        Blackboard blackboard = null;

        public OnNodeDeserialized onNodeDeserialized;
        public OnInputVariableDiserialized onInputVariableDeserialized;

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

            //노드 먼저... 
            //구린데??
            foreach (var item in serializableObjectMap)
            {
                if (item.Value is Node)
                    AddNode(item.Value as Node);                    

                item.Value.Deserialize(serializableObjectMap);
            }

            foreach (var item in serializableObjectMap)
            {
                if(item.Value is Node)
                    onNodeDeserialized?.Invoke(item.Value as Node);
                    
                else if(item.Value is ILinkable)
                {
                    ILinkable linkable = item.Value as ILinkable;
                    if (linkable.IsInputVaraible)
                        onInputVariableDeserialized?.Invoke(item.Value as Variable);
                }
            }
        }

        public void Clear()
        {
            nodes.Clear();
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
