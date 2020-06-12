using System;

namespace AParse
{
    public class AtRule : Rule
    {
        public AtRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            var old = state.Clone();
            bool result = Child.Match(state);
            state.Assign(old);
            return result;
        }

        public override string Definition
        {
            get { return String.Format("At({0})", Child.ToString()); }
        }
    }
}