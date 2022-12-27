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

        public override void Deserialize(VisualScriptStream stream)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(VisualScriptStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
