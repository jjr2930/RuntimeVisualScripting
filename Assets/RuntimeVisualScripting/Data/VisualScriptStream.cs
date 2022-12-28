using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class VisualScriptStream
    {
        [Serializable]    
        public struct SerializedObject
        {
            public string typeName;
            public string json;

            public SerializedObject(SerializableObject obj)
            {
                typeName = obj.GetType().FullName;
                json = JsonUtility.ToJson(obj);
            }
        }

        [SerializeField]
        List<SerializedObject> serializedObjects = new List<SerializedObject>();

        public int Count { get { return serializedObjects.Count; } }

        int header = 0;
        
        public void Push(SerializableObject obj)
        {
            serializedObjects.Add(new SerializedObject(obj));
        }
        public SerializedObject GetNext()
        {
            //return serializedObjects[header++];

            var found = serializedObjects[header];
            header++;
            return found;
        }

        public SerializedObject GetCurrent()
        {
            return serializedObjects[header];
        }

        public bool HasNext()
        {
            return header < serializedObjects.Count;
        }

        public void Clear()
        {
            header = 0;
            serializedObjects.Clear();
        }
    }
}
