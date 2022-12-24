using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public abstract class ExecutionNode : Node
    {
        protected ExecutionNode previousNode;
        protected ExecutionNode nextNode;

        public abstract void Run();
    }
}
