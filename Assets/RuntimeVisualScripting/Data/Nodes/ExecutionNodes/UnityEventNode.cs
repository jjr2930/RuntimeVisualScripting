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
        protected UnityEventType eventType;
        protected Node nextNode;

        public UnityEventType EventType 
        { 
            get => eventType; 
            set => eventType = value; 
        }

        public Node NextNode
        {
            get => nextNode;
            set => nextNode = value;
        }

        public override string DisplayName { get => eventType.ToString(); }

        public override List<Variable> GetInputVariables()
        {
            return null;
        }

        public override List<Variable> GetOutputVariables()
        {
            return null;
        }

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
