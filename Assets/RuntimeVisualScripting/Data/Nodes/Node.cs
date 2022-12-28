using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public abstract class Node : SerializableObject
    {
        [SerializeField]
        protected Vector2 position;
        public Vector2 Position { get => position; set => position = value; }
        public virtual string DisplayName { get; }
        public abstract List<Variable> GetInputVariables();
        public abstract List<Variable> GetOutputVariables();
    }
}
