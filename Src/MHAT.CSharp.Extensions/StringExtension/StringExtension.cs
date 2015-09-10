using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssentialExtensionSet.StringExtension
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Determines whether value is null, empty or only whitespace
        /// </summary>
        /// <param name="value">Test value</param>
        /// <returns>true if value is null, empty or only whitespace</returns>
        public static bool IsStringEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Determines whether value is NOT null, empty or only whitespace
        /// </summary>
        /// <param name="value">Test value</param>
        /// <returns>true if value is NOT null, empty or only whitespace</returns>
        public static bool IsStringNotEmpty(this string value)
        {
            return value.IsStringEmpty() == false;
        }
    }
}
