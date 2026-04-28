namespace DBCode.Syntax.Tokenizing {
   internal sealed class KeywordReader : ITokenReader {
      private readonly ILanguageDefinition mDefinition;
      private readonly IdentifierReader mIdentifierReader;

      internal KeywordReader(ILanguageDefinition pDefinition) {
         mDefinition = pDefinition;
         mIdentifierReader = new IdentifierReader();
      }

      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int newIndex, startPosition, length;
         Token identifierToken;
         string value;
         TokenKind kind;

         pToken = null!;
         pNewIndex = pStartIndex;
         if (!mIdentifierReader.TryRead(pText, pStartIndex, out identifierToken, out newIndex))
            return false;
         startPosition = identifierToken.StartIndex;
         length = identifierToken.Length;
         value = pText.Substring(startPosition, length);
         kind = mDefinition.Keywords.Contains(value, mDefinition.KeywordComparer)
            ? TokenKind.Keyword
            : TokenKind.Identifier;
         pToken = new Token(kind, startPosition, length);
         pNewIndex = newIndex;
         return true;
      }
   }
}
