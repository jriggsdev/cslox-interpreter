namespace CsLox.Cli;

public record Token
{
    public TokenType Type { get; }
    public string Lexeme { get; }
    public object? Literal { get; }
    public int Line { get; }

    private Token(TokenType type, string lexeme, int line)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = null;
        Line = line;
    }
    
    private Token(TokenType type, string lexeme, int line, object literal)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = literal;
        Line = line;
    }

    public override string ToString()
    {
        return $"{Type} {Lexeme} {Literal}";
    }
    
    public static Token LeftParen(int line)
    {
        return new Token(TokenType.LEFT_PAREN, "(", line); 
    }
    
    public static Token RightParen(int line)
    {
        return new Token(TokenType.RIGHT_PAREN, ")", line); 
    }

    public static Token LeftBrace(int line)
    {
        return new Token(TokenType.LEFT_BRACE, "{", line); 
    }
    
    public static Token RightBrace(int line)
    {
        return new Token(TokenType.RIGHT_BRACE, "}", line); 
    }
    
    public static Token Comma(int line)
    {
        return new Token(TokenType.COMMA, ",", line);
    } 
    
    public static Token Dot(int line)
    {
        return new Token(TokenType.DOT, ".", line);
    }

    public static Token Minus(int line)
    {
        return new Token(TokenType.MINUS, "-", line); 
    }
    
    public static Token Plus(int line)
    {
        return new Token(TokenType.PLUS, "+", line); 
    }

    public static Token Semicolon(int line)
    {
        return new Token(TokenType.SEMICOLON, ";", line); 
    }

    public static Token Star(int line)
    {
        return new Token(TokenType.STAR, "*", line);
    }

    public static Token Bang(int line)
    {
        return new Token(TokenType.BANG, "!", line); 
    }

    public static Token BangEqual(int line)
    {
        return new Token(TokenType.BANG_EQUAL, "!=", line); 
    }
    
    public static Token Equal(int line)
    {
        return new Token(TokenType.EQUAL, "=", line); 
    }

    public static Token EqualEqual(int line)
    {
        return new Token(TokenType.EQUAL_EQUAL, "==", line); 
    }

    public static Token Greater(int line)
    {
        return new Token(TokenType.GREATER, ">", line); 
    }
    
    public static Token GreaterEqual(int line)
    {
        return new Token(TokenType.GREATER_EQUAL, ">=", line); 
    }
    
    public static Token Less(int line)
    {
        return new Token(TokenType.LESS, ">", line); 
    }
    
    public static Token LessEqual(int line)
    {
        return new Token(TokenType.LESS_EQUAL, ">=", line); 
    }

    public static Token Slash(int line)
    {
        return new Token(TokenType.SLASH, "/", line);
    }

    public static Token String(int line, string lexeme, string literal)
    {
        return new Token(TokenType.STRING, lexeme, line, literal);
    }
    
    public static Token Number(int line, string lexeme, double literal)
    {
        return new Token(TokenType.NUMBER, lexeme, line, literal);
    }
    
    public static Token Identifier(int line, string lexeme)
    {
        return new Token(TokenType.IDENTIFIER, lexeme, line, lexeme);
    }

    public static Token Keyword(TokenType tokenType, int line, string lexeme)
    {
        return new Token(tokenType, lexeme, line);
    }
}