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
            var postal = Grammar.Parse<ArrayGrammar>("[1,2,3,]", ArrayGrammar.Array);


            Grammar.OutputGrammar(typeof(ArrayGrammar));
            
            
        }
    }
}