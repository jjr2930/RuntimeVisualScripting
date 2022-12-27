using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class BlackboardVariable : SerializableObject
    {
        string name;

        public string Name { get => name; set => name = value; }

        public override void Deserialize(VisualScriptStream stream)
        {
            throw new System.NotImplementedException();
        }

        public override void Serialize(VisualScriptStream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
