using System;
using AParse;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenizer = new Tokenizer<TestTokens>();
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.OpenBracket, @"\[", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.CloseBracket, @"\]", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.Comma, @"\,", 0));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.Whitespace, "\\s", 0, true));
            tokenizer.AddDefinition(new TokenDefinition<TestTokens>(TestTokens.Number, @"[0-9]+", 0));

            foreach (var token in tokenizer.Tokenize("[123,456,789,]"))
            {
                Console.WriteLine(token.TokenType + ": " + token.Value);
            }

            var p = new ArrayParser(tokenizer);
            var res = p.Parse("[123,456,789]");
        }
    }
}