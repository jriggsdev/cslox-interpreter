namespace CsLox.Cli;

public record ScanTokenError
{
    public required int Line { get; init; }
    public required string Message { get; init; } 
}