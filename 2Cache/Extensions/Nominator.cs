namespace Main2Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Performs bottom-up analysis to determine which nodes can possibly
    /// be part of an evaluated sub-tree.
    /// </summary>
    internal class Nominator : ExpressionVisitor
    {
        Func<Expression, bool> canBeEvaluated;
        HashSet<Expression> candidates;
        bool cannotBeEvaluated;

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="canBeEvaluated">Assess if the expression can be evaluated locally</param>
        internal Nominator(Func<Expression, bool> canBeEvaluated)
        {
            this.canBeEvaluated = canBeEvaluated;
        }

        /// <summary>
        /// Analyze the expression.
        /// </summary>
        /// <param name="expression">The expression to visit</param>
        /// <returns>Returns instance of <see cref="HashSet{Expression}"/></returns>
        internal HashSet<Expression> Nominate(Expression expression)
        {
            this.candidates = new HashSet<Expression>();
            this.Visit(expression);
            return this.candidates;
        }

        /// <inheritdoc cref="ExpressionVisitor.Visit(Expression)"/>
        public override Expression Visit(Expression expression)
        {
            if (expression == null)
            {
                return expression;
            }

            // store the current flag
            bool saveCannotBeEvaluated = this.cannotBeEvaluated;

            // set it to false
            this.cannotBeEvaluated = false;

            // call the base method
            base.Visit(expression);

            // if the flag is still false
            if (!this.cannotBeEvaluated)
            {
                if (this.canBeEvaluated(expression))
                {
                    this.candidates.Add(expression);
                }
                else
                {
                    this.cannotBeEvaluated = true;
                }
            }

            this.cannotBeEvaluated |= saveCannotBeEvaluated;
            return expression;
        }
    }
}
