using System;

namespace AParse
{
    public class ZeroOrMoreRule : Rule
    {
        public ZeroOrMoreRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            while (Child.Match(state)) { };
            return true;
        }

        public override string Definition
        {
            get { return String.Format("{0}*", Child.ToString()); }
        }
    }

}
