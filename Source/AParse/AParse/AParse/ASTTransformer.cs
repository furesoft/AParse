using System;
namespace AParse
{
    public abstract class ASTTransformer<T>
    {
        public abstract T Eval(Node n);
    }
}