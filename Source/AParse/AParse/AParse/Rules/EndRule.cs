namespace AParse
{
    public class EndRule : Rule
    {
        protected override bool InternalMatch(ParserState state)
        {
            return state.pos == state.input.Length;
        }

        public override string Definition
        {
            get { return "_EOF_"; }
        }
    }

}
