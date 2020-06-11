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
            string s = "Int32 f() { ldc_i4_1; ret; }";
            var result = ILGrammar.ILFunc.Parse(s);

            var t = JsonConvert.SerializeObject(result, Formatting.Indented);

            Console.WriteLine(t);
            Console.ReadLine();
        }
    }
}