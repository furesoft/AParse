using System;
using System.Text.RegularExpressions;

namespace AParse
{
    public class RegexRule : Rule
    {
        Regex re;

        public RegexRule(Regex re)
        {
            this.re = re;
        }

        protected override bool InternalMatch(ParserState state)
        {
            var m = re.Match(state.input, state.pos);
            if (m == null || m.Index != state.pos) return false;
            state.pos += m.Length;
            return true;
        }

        public override string Definition
        {
            get { return String.Format("regex({0})", re.ToString()); }
        }
    }

}
