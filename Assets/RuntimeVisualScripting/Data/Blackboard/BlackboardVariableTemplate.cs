using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class BlackboardVariableTemplate<T> : BlackboardVariable
    {
        [SerializeField]
        T value;

        public T Value { get => value; set => this.value = value; }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            throw new NotImplementedException();
        }
    }
}
