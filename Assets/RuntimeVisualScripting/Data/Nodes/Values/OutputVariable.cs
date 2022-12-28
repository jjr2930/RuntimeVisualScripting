using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class OutputVariable<T> : Variable<T>
    {
        [SerializeField]
        protected List<long> linkIds = new List<long>();
        
        protected List<InputVariable<T>> links = new List<InputVariable<T>>();

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

        public void AddLinkOneWay(InputVariable<T> newNode)
        {
            if (links.Contains(newNode))
            {
                Debug.LogWarning($"already has link({newNode.Name})");
                return;
            }

            links.Add(newNode);
        }
        public void AddLinkTwoWay(InputVariable<T> newLink)
        {
            AddLinkOneWay(newLink);
            newLink.SetLinkOneWay(this);
        }

        public void RemoveLinkTwoWay(InputVariable<T> oldLink)
        {
            RemoveLinkOneWay(oldLink);
            oldLink.SetLinkOneWay(null);
        }

        public void RemoveLinkOneWay(InputVariable<T> oldLink)
        {
            if (false == links.Contains(oldLink))
            {
                Debug.LogWarning($"there is no link({oldLink.Name})");
                return;
            }

            links.Remove(oldLink);
        }


        public override void Serialize(VisualScriptStream stream)
        {
            this.linkIds.Clear();
            for (int i = 0; i < links.Count; i++)
            {
                linkIds.Add(links[i].Id);
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
    }
}
