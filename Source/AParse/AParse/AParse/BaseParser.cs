using System;
using System.Linq;

namespace AParse
{
    public class BaseParser<TToken>
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
            if (token.CompareTo(current.TokenType) != 0) throw new Exception($"Expected '{token}' but found '{current}' at pos '{pos}'");
            getToken();
        }

        public bool Accept(TToken token)
        {
            if (token.CompareTo(current.TokenType) != 0) return false;
            getToken();
            return true;
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

        public object Parse(string source)
        {
            tokens = Tokenizer.Tokenize(source).ToArray();

            return ParseInternal();
        }

        protected virtual object ParseInternal() {
            return null;
        }
    }
}