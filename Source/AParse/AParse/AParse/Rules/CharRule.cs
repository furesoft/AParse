using System;

namespace AParse
{
    public class CharRule : Rule
    {
        Predicate<char> predicate;

        public CharRule(Predicate<char> p)
        {
            predicate = p;
        }

        protected override bool InternalMatch(ParserState state)
        {
            if (state.pos >= state.input.Length)
                return false;
            if (!predicate(state.input[state.pos]))
                return false;
            state.pos++;
            return true;
        }

        public override string Definition
        {
            get { return "f(char)"; }
        }
    }

}
