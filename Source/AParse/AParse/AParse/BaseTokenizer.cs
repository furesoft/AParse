using System;

namespace AParse
{
    public class BaseTokenizer<TToken>
    {
        public virtual TToken NextToken()
        {
            return default(TToken);
        }

        public TToken[] Tokenize(string src)
        {
            return null;
        }
    }
}