using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EssentialExtensionSet.ObjectExtension
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
    }
}