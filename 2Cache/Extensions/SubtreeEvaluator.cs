namespace Main2Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Evaluates & replaces sub-trees recursivelly till the root item is found
    /// </summary>
    internal class SubtreeEvaluator : ExpressionVisitor
    {
        /// <summary>
        /// The expression set.
        /// </summary>
        HashSet<Expression> candidates;

        /// <summary>
        /// Constructor with parameter <see cref="HashSet{Expression}"/>
        /// </summary>
        /// <param name="candidates"></param>
        internal SubtreeEvaluator(HashSet<Expression> candidates)
        {
            this.candidates = candidates;
        }

        /// <summary>
        /// Start the evaluation of the tree.
        /// </summary>
        /// <param name="exp">Instance of <see cref="Expression"/></param>
        /// <returns>Returns a processed <see cref="Expression"/> with the parameter replaces</returns>
        internal Expression Eval(Expression exp)
        {
            return this.Visit(exp);
        }

        /// <summary>
        /// Visits the expression and evaluates the item.
        /// </summary>
        /// <param name="exp">The instance of <see cref="Expression"/> to visit</param>
        /// <returns>Returns an instance of <see cref="Expression"/> evaluated</returns>
        public override Expression Visit(Expression exp)
        {
            return exp != null ? this.candidates.Contains(exp) ? this.Evaluate(exp) : base.Visit(exp) : null;
        }

        /// <summary>
        /// Evaluates the epression for constant values.
        /// </summary>
        /// <param name="e">Instance if <see cref="Expression"/> to evaluate</param>
        /// <returns>Returns the evaluated instance of <see cref="Expression"/></returns>
        private Expression Evaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                return e;
            }

            LambdaExpression lambda = Expression.Lambda(e);
            Delegate fn = lambda.Compile();
            return Expression.Constant(fn.DynamicInvoke(null), e.Type);
        }
    }
}
