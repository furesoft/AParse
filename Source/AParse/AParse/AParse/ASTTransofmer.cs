using System;
namespace AParse
{
    public abstract class ASTTransofmer<T>
    {
        public abstract T Eval(Node n);
    }
}