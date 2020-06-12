using System;

namespace AParse
{
    public class IgnoreRule : Rule
    {
        public IgnoreRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            var old = state.Clone();
            bool result = Child.Match(state);
            string newInput = old.Current.Substring(state.pos);
            state.input = newInput;
            state.pos = 0;

            return result;
        }

        public override string Definition
        {
            get { return String.Format("Ignore({0})", Child.ToString()); }
        }
    }
}