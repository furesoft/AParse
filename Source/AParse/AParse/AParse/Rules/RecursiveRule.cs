using System;

namespace AParse
{
    public class RecursiveRule : Rule
    {
        Func<Rule> ruleGen;

        public RecursiveRule(Func<Rule> ruleGen)
        {
            this.ruleGen = ruleGen;
        }

        protected override bool InternalMatch(ParserState state)
        {
            if (Children.Count == 0)
                Children.Add(ruleGen());
            return Child.Match(state);
        }

        public override string ToString()
        {
            return Name ?? (Children.Count > 0 ? Children[0].ToString() : "recursive");
        }

        public override string Definition
        {
            get { return ruleGen().Definition; }
        }
    };

}
