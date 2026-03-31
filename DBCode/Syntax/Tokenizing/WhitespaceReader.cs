namespace DBCode.Syntax.Tokenizing {
   internal sealed class WhitespaceReader : ITokenReader {
      public bool TryRead(
         string pText,
         int pStartIndex,
         out Token pToken,
         out int pNewIndex
      ) {
         pToken = null!;
         pNewIndex = pStartIndex;

         int length = pText.Length;
         int index = pStartIndex;

         if (index >= length)
            return false;

         char c = pText[index];
         if (!char.IsWhiteSpace(c))
            return false;

         int start = index;
         index++;

         while (index < length && char.IsWhiteSpace(pText[index])) {
            index++;
         }

         pToken = new Token(TokenKind.Whitespace, start, index - start);
         pNewIndex = index;
         return true;
      }
   }
}
