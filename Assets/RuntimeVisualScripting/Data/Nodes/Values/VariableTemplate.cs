using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeVisualScripting.Data
{
    public abstract class Variable<T> : Variable
    {
        protected T value;
        public virtual T Value 
        { 
            get { return value; } 
            set { this.value = value; } 
        }

        protected override void Deserialize(DeserializeStream stream)
        {
            throw new NotImplementedException();
        }

        protected override void Serialize(SerializeStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
