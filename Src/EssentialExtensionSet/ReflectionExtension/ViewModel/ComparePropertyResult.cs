using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssentialExtensionSet.ReflectionExtension.ViewModel
{
    /// <summary>
    /// Holds the result of compare property
    /// </summary>
    public class ComparePropertyResult
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the value a.
        /// </summary>
        /// <value>
        /// The value a.
        /// </value>
        public object ValueA { get; set; }

        /// <summary>
        /// Gets or sets the value b.
        /// </summary>
        /// <value>
        /// The value b.
        /// </value>
        public object ValueB { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
        public Type PropertyType { get; set; }
    }
}
