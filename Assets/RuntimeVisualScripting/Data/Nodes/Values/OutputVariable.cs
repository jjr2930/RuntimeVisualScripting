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
