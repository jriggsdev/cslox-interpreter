namespace CsLox.Cli.Expressions;

public interface IExpressionVisitor<out TOut>
{
    TOut Visit(BinaryExpression expression);
    TOut Visit(GroupingExpression expression);
    TOut Visit(LiteralExpression expression);
    TOut Visit(UnaryExpression expression);
}