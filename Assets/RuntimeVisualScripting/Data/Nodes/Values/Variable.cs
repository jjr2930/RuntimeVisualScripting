using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public abstract class Variable : SerializableObject
    {
        protected Node parent;

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public abstract Type ValueType { get; }
    }
}