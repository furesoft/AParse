using System.Collections.Generic;
using AParse;

namespace TestConsole
{
    public enum TestTokens { EOF, OpenBracket, CloseBracket, Number, Comma, Whitespace }
    class ArrayParser : BaseParser<TestTokens, AstNode>
    {
        public ArrayParser(Tokenizer<TestTokens> tokenizer) : base(tokenizer)
        {
        }

        protected override AstNode ParseInternal()
        {
            this.getToken();

            var ast = parseArray();

            Expect(TestTokens.EOF);
            return ast;
        }

        private AstNode parseArray()
        {
            Expect(TestTokens.OpenBracket);

            var values = parseValues();

            Expect(TestTokens.CloseBracket);

            return new ArrayNode { Values = values };
        }

        private List<AstNode> parseValues()
        {
            //int,int,int
            List<AstNode> res = new List<AstNode>();

            string tokenValue = null;

            while (Accept(TestTokens.Number, out tokenValue))
            {
                var v = new ValueNode { Value = int.Parse(tokenValue) };
                res.Add(v);

                Expect(TestTokens.Comma);
            }

            return res;
        }
    }
}
