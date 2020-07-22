namespace Main2Cache
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Class to generate the key for the query to cache.
    /// </summary>
    public static class GetQueryKey
    {
        /// <summary>
        /// Determines if the query can be first evaluated locally to add the posted parameters to the cache key.
        /// </summary>
        static Func<Expression, bool> CanBeEvaluatedLocally
        {
            get
            {
                return expression =>
                {
                    // don't evaluate parameters
                    if (expression.NodeType == ExpressionType.Parameter)
                        return false;

                    // can't evaluate queries
                    if (typeof(IQueryable).IsAssignableFrom(expression.Type))
                        return false;

                    return true;
                };
            }
        }

        /// <summary>
        /// Generates the key of the cache based on the query expression.
        /// This allows the system, when CRUD operations occurs, determin which cached result to expire.
        /// </summary>
        /// <param name="query">The query item to cache</param>
        /// <returns>Returns the stored cached json result</returns>
        public static string GetKey(this IQueryable query)
        {
            string result = string.Empty;
            Expression expression = query.Expression;
            expression = Evaluator.PartialEval(expression, CanBeEvaluatedLocally);
            expression = LocalCollectionExpander.Rewrite(expression);
            string key = expression.ToString();
            return key;
        }
    }
}
