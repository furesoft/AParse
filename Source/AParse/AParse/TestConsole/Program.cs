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
            var postal = Grammar.Parse<ArrayGrammar>(" a", ArrayGrammar.t);


            Grammar.OutputGrammar(typeof(ArrayGrammar));
            
            
        }
    }
}