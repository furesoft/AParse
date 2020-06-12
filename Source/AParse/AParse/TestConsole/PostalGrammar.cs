using System;
using AParse;

namespace TestConsole
{
    public class PostalGrammar : Grammar
    {
        public static Rule NonZeroDigit = CharRange('1', '9');
        public static Rule Digit = "0" | NonZeroDigit;


        public static Rule Postal = Node(NonZeroDigit + Digit);
        public static Rule Test = !Postal;
    }

    public class ArrayGrammar : PostalGrammar
    {
        public static Rule Digits = Node(Digit^1);
        public static Rule ArrayItem = DelimitedBy(Digits, MatchChar(',')); // (Digits + ZeroOrMore(MatchChar(',') | Digits));
        public static Rule Array = Node('[' + ArrayItem + ']');
    }
}