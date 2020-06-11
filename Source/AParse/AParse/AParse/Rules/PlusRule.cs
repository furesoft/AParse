using System;

namespace AParse
{
    public class PlusRule : Rule
    {
        public PlusRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            if (!Child.Match(state)) return false;
            while (Child.Match(state)) { }
            return true;
        }

        public override string Definition
        {
            get { return String.Format("{0}+", Child.ToString()); }
        }
    }

}
