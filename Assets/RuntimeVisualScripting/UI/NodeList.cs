using RuntimeVisualScripting.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeVisualScripting.UI
{

    public static class NodeList 
    {
        [Serializable]
        public class CategoryItem
        {
            public string name;
            public Type type;

            public CategoryItem(string name, Type type)
            {
                this.name = name;
                this.type = type;
            }   
        }
        [Serializable]
        public class Category
        {
            /// <summary>
            /// name of category
            /// </summary>
            public string title;
            public List<CategoryItem> items;
        }

        public static List<Category> Categories = new List<Category>()
        {
            new Category()
            {
                title = "Unity Event",
                items = new List<CategoryItem>()
                {
                    new CategoryItem("Start", typeof(UnityEventNode)),
                    new CategoryItem("Update", typeof(UnityEventNode)),
                    new CategoryItem("OnDestroy", typeof(UnityEventNode))
                }
            },
            new Category()
            {
                title = "Arithmetic",
                items = new List<CategoryItem>()
                {
                    new CategoryItem("Add Int", typeof(AddInt)),
                    new CategoryItem("Add Float", typeof(AddFloat)),
                    new CategoryItem("Add String", typeof(AddString)),
                    new CategoryItem("Int To String", typeof(IntToString)),
                }
            },
            new Category()
            {
                title = "Debug",
                items = new List<CategoryItem>()
                {
                    new CategoryItem("Log", typeof(Log))
                }
            }
        };
    }
}
