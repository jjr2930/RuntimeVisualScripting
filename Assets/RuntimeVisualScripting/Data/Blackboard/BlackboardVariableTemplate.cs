using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class BlackboardVariableTemplate<T> : BlackboardVariable
    {
        T value;

        public T Value { get => value; set => this.value = value; }
    }
}
