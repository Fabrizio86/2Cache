namespace Main2Cache
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class LocalCollectionExpander
    {
        /// <summary>
        /// Overrides ToString to print each element of a collection.
        /// </summary>
        /// <remarks>
        /// Inherits List in order to support List.Contains instance method as well
        /// as standard Enumerable.Contains/Any extension methods.
        /// </remarks>
        class Printer<T> : List<T>
        {
            public Printer(IEnumerable collection)
            {
                this.AddRange(collection.Cast<T>());
            }

            public override string ToString()
            {
                return "{" + this.ToConcatenatedString(t => t.ToString(), "|") + "}";
            }
        }
    }
}
