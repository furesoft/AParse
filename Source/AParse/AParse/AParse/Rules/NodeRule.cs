using System;
using System.Collections.Generic;

namespace AParse
{
    public class NodeRule : Rule
    {
        /// <summary>
        /// Set to false to see how long it takes to parse grammars without memoization.
        /// </summary>
        private static readonly bool UseCache = true;

        public NodeRule(Rule r)
            : base(r)
        { }

        protected override bool InternalMatch(ParserState state)
        {
            try
            {
                if (UseCache)
                    return InternalMatchWithCaching(state);
                else
                    return InternalMatchWithoutCaching(state);
            }
            catch (Exception e)
            {
                Console.WriteLine("While parsing rule {0}, an error occured: {1}", Name, e.Message);
                throw;
            }
        }

        private bool InternalMatchWithCaching(ParserState state)
        {
            Node node;

            int start = state.pos;

            if (state.GetCachedResult(this, out node))
            {
                if (node == null)
                    return false;

                state.pos = node.End;
                state.nodes.Add(node);
                return true;
            }

            node = new Node(state.pos, Name, state.input);
            var oldNodes = state.nodes;
            state.nodes = new List<Node>();

            if (Child.Match(state))
            {
                node.End = state.pos;
                node.Nodes = state.nodes;
                oldNodes.Add(node);
                state.nodes = oldNodes;
                state.CacheResult(this, start, node);
                return true;
            }
            else
            {
                state.nodes = oldNodes;
                state.CacheResult(this, start, null);
                return false;
            }
        }

        private bool InternalMatchWithoutCaching(ParserState state)
        {
            Node node;

            node = new Node(state.pos, Name, state.input);
            var oldNodes = state.nodes;
            state.nodes = new List<Node>();

            if (Child.Match(state))
            {
                node.End = state.pos;
                node.Nodes = state.nodes;
                oldNodes.Add(node);
                state.nodes = oldNodes;
                return true;
            }
            else
            {
                state.nodes = oldNodes;
                return false;
            }
        }

        public override string Definition
        {
            get { return Child.Definition; }
        }
    }

}
