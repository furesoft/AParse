using System.Collections.Generic;

namespace AParse
{
    public interface ITokenizer<TToken>
    {
        IEnumerable<Token<TToken>> Tokenize(string lqlText);
    }
}