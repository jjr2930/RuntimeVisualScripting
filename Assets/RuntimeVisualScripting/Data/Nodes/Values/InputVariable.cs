using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class InputVariable<T> : Variable<T>
    {
        protected OutputVariable<T> link;

        public bool IsStatic
        {
            get => null == link;
        }

        public override T Value 
        {
            get
            {
                if (IsStatic)
                    return value;
                    
                return link.Value;
            }
            set
            {
                if (IsStatic)
                    base.Value = value;
                else
                    throw new InvalidOperationException("dynamic 변수에 직접 값을 넣음");
            }
        }

        public override Type ValueType 
        {
            get { return typeof(T); }
        }

        public void SetLinkOneWay(OutputVariable<T> link)
        {
            this.link = link;
        }

        public void SetLinkTwoWay(OutputVariable<T> link)
        {
            this.link = link;
            link.AddLinkOneWay(this);
        }

        public void RemoveLinkOneWay()
        {
            link = null;
        }

        public void RemoveLinkTwoWay()
        {
            link.RemoveLinkOneWay(this);
            RemoveLinkOneWay();
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
