namespace CsLox.Cli.Expressions;

public class LiteralExpression : IExpression
{
    public object? Value { get; }
    
    public LiteralExpression(object? value)
    {
        Value = value;
    }

    public TOut Accept<TOut>(IExpressionVisitor<TOut> visitor)
    {
        return visitor.Visit(this);
    }
}