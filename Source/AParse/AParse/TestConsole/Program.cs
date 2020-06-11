using System;
using System.IO;
using Newtonsoft.Json;
using AParse;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "{\"hello\": true, \"world\": [false, 2, 3]}";
            var result = Grammar.Parse<JsonGrammar>(s, JsonGrammar.Object);
            var transformer = new TestTransofmer();  
            var ast = transformer.Eval(result[0]);

            var t = JsonConvert.SerializeObject(ast, Formatting.Indented);
            Grammar.OutputGrammar(typeof(JsonGrammar));
            Console.WriteLine(t);
            Console.ReadLine();
        }
    }
}