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

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            throw new System.NotImplementedException();
        }

    }
}
