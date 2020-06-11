using System;
using System.Collections.Generic;
using System.Linq;
using AParse;

namespace TestConsole
{
    class TestTransofmer : ASTTransformer<JObj>
    {
        public override JObj Eval(Node n)
        {
            switch (n.Label)
            {
                case "Name": return Eval(n[0]);
                case "Value": return Eval(n[0]);
                case "Number": return Eval(n[0]);
                case "Integer": return new JVal(int.Parse(n.Text));
                case "Float": return new JVal(double.Parse(n.Text));
                case "String": return new JVal(n.Text.Substring(1, n.Text.Length - 2));
                case "True": return new JVal(true);
                case "False": return new JVal(false);
                case "Null": return new JObject();
                case "Array": return new JArr(n.Nodes.Select(Eval));
                case "Object":
                    {
                        var r = new JObject();
                        foreach (var pair in n.Nodes)
                        {
                            var name = pair[0].Text;
                            var value = Eval(pair[1]);
                            r[name] = value;
                        }
                        return r;
                    }
                case "Pair":
                    var tmp = n.Nodes;
                return null;
                default:
                    throw new Exception("Unexpected node type " + n.Label);
            }
        }
    }
    interface JObj { }
    class JObject : Dictionary<string, JObj>, JObj
    {

    }
    class JVal : JObj
    {
        public JVal(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
    class JArr : List<JObj>, JObj
    {
        public JArr(IEnumerable<JObj> data)
        {
            AddRange(data);
        }
    }
}