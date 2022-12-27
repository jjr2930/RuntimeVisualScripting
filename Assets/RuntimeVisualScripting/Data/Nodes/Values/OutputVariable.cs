using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class OutputVariable<T> : Variable<T>
    {
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

        public override void Deserialize(VisualScriptStream stream)
        {
            throw new System.NotImplementedException();
        }

        public override void Serialize(VisualScriptStream stream)
        {
            List<long> ids = new List<long>();
            for (int i = 0; i < links.Count; i++)
            {
                ids.Add(links[i].GUID);
            }
            stream.AddNewOutVariable(GUID, GetType(), ids);
        }
    }
}
