namespace Main2Cache
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Main2Cache.Utilities;

    public static class GetQueryKey
    {
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

        public static string GetKey(this IQueryable query)
        {
            string result = string.Empty;
            Expression expression = query.Expression;
            expression = Evaluator.PartialEval(expression, CanBeEvaluatedLocally);
            expression = LocalCollectionExpander.Rewrite(expression);
            string key = expression.ToString();
            result = KeyGenerator.ComputeKey(key);

            return result;
        }
    }
}
