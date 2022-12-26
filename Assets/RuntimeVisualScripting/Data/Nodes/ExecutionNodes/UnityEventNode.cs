using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public enum UnityEventType
    {
        Start,
        Update
    }

    public class UnityEventNode : Node
    {
        [SerializeField]
        protected UnityEventType eventType;

        public UnityEventType EventType 
        { 
            get => eventType; 
            set => eventType = value; 
        }

        public override List<Variable> GetInputVariables()
        {
            return null;
        }

        public override List<Variable> GetOutputVariables()
        {
            return null;
        }

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
