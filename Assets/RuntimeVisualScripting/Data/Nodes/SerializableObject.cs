using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.Data
{
    public abstract class SerializableObject
    {
        static Random random = new Random();
        protected string name = "";
        public string Name 
        { 
            get { return name; } 
            set { name = value; } 
        }

        protected long? id = null;
        public long GUID
        {
            get
            {
                if(null == id)
                {
                    byte[] randomLong = new byte[sizeof(long)];
                    random.NextBytes(randomLong);
                    id = BitConverter.ToInt64(randomLong);
                    UnityEngine.Debug.Log("id : " + id);
                }
               
                return id.Value;
            }
        }
        public abstract void Serialize(VisualScriptStream stream);
        public abstract void Deserialize(VisualScriptStream stream);
    }
}