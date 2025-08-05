namespace CsLox.Cli.Expressions;

public interface IExpression
{
    TOut Accept<TOut>(IExpressionVisitor<TOut> visitor);
}