using System;
namespace AParse
{
    public class BaseParser
    {
        string Source;
        int pos = 0;
        protected char current;

        public void Expect(char token)
        {
            if (token != current) throw new Exception($"Expected '{token}' but found '{current}' at pos '{pos}'");
            getToken();
        }

        public bool Accept(char token)
        {
            if (token != current) return false;
            getToken();
            return true;
        }

        protected void getToken()
        {
            if(pos == Source.Length)
            {
                current = '\0';
            }
            else
            {
                current = Source[pos++];
            }
        }

        public object Parse(string source)
        {
            this.Source = source;

            return ParseInternal();
        }

        protected virtual object ParseInternal() {
            return null;
        }
    }
}