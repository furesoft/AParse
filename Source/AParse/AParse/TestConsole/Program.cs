using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new TestParser();
            var res = p.Parse("ab?c");
        }
    }
}
