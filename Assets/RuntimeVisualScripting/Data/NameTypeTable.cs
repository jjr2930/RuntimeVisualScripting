using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuntimeVisualScripting.Data
{
    public class NameTypeTable : MonoBehaviour
    {
        static NameTypeTable instance = null;
        public static NameTypeTable Instance
        {
            get
            {
                if(null == instance)
                {
                    var go = new GameObject("NameTypeTable");
                    instance = go.AddComponent<NameTypeTable>();
                    DontDestroyOnLoad(go);
                }

                return instance;
            }
        }

        /// <summary>
        /// key : full name of type
        /// value : type infomation
        /// </summary>
        Dictionary<string, Type> typeByString = new Dictionary<string, Type>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Type GetType(string fullName)
        {
            Type found = null;
            if (!typeByString.TryGetValue(fullName, out found))
            {
                found = Type.GetType(fullName);

                if (null == found)
                    throw new InvalidOperationException("there is no type : " + fullName);

                typeByString[fullName] = found;
            }

            return found;
        }
    }
}
