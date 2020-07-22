namespace Main2Cache
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Class to handle the expansion of the queries to include local variables.
    /// </summary>
    public class LocalCollectionExpander : ExpressionVisitor
    {

        /// <summary>
        /// Handles the rewrite of the query.
        /// </summary>
        /// <param name="expression">The expression to visit</param>
        /// <returns>Returns a new instance of <see cref="Expression"/> evaluated</returns>
        public static Expression Rewrite(Expression expression)
        {
            return new LocalCollectionExpander().Visit(expression);
        }
    }
}
