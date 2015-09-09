using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using System.Text;

namespace EssentialExtensionSet.ReflectionExtension
{
    /// <summary>
    /// Reflection related extension
    /// </summary>
    public static class ReflectionExtension
    {
        /// <summary>
        /// Get Object property value by property name.  Also deal with multi level property.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="name">Name of the object proeprty to get value.
        /// Support multi level property such as Property1.InnerProperty</param>
        /// <returns>The value or null if not find</returns>
        public static Object GetPropertyValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) 
                { 
                    return null; 
                }

                Type type = obj.GetType();

                PropertyInfo info = type.GetProperty(part);

                if (info == null) 
                { 
                    return null; 
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        /// <summary>
        /// Get member name from DisplayAttribute or DisplayNameAttribute
        /// </summary>
        /// <param name="memberInfo">The memberinfo which want to get the name</param>
        /// <param name="defaultValue">The default value used if no attribute found</param>
        /// <returns>
        /// DisplayAttribute or DisplayNameAttribute or defaultValue
        /// </returns>
        public static string DisplayName(this MemberInfo memberInfo, string defaultValue)
        {
            string result = defaultValue;

            var displayAttr = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                            .FirstOrDefault() as DisplayAttribute;

            if (displayAttr != null)
            {
                result = displayAttr.Name;

                if (displayAttr.ResourceType != null)
                {
                    result = displayAttr.GetName();
                }
            }
            else
            {
                var displayNameAttr = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                            .FirstOrDefault() as DisplayNameAttribute;

                if (displayNameAttr != null)
                {
                    result = displayNameAttr.DisplayName;
                }
            }

            return result;
        }
    }
}