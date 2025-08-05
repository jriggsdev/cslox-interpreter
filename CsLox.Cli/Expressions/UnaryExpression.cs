namespace CsLox.Cli.Expressions;

public class UnaryExpression : IExpression
{
    public Token Operator { get; }
    public IExpression Right { get; }

    public UnaryExpression(Token op, IExpression right)
    {
        Operator = op;
        Right = right;
    }

    public TOut Accept<TOut>(IExpressionVisitor<TOut> visitor)
    {
        return visitor.Visit(this);
    }
}