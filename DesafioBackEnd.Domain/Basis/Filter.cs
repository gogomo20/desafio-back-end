using System.Linq.Expressions;

namespace StockManager.Domain.Basis;

public class Filter<T>
{
    private Expression<Func<T, bool>> _expression;

    public Filter<T> And(Expression<Func<T, bool>> expression)
    {
        _expression = _expression == null ? expression : Combine(_expression, expression, Expression.AndAlso);
        return this;
    }

    public Filter<T> Or(Expression<Func<T, bool>> expression)
    {
        _expression = _expression == null ? expression : Combine(_expression, expression, Expression.OrElse);
        return this;
    }
    
    public Expression<Func<T, bool>> BuildExpression() => _expression;

    private Expression<Func<T, bool>> Combine(
        Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right,
        Func<Expression, Expression, BinaryExpression> merge)
    {
        var parameter = left.Parameters[0];
        var visitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);
        var rightBody = visitor.Visit(right.Body);

        var body = merge(left.Body, rightBody);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
    private class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _from;
        private readonly ParameterExpression _to;

        public ReplaceParameterVisitor(ParameterExpression from, ParameterExpression to)
        {
            _from = from;
            _to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _from ? _to : base.VisitParameter(node);
    }
}