using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class BlackboardVariable : SerializableObject
    {
        string name;

        public string Name { get => name; set => name = value; }

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
