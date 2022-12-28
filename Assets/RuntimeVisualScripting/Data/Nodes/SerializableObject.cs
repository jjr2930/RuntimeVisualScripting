using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public abstract class SerializableObject
    {
        static System.Random random = new System.Random();
        
        [SerializeField]
        protected string name = "";
        public string Name 
        { 
            get { return name; } 
            set { name = value; } 
        }

        /// <summary>
        /// it not should be 0, 0 means not set
        /// </summary>
        [SerializeField]
        protected long id;

        public long Id { get => id; }
        public SerializableObject()
        {
            //������ ���ڰ� ���ͼ� ���⸦ ���� ���ΰ�?
            //�����Ѵ� ����� 1/1800�� �� Ȯ���� �վ���.
            //:-)
            while (id == 0)
            {
                byte[] randomLong = new byte[sizeof(long)];
                random.NextBytes(randomLong);
                id = BitConverter.ToInt64(randomLong);
            }
        }

        public virtual void Serialize(VisualScriptStream stream)
        {
            stream.Push(this);
        }

        public abstract void Deserialize(Dictionary<long, SerializableObject> objectMap);
    }
}