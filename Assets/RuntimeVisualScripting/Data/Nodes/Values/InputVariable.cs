using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class InputVariable<T> : Variable<T> , ILinkable
    {
        [SerializeField]
        protected long linkId;

        protected ILinkable link;

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
                    

                return (link as OutputVariable<T>).Value;
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

        public bool IsInputVaraible => true;

        public bool HasLink => link != null;

        /// <summary>
        /// input variable에서만 링크 정보를 직렬화 한다.
        /// </summary>
        /// <param name="stream"></param>
        public override void Serialize(VisualScriptStream stream)
        {
            if(null != link)
                linkId = (link as SerializableObject).Id;

            base.Serialize(stream);
        }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            //there is no link
            if (0 == linkId)
                return;

            link = objectMap[linkId] as ILinkable;
        }

        public void LinkOneWay(ILinkable other)
        {
            this.link = other;
        }

        public void LinkTwoWay(ILinkable other)
        {
            this.link = other;
            other.LinkOneWay(this);
        }

        public void UnlinkOneWay(ILinkable other)
        {
            this.link = null;
        }

        public void UnlinkTwoWay(ILinkable other)
        {
            this.link = null;
            other.UnlinkOneWay(this);
        }

        public ILinkable GetTarget(int index)
        {
            return link;
        }
    }
}
