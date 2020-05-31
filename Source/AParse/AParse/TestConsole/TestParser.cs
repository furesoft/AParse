using System.Collections.Generic;
using AParse;

namespace TestConsole
{
    public enum TestTokens {EOF, OpenBracket, CloseBracket, Number, Comma, Whitespace}
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
            if(Accept(TestTokens.OpenBracket))
            {
                var values = parseValues();

                Expect(TestTokens.CloseBracket);

                return new ArrayNode { Values = values };
            }

            return new AstNode();
        }

        private List<AstNode> parseValues()
        {
            //int,int,int
            List<AstNode> res = new List<AstNode>();

            while(Accept(TestTokens.Number))
            {
                SkipWhitespace();
                var v = new ValueNode { Value = int.Parse(current.Value) };
                res.Add(v);

                Expect(TestTokens.Comma);
            }

            return res;
        }

        private void SkipWhitespace()
        {
            if(current.TokenType == TestTokens.Whitespace)
            {
                getToken();
            }
        }


    }
}
