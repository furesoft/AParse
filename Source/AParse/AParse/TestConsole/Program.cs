using System;
using AParse;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenizer = new Tokenizer<TestTokens>();
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.OpenBracket, @"\(", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.CloseBracket, @"\)", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.Disjunction, @"\|", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.Optional, @"\?", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.Letter, @"[a-zA-Z]", 0));

            var p = new TestParser(tokenizer);
            var res = p.Parse("ab?c");
        }
    }
}
