namespace AParse
{
    public class TokenMatch<TToken>
    {
        public TToken TokenType { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Precedence { get; set; }

        public bool Ignore { get; set; }   
    }

    public class Token<TToken>
    {
        public Token(TToken tokenType)
        {
            TokenType = tokenType;
            Value = string.Empty;
        }

        public Token(TToken tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public TToken TokenType { get; set; }
        public string Value { get; set; }

        public Token<TToken> Clone()
        {
            return new Token<TToken>(TokenType, Value);
        }

        public static implicit operator TToken(Token<TToken> token)
        {
            return token.TokenType;
        }
    }
}