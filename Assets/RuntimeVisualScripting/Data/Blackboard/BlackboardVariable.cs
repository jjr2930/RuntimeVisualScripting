using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public abstract class BlackboardVariable : SerializableObject
    {
        [SerializeField]
        string name;

        public string Name { get => name; set => name = value; }
    }
}
