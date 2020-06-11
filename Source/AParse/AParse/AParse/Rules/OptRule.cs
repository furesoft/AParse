using System;

namespace AParse
{
    public class OptRule : Rule
    {
        public OptRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            Child.Match(state);
            return true;
        }

        public override string Definition
        {
            get { return String.Format("{0}?", Child.ToString()); }
        }
    }

}
