using System;

namespace AParse
{
    public class NotRule : Rule
    {
        public NotRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            var old = state.Clone();
            if (Child.Match(state))
            {
                state.Assign(old);
                return false;
            }
            return true;
        }

        public override string Definition
        {
            get { return String.Format("!{0}", Child.ToString()); }
        }
    }

}
