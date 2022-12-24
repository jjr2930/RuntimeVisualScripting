using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class Blackboard : SerializableObject
    {
        List<BlackboardVariable> variables = new List<BlackboardVariable>(0);

        public void AddVariable(BlackboardVariable newVariable)
        {
            variables.Add(newVariable);
        }

        public void RemoveVariable(BlackboardVariable oldVariable)
        {
            variables.Remove(oldVariable);
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
