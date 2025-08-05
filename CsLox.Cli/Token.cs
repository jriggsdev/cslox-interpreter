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
        return new Token(TokenType.LeftParen, "(", line); 
    }
    
    public static Token RightParen(int line)
    {
        return new Token(TokenType.RightParen, ")", line); 
    }

    public static Token LeftBrace(int line)
    {
        return new Token(TokenType.LeftBrace, "{", line); 
    }
    
    public static Token RightBrace(int line)
    {
        return new Token(TokenType.RightBrace, "}", line); 
    }
    
    public static Token Comma(int line)
    {
        return new Token(TokenType.Comma, ",", line);
    } 
    
    public static Token Dot(int line)
    {
        return new Token(TokenType.Dot, ".", line);
    }

    public static Token Minus(int line)
    {
        return new Token(TokenType.Minus, "-", line); 
    }
    
    public static Token Plus(int line)
    {
        return new Token(TokenType.Plus, "+", line); 
    }

    public static Token Semicolon(int line)
    {
        return new Token(TokenType.Semicolon, ";", line); 
    }

    public static Token Star(int line)
    {
        return new Token(TokenType.Star, "*", line);
    }

    public static Token Bang(int line)
    {
        return new Token(TokenType.Bang, "!", line); 
    }

    public static Token BangEqual(int line)
    {
        return new Token(TokenType.BangEqual, "!=", line); 
    }
    
    public static Token Equal(int line)
    {
        return new Token(TokenType.Equal, "=", line); 
    }

    public static Token EqualEqual(int line)
    {
        return new Token(TokenType.EqualEqual, "==", line); 
    }

    public static Token Greater(int line)
    {
        return new Token(TokenType.Greater, ">", line); 
    }
    
    public static Token GreaterEqual(int line)
    {
        return new Token(TokenType.GreaterEqual, ">=", line); 
    }
    
    public static Token Less(int line)
    {
        return new Token(TokenType.Less, ">", line); 
    }
    
    public static Token LessEqual(int line)
    {
        return new Token(TokenType.LessEqual, ">=", line); 
    }

    public static Token Slash(int line)
    {
        return new Token(TokenType.Slash, "/", line);
    }

    public static Token String(int line, string lexeme, string literal)
    {
        return new Token(TokenType.String, lexeme, line, literal);
    }
    
    public static Token Number(int line, string lexeme, double literal)
    {
        return new Token(TokenType.Number, lexeme, line, literal);
    }
    
    public static Token Identifier(int line, string lexeme)
    {
        return new Token(TokenType.Identifier, lexeme, line, lexeme);
    }

    public static Token Keyword(TokenType tokenType, int line, string lexeme)
    {
        return new Token(tokenType, lexeme, line);
    }
}