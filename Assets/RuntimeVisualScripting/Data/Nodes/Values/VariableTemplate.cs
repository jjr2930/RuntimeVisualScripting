using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public abstract class Variable<T> : Variable
    {
        [SerializeField]
        protected T value;
        public virtual T Value 
        { 
            get { return value; } 
            set { this.value = value; } 
        }
    }
}
