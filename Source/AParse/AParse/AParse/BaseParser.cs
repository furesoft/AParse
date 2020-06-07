using System;
using System.Linq;

namespace AParse
{
    public class BaseParser<TToken, TResult>
        where TToken : struct, IConvertible, IComparable
    {
        int pos = 0;
        protected Token<TToken> current;

        protected PatternTokenizer<TToken> Tokenizer;

        Token<TToken>[] tokens;

        public BaseParser(PatternTokenizer<TToken> tokenizer)
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
            var tmp = Accept(token, out Token<TToken> t);
            value = t.Value;

            return tmp;
        }

        public bool Accept(TToken tokentype, out Token<TToken> token)
        {
            if (tokentype.CompareTo(current.TokenType) != 0)
            {
                token = default(Token<TToken>);
                return false;
            }

            token = current;
            getToken();
            return true;
        }

        public bool Accept<T>(TToken token, out T value)
        {
            Token<TToken> tmp = null;
            var res = Accept(token, out tmp);

            if (res) { 
                value = (T)tmp.Factory(tmp.Value);
            }
            else
            {
                value = default(T);
            }
            return res;
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