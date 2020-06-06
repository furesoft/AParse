using System;
using System.Linq;

namespace AParse
{
    public class BaseParser<TToken, TResult>
        where TToken : struct, IConvertible, IComparable
    {
        int pos = 0;
        protected Token<TToken> current;

        protected Tokenizer<TToken> Tokenizer;

        Token<TToken>[] tokens;

        public BaseParser(Tokenizer<TToken> tokenizer)
        {
            this.Tokenizer = tokenizer;
        }

        public void Expect(TToken token)
        {
            if (token.CompareTo(current.TokenType) != 0) throw new Exception($"Expected '{token}' but found '{current.TokenType}' at pos '{pos}'");
            getToken();
        }

        public bool Accept(TToken token)
        {
            if (token.CompareTo(current.TokenType) != 0) return false;
            getToken();
            return true;
        }

        public bool Accept(TToken token, out string value)
        {
            if (token.CompareTo(current.TokenType) != 0) {
                value = null;
                return false;
            }

            value = current.Value;
            getToken();
            return true;
        }

        public Token<TToken> Peek()
        {
            return tokens[pos + 1];
        }

        protected void getToken()
        {
            if(pos == tokens.Length)
            {
                current = new Token<TToken>(default(TToken));
            }
            else
            {
                current = tokens[pos++];
            }
        }



        public TResult Parse(string source)
        {
            tokens = Tokenizer.Tokenize(source).ToArray();

            this.getToken();
            return ParseInternal();
        }

        protected virtual TResult ParseInternal() {
            return default(TResult);
        }
    }
}