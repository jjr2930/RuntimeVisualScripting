using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public abstract class ExecutionNode : Node
    {
        [SerializeField]
        protected long previousNodeId;
        [SerializeField]
        protected long nextNodeId;

        protected ExecutionNode previousNode;
        protected ExecutionNode nextNode;

        public abstract void Run();
    }
}
