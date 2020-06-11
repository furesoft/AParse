using System;
using AParse;

namespace TestConsole
{
    public class PostalGrammar : Grammar
    {
        public static Rule NonZeroDigit = CharRange('1', '9');
        public static Rule Digit = "0" | NonZeroDigit;

        public static Rule Postal = Node(NonZeroDigit + OneOrMore(Digit)); //ToDo: implement more operator ^0 und ^1
    }
}
