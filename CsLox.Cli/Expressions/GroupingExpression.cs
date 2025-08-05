namespace CsLox.Cli.Expressions;

public class GroupingExpression : IExpression
{
    public IExpression Expression { get; }

    public GroupingExpression(IExpression expression)
    {
        Expression = expression;
    }

    public TOut Accept<TOut>(IExpressionVisitor<TOut> visitor)
    {
        return visitor.Visit(this);
    }
}