using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using MHAT.CSharp.Extensions.ReflectionExtensions.ViewModel;

namespace MHAT.CSharp.Extensions.ReflectionExtensions
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

        /// <summary>
        /// Compares the public primitive type properties.
        /// This only compare the properties of T. No include all properties of the pass in object.
        /// If T is interface and self object has more properties then T, those will not be compared
        /// </summary>
        /// <typeparam name="T">Type of pass in object</typeparam>
        /// <param name="self">Source compare object</param>
        /// <param name="to">Compare to object</param>
        /// <param name="ignorePropertiesName">Name of properies to ignore.</param>
        /// <returns>Different primitive type properties name and compare value object</returns>
        public static IEnumerable<ComparePropertyResult>
            ComparePublicPrimitivePropertiesTypeOfPassinType<T>(this T self, T to,
            IEnumerable<string> ignorePropertiesName = null)
        {
            Type type = typeof(T);

            IEnumerable<ComparePropertyResult> variances =
                   CompareProperties<T>(self, to, type, ignorePropertiesName);

            return variances.Where(x => x.PropertyType.IsTypePrimitive());
        }

        /// <summary>
        /// Compares the public primitive type properties.
        /// This will include all properties of self object, not just compare T properties.
        /// </summary>
        /// <typeparam name="T">Type of pass in object</typeparam>
        /// <param name="self">Source compare object</param>
        /// <param name="to">Compare to object</param>
        /// <param name="ignorePropertiesName">Name of properies to ignore.</param>
        /// <returns>Different primitive type properties name and compare value object</returns>
        public static IEnumerable<ComparePropertyResult>
            ComparePublicPrimitivePropertiesTypeOfObject<T>(this T self, T to,
            IEnumerable<string> ignorePropertiesName = null)
        {
            IEnumerable<ComparePropertyResult> variances = new List<ComparePropertyResult>();

            if (self != null)
            {
                Type type = self.GetType();

                variances =
                   CompareProperties<T>(self, to, type, ignorePropertiesName);
            }

            return variances.Where(x => x.PropertyType.IsTypePrimitive());
        }

        /// <summary>
        /// Determines whether type is prmitive
        /// This does not check generic and lists
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Treu for being a primitive type</returns>
        public static bool IsTypePrimitive(this Type type)
        {
            return
                    type.IsPrimitive
                     || new Type[]
                        {
                              typeof(Enum),
                              typeof(String),
                              typeof(Char),
                              typeof(Guid),
                              typeof(Boolean),
                              typeof(Byte),
                              typeof(Int16),
                              typeof(Int32),
                              typeof(Int64),
                              typeof(Single),
                              typeof(Double),
                              typeof(Decimal),
                              typeof(SByte),
                              typeof(UInt16),
                              typeof(UInt32),
                              typeof(UInt64),
                              typeof(DateTime),
                              typeof(DateTimeOffset),
                              typeof(TimeSpan),
                        }.Contains(type);
        }

        /// <summary>
        /// Compares the properties.
        /// </summary>
        /// <typeparam name="T">Type of pass in object</typeparam>
        /// <param name="self">Source compare object</param>
        /// <param name="to">Compare to object</param>
        /// <param name="type">The type.</param>
        /// <param name="ignorePropertiesName">Name of properies to ignore.</param>
        /// <returns>
        /// Different primitive type properties name and compare value object
        /// </returns>
        private static IEnumerable<ComparePropertyResult> CompareProperties<T>
            (T self, T to, Type type, IEnumerable<string> ignorePropertiesName = null)
        {
            List<ComparePropertyResult> variances =
                new List<ComparePropertyResult>();

            if (ignorePropertiesName == null)
            {
                ignorePropertiesName = new List<string>();
            }

            if (self != null && to != null)
            {
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignorePropertiesName.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            ComparePropertyResult result = new ComparePropertyResult();

                            result.PropertyName = pi.Name;

                            result.ValueA = selfValue;

                            result.ValueB = toValue;

                            result.PropertyType = pi.PropertyType;

                            variances.Add(result);
                        }
                    }
                }
            }

            return variances;
        }
    }
}