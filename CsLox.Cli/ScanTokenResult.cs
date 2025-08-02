namespace CsLox.Cli;

public enum ScanTokenResultType
{
    Token,
    Error,
}

public class ScanTokenResult
{
    private readonly ScanTokenResultType _type;
    private readonly Token? _token;
    private readonly ScanTokenError? _error;
    
    private ScanTokenResult(Token token)
    {
        _type = ScanTokenResultType.Token;
        _token = token;
    }

    private ScanTokenResult(int line, string message)
    {
        _type = ScanTokenResultType.Error;
        _error = new ScanTokenError
        {
            Line = line,
            Message = message
        };
    }
    
    public bool IsOk => _type == ScanTokenResultType.Token;

    public Token GetToken()
    {
        return _token ?? throw new InvalidOperationException("Cannot get a token for an error result");
    }

    public ScanTokenError Error()
    {
        return _error ?? throw new InvalidOperationException("Cannot get an error for a token result");
    }

    public static ScanTokenResult TokenResult(Token token)
    {
        return new ScanTokenResult(token);
    }

    public static ScanTokenResult ErrorResult(int line, string message)
    {
        return new ScanTokenResult(line, message);
    }
}