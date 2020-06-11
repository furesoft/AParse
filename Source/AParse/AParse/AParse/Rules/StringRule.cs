using System;

namespace AParse
{
    public class StringRule : Rule
    {
        string s;

        public StringRule(string s)
        {
            this.s = s;
        }

        protected override bool InternalMatch(ParserState state)
        {
            if (!state.input.Substring(state.pos).StartsWith(s))
                return false;
            state.pos += s.Length;
            return true;
        }

        public override string Definition
        {
            get { return String.Format("\"{0}\"", s); }
        }
    }

}
