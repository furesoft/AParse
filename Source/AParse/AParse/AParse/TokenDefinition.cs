using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AParse
{
    public class TokenDefinition<TToken>
    {
        private Regex _regex;
        private readonly TToken _returnsToken;
        private readonly int _precedence;

        private readonly bool _ignore;
        private readonly Func<string, object> _factory;

        public TokenDefinition(TToken returnsToken, string regexPattern, int precedence, bool ignore = false, Func<string, object> factory = null)
        {
            _regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            _returnsToken = returnsToken;
            _precedence = precedence;
            _ignore = ignore;
            _factory = factory;
        }

        public IEnumerable<TokenMatch<TToken>> FindMatches(string inputString)
        {
            var matches = _regex.Matches(inputString);
            for (int i = 0; i < matches.Count; i++)
            {
                yield return new TokenMatch<TToken>()
                {
                    StartIndex = matches[i].Index,
                    EndIndex = matches[i].Index + matches[i].Length,
                    TokenType = _returnsToken,
                    Value = matches[i].Value,
                    Precedence = _precedence,
                    Ignore = _ignore,
                    Factory = _factory
                };
            }
        }
    }
}