using System;
using System.Collections.Generic;
using System.Linq;

namespace AParse
{
    public abstract class Rule
    {
        public Rule(IEnumerable<Rule> rules)
        {
            if (rules.Any(r => r == null)) throw new Exception("No child rule can be null");
            Children = new List<Rule>(rules);
        }

        public Rule(params Rule[] rules)
            : this((IEnumerable<Rule>)rules)
        {
        }

        public List<Rule> Children = new List<Rule>();

        public string Name { get; set; }

        public Rule Child { get { return Children[0]; } }

        protected abstract bool InternalMatch(ParserState state);

        public bool Match(ParserState state)
        {
            // HINT: This is a good place to set a conditional break-point when debugging.
            // Using the Name = X as a condition.
            return InternalMatch(state);            
        }

        public static Rule operator +(Rule r1, Rule r2)
        {
            return Grammar.Seq(r1, r2);
        }

        public static Rule operator !(Rule r1)
        {
            return Grammar.Not(r1);
        }

        public static Rule operator |(Rule r1, Rule r2)
        {
            return Grammar.Choice(r1, r2);
        }

        public static Rule operator ^(Rule rule, int type)
        {
            if (type == 0) return Grammar.ZeroOrMore(rule);
            if (type == 1) return Grammar.OneOrMore(rule);

            return rule;
        }

        public static Rule operator *(Rule rule, int count)
        {
            Rule oldRule = rule;
            for (int i = 0; i < count - 1; i++)
            {
                oldRule = oldRule + rule;
            }
            
            return oldRule;
        }

        public static implicit operator Rule(char c)
        {
            return Grammar.MatchChar(_ => _ == c);
        }

        public static implicit operator Rule(string str)
        {
            return Grammar.MatchString(str);
        }

        public override string ToString()
        {
            return Name ?? Definition;
        }

        public List<Node> Parse(string input)
        {
            var state = new ParserState() { input = input, pos = 0 };
            if (!Match(state))
                throw new Exception(String.Format("Rule {0} failed to match", Name));
            return state.nodes;
        }

        public bool Match(string input)
        {
            var state = new ParserState() { input = input, pos = 0 };
            return Match(state);
        }

        public Rule SetName(string s)
        {
            Name = s;
            return this;
        }

        public abstract string Definition { get; }
    }
}