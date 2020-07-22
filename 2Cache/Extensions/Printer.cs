namespace Main2Cache
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Overrides ToString to print each element of a collection.
    /// </summary>
    /// <remarks>
    /// Inherits List in order to support List.Contains instance method as well
    /// as standard Enumerable.Contains/Any extension methods.
    /// </remarks>
    internal class Printer<T> : List<T>
    {
        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IEnumerable"/></param>
        public Printer(IEnumerable collection)
        {
            this.AddRange(collection.Cast<T>());
        }

        /// <summary>
        /// Create a string from the entity.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{" + this.ToConcatenatedString(t => t.ToString(), "|") + "}";
        }
    }
}
