using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.Data
{
    public abstract class SerializableObject
    {
        protected string name = "";
        public string Name 
        { 
            get { return name; } 
            set { name = value; } 
        }

        protected long? id = 0;
        public long GUID
        {
            get
            {
                if(null == id)
                {
                    System.Random r = new System.Random((int)DateTime.Now.Ticks);
                    id = r.Next(int.MaxValue);
                }
                
                return id.Value;
            }
        }
        protected abstract void Serialize(SerializeStream stream);
        protected abstract void Deserialize(DeserializeStream stream);
    }
}