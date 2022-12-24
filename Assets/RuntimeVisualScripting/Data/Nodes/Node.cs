using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public abstract class Node : SerializableObject
    {
        protected Vector2 position;
        protected Vector2 Position { get => position; set => position = value; }

        public abstract List<Variable> GetInputVariables();
        public abstract List<Variable> GetOutputVariables();
    }
}
