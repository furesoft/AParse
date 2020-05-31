using System.Collections.Generic;

namespace TestConsole
{
    class AstNode
    {
       
    }
    class ValueNode : AstNode
    {
        public int Value { get; set; }
    }
    class ArrayNode : AstNode
    {
        public List<AstNode> Values { get; set; } = new List<AstNode>();
    }
}