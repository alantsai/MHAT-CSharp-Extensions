using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EssentialExtensionSet.ReflectionExtension;

namespace EssentialExtensionSet.EnumExtension
{
    /// <summary>
    /// Enum Extension
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get display name for enum.
        /// Will try if DisplayAttribute exist, then DisplayNameAttribute
        /// then the actual enum name
        /// </summary>
        /// <param name="value">The enum value want to get the display name</param>
        /// <returns>Enum display name</returns>
        public static string DisplayName(this Enum value)
        {
            string result = value.ToString();

            Type enumType = value.GetType();

            var enumValue = Enum.GetName(enumType, value);

            result = enumType.GetMember(enumValue)[0].DisplayName(result);

            return result;
        }
    }
}