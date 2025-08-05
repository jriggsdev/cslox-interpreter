namespace CsLox.Cli;

public class Scanner
{
    private static readonly IReadOnlyDictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
    {
        { "and", TokenType.And },
        { "class", TokenType.Class },
        { "else", TokenType.Else },
        { "false", TokenType.False },
        { "for", TokenType.For },
        { "fun", TokenType.Fun },
        { "if", TokenType.If },
        { "nil", TokenType.Nil },
        { "or", TokenType.Or },
        { "print", TokenType.Print },
        { "return", TokenType.Return },
        { "super", TokenType.Super },
        { "this", TokenType.This },
        { "true", TokenType.True },
        { "var", TokenType.Var },
        { "while", TokenType.While },
    };
    
    private readonly string _source;

    private int _start = 0;
    private int _current = 0;
    private int _line = 1;
    
    public Scanner(string source)
    {
        _source = source;
    }

    public IEnumerable<ScanTokenResult> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            var c = Advance();
            
            switch (c)
            {
                // Whitespace
                case ' ':
                case '\r':
                case '\t':
                    break; // ignore whitespace
                
                case '\n':
                    _line++;
                    break;
                
                case '(':
                    yield return ScanTokenResult.TokenResult(Token.LeftParen(_line));
                    break;
                
                case ')':
                    yield return ScanTokenResult.TokenResult(Token.RightParen(_line));
                    break;
                
                case '{':
                    yield return ScanTokenResult.TokenResult(Token.LeftBrace(_line));
                    break;
                
                case '}':
                    yield return ScanTokenResult.TokenResult(Token.RightBrace(_line));
                    break;
                
                case ',':
                    yield return ScanTokenResult.TokenResult(Token.Comma(_line));
                    break;
                
                case '.':
                    yield return ScanTokenResult.TokenResult(Token.Dot(_line));
                    break;
                
                case '-':
                    yield return ScanTokenResult.TokenResult(Token.Minus(_line));
                    break;
                
                case '+':
                    yield return ScanTokenResult.TokenResult(Token.Plus(_line));
                    break;
                
                case ';':
                    yield return ScanTokenResult.TokenResult(Token.Semicolon(_line));
                    break;
                
                case '*':
                    yield return ScanTokenResult.TokenResult(Token.Star(_line));
                    break;
                
                case '!':
                    var bangToken = Match('=') 
                        ? Token.BangEqual(_line) 
                        : Token.Bang(_line);
                    
                    yield return ScanTokenResult.TokenResult(bangToken);
                    break;
                
                case '=':
                    var equalsToken = Match('=') 
                        ? Token.EqualEqual(_line) 
                        : Token.Equal(_line);
                    
                    yield return ScanTokenResult.TokenResult(equalsToken);
                    break;
                
                case '<':
                    var lessToken = Match('=') 
                        ? Token.LessEqual(_line) 
                        : Token.Less(_line);
                    
                    yield return ScanTokenResult.TokenResult(lessToken);
                    break;
                
                case '>':
                    var greaterToken = Match('=') 
                        ? Token.GreaterEqual(_line)
                        : Token.Greater(_line);
                    
                    yield return ScanTokenResult.TokenResult(greaterToken);
                    break;
                
                case '/':
                    // scan comment
                    if (Match('/')) {
                        // This Lexeme is a comment, so read till the new line character and don't return a Token
                        while (Peek() != '\n' && !IsAtEnd())
                        {
                            Advance();
                        }

                        break; // comments are ignored
                    }

                    // scan multi-line comment
                    if (Match('*'))
                    {
                        while (!(Peek() == '*' && PeekNext() == '/'))
                        {
                            if (IsAtEnd())
                            {
                                yield return ScanTokenResult.ErrorResult(_line, "Unterminated multi-line comment");
                                break;
                            }

                            if (Peek() == '\n')
                            {
                                _line++;
                            }
        
                            Advance();
                        }

                        Advance();
                        Advance();

                        break;
                    }
                    
                    yield return ScanTokenResult.TokenResult(Token.Slash(_line));
                    break;
                
                case '"': 
                    yield return ScanString();
                    break;
                
                case var _ when char.IsAsciiDigit(c):
                    yield return ScanNumber();
                    break;
                
                case var _ when char.IsAsciiLetter(c) || c == '_':
                    yield return ScanKeywordOrIdentifier();
                    break;
                    
                default:
                    var errorMessage = $"Encountered unexpected character {c} at position {_start}";
                    yield return ScanTokenResult.ErrorResult(_line, errorMessage);
                    break;
            }
        }
    }

    private bool IsAtEnd()
    {
        return _current >= _source.Length;
    }

    private char Advance()
    {
        return _source[_current++];
    }

    private string GetLexeme()
    { 
        return _source[_start.._current];
    }

    private bool Match(char expected)
    {
        var nextChar = Peek();

        if (nextChar != expected)
        {
            return false;
        }
        
        _current++;
        return true;
    }

    private char Peek()
    {
        return IsAtEnd() ? '\0' : _source[_current];
    }

    private char PeekNext()
    {
        return _current + 1 >= _source.Length ? '\0' : _source[_current + 1];
    }

    private ScanTokenResult ScanString()
    {
        var nextChar = Peek();
        while (nextChar != '"')
        {
            if (IsAtEnd())
            {
                return ScanTokenResult.ErrorResult(_line, "Unterminated string");
            }
            
            if (nextChar == '\n')
            {
                _line++;
            }
            
            Advance();
            nextChar = Peek();
        }

        if (IsAtEnd())
        {
            return ScanTokenResult.ErrorResult(_line, "Unterminated string");
        }

        Advance();
        
        var lexeme = GetLexeme();
        
        // _start and (_current - 1) are the indexes of the quotes, and we want the value inside the quotes
        var literal = _source[(_start + 1)..(_current - 1)];
        
        return ScanTokenResult.TokenResult(Token.String(_line, lexeme, literal));
    }

    private ScanTokenResult ScanNumber()
    {
        // Scan all the digits for the integer part of the literal
        while (char.IsAsciiDigit(Peek()))
        {
            Advance();
        }

        
        // If the next two characters are a '.' followed by another digit, then scan the fractional part of the literal
        if (Peek() == '.' && char.IsAsciiDigit(PeekNext()))
        {
            Advance();
            
            while (char.IsAsciiDigit(Peek()))
            {
                Advance();
            }
        }
        
        var lexeme = GetLexeme();
        var literal = double.Parse(_source[_start.._current]); 
        return ScanTokenResult.TokenResult(Token.Number(_line, lexeme, literal));
    }

    private ScanTokenResult ScanKeywordOrIdentifier()
    {
        while (char.IsAsciiLetterOrDigit(Peek()))
        {
            Advance();
        }
        
        var lexeme = GetLexeme();

        return Keywords.TryGetValue(lexeme, out var tokenType) 
            ? ScanTokenResult.TokenResult(Token.Keyword(tokenType, _line, lexeme)) 
            : ScanTokenResult.TokenResult(Token.Identifier(_line, lexeme));
    }
}