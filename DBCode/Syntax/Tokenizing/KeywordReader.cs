namespace DBCode.Syntax.Tokenizing {
   internal sealed class KeywordReader : ITokenReader {
      private readonly ILanguageDefinition mDefinition;
      private readonly IdentifierReader mIdentifierReader;

      internal KeywordReader(ILanguageDefinition pDefinition) {
         mDefinition = pDefinition;
         mIdentifierReader = new IdentifierReader();
      }

      public bool TryRead(
         string pText,
         int pStartIndex,
         out Token pToken,
         out int pNewIndex
      ) {
         pToken = null!;
         pNewIndex = pStartIndex;

         Token identifierToken;
         int newIndex;

         if (!mIdentifierReader.TryRead(pText, pStartIndex, out identifierToken, out newIndex))
            return false;

         int start = identifierToken.StartIndex;
         int length = identifierToken.Length;
         string value = pText.Substring(start, length);

         TokenKind kind = mDefinition.Keywords.Contains(value)
            ? TokenKind.Keyword
            : TokenKind.Identifier;

         pToken = new Token(kind, start, length);
         pNewIndex = newIndex;
         return true;
      }
   }
}
