using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class OutputVariable<T> : Variable<T> , ILinkable
    {
        [SerializeField]
        protected List<long> linkIds = new List<long>();
        
        protected List<ILinkable> links = new List<ILinkable>();

        public override T Value
        {
            get
            {
                if (parent is ArithmeticNode)
                {
                    var arithmeticNode = parent as ArithmeticNode;
                    arithmeticNode.Calculate();
                }

                return value;
            }
            set => base.value = value; 
        }

        public override Type ValueType { get => typeof(T); }

        public bool IsInputVaraible => false;

        public bool HasLink => links.Count > 0;

        public override void Serialize(VisualScriptStream stream)
        {
            linkIds.Clear();
            for (int i = 0; i < links.Count; i++)
            {
                linkIds.Add((links[i] as SerializableObject).Id);
            }
            base.Serialize(stream);
        }

        public override void Deserialize(Dictionary<long, SerializableObject> objectMap)
        {
            for (int i = 0; i < linkIds.Count; i++)
            {
                var found = objectMap[linkIds[i]];
                links.Add(found as InputVariable<T>);
            }
        }

        public void LinkOneWay(ILinkable other)
        {
            if (links.Contains(other))
            {
                Debug.LogWarning($"already has link({other.ToString()})");
                return;
            }

            links.Add(other);
        }

        public void LinkTwoWay(ILinkable other)
        {
            LinkOneWay(other);
            other.LinkOneWay(this);
        }

        public void UnlinkOneWay(ILinkable other)
        {
            if (false == links.Contains(other))
            {
                Debug.LogWarning($"there is no link({other})");
                return;
            }

            links.Remove(other);
        }

        public void UnlinkTwoWay(ILinkable other)
        {
            UnlinkOneWay(other);
            other.UnlinkOneWay(this);
        }

        public ILinkable GetTarget(int index)
        {
            if (index >= links.Count)
                return null;

            return links[index];
        }
    }
}
