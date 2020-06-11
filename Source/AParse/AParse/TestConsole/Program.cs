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
            var postal = Grammar.IsMatch<PostalGrammar>("123", PostalGrammar.Postal);


            Grammar.OutputGrammar(typeof(PostalGrammar));
            
            
        }
    }
}