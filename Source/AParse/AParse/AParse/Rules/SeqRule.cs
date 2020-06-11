using System;
using System.Text;

namespace AParse
{
    public class SeqRule : Rule
    {
        public SeqRule(params Rule[] rs)
            : base(rs)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            var old = state.Clone();
            foreach (var r in Children)
                if (!r.Match(state))
                {
                    state.Assign(old);
                    return false;
                }
            return true;
        }

        public override string Definition
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(Children[0].ToString());
                if (Children.Count == 2 && Children[1] is SeqRule)
                {
                    sb.Append(" + ");
                    sb.Append(Children[1].Definition);
                }
                else
                {
                    for (int i = 1; i < Children.Count; ++i)
                        sb.Append(" + ").Append(Children[i].ToString());
                }
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return String.Format("({0})", base.ToString());
        }
    }

}
