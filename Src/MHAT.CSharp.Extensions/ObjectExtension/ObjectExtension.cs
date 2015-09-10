using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MHAT.CSharp.Extensions.ReflectionExtension;
using Newtonsoft.Json;

namespace MHAT.CSharp.Extensions.ObjectExtension
{
    /// <summary>
    /// Extension methods for object
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Get string of object; guard against null object with return empty string
        /// </summary>
        /// <param name="value">getting string value of object</param>
        /// <returns>
        /// If the pass in object is null, return empty string. Else call ToString method on the object.
        /// </returns>
        public static string ToStringNullGuard(this object value)
        {
            return (value ?? string.Empty).ToString();
        }

        /// <summary>
        /// Get Object proeprty value by property name
        /// </summary>
        /// <typeparam name="T">The expected return type</typeparam>
        /// <param name="obj">The object</param>
        /// <param name="name">The Property name</param>
        /// <returns>if is null, return default(t), else return value in T</returns>
        public static T GetPropertyValue<T>(this Object obj, String name)
        {
            Object retval = obj.GetPropertyValue(name);

            if (retval == null) 
            { 
                return default(T); 
            }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        /// <summary>
        /// To the json string.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">The object to json</param>
        /// <returns>Json string of the object</returns>
        public static string ToJsonString<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}