namespace CsLox.Cli.Expressions;

public class BinaryExpression : IExpression
{
    public IExpression Left { get; }
    public IExpression Right { get; }
    public Token Operator { get; }
    
    public BinaryExpression(IExpression left, IExpression right, Token op)
    {
        Left = left;
        Right = right;
        Operator = op;
    }

    public TOut Accept<TOut>(IExpressionVisitor<TOut> visitor)
    {
        return visitor.Visit(this);
    }
}