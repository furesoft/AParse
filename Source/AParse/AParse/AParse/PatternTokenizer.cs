using System;
using System.Collections.Generic;
using System.Linq;

namespace AParse
{
    public class PatternTokenizer<TToken> : ITokenizer<TToken>
    {
        private List<TokenDefinition<TToken>> _tokenDefinitions = new List<TokenDefinition<TToken>>();

        public PatternTokenizer()
        {
           
        }

        public void AddDefinition(TokenDefinition<TToken> def)
        {
            _tokenDefinitions.Add(def);
        }

        public IEnumerable<Token<TToken>> Tokenize(string lqlText)
        {
            var tokenMatches = FindTokenMatches(lqlText);
            tokenMatches.RemoveAll((obj) => obj.Ignore == true);

            var groupedByIndex = tokenMatches.GroupBy(x => x.StartIndex)
                .OrderBy(x => x.Key)
                .ToList();

            TokenMatch<TToken> lastMatch = null;
            for (int i = 0; i < groupedByIndex.Count; i++)
            {
                var bestMatch = groupedByIndex[i].OrderBy(x => x.Precedence).First();
                if (lastMatch != null && bestMatch.StartIndex < lastMatch.EndIndex)
                    continue;

                yield return new Token<TToken>(bestMatch.TokenType, bestMatch.Value) { Factory = bestMatch.Factory };

                lastMatch = bestMatch;
            }

            yield return new Token<TToken>(default(TToken));
        }

        private List<TokenMatch<TToken>> FindTokenMatches(string lqlText)
        {
            var tokenMatches = new List<TokenMatch<TToken>>();

            foreach (var tokenDefinition in _tokenDefinitions)
                tokenMatches.AddRange(tokenDefinition.FindMatches(lqlText).ToList());

            return tokenMatches;
        }
    }
}