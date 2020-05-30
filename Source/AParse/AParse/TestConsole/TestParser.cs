using AParse;

namespace TestConsole
{
    public enum TestTokens {EOF, OpenBracket, CloseBracket, Disjunction, Optional, Letter}
    class TestParser : BaseParser<TestTokens>
    {
        public TestParser(Tokenizer<TestTokens> tokenizer) : base(tokenizer)
        {
        }

        protected override object ParseInternal()
        {
            this.getToken();
            AstNode ast = parseDisjunction();
            Expect(TestTokens.EOF);
            return ast;
        }

        private AstNode parseAtom()
        {
            if (Accept(TestTokens.OpenBracket))
            {
                AstNode node = parseDisjunction();
                Expect(TestTokens.CloseBracket);
                return node;
            }
            
                AstNode ast = new AstNode { Name = "Literal", Value = current };
                Accept(current.TokenType);
                return ast;
        }

        private AstNode parseTerm()
        {
            var ast = parseAtom();
            if (ast == null) return null;
            if (Accept(TestTokens.Optional)) return new AstNode { Name = "Disjunction", Value = new { ast, second = new AstNode { Name = "empty_alternative" } } };
            return ast;
        }

        private AstNode parseAlternative()
        {
            AstNode ast = parseTerm();
            if (ast == null) return new AstNode { Name = "epty_alternative" };
            while (true)
            {
                AstNode next = parseTerm();
                if (next == null) return ast;
                ast = new AstNode { Name = "alternative", Value = new { ast, next } };
            }
        }

        private AstNode parseDisjunction()
        {
            AstNode ast = parseAlternative();
            while (Accept(TestTokens.Disjunction))
            {
                ast = new AstNode { Name = "disjunction", Value = new { ast, second = parseAlternative() } };
            }

            return ast;
        }
    }
}
