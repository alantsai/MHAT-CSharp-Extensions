using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHAT.CSharp.Extensions.IEnumerableExtension
{
    /// <summary>
    /// IEnumerable Extensions
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Out put enumerable of item as strings
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="seperator">The seperator between each value</param>
        /// <returns>
        /// String represent of enumrable of items
        /// </returns>
        public static string ToConcatString<T>(this IEnumerable<T> value, 
            string seperator = ",")
        {
            return string.Join(seperator, value.Select(x => x.ToString()));
        }
    }
}
