namespace DBCode.Syntax.Tokenizing {
   internal sealed class WhitespaceReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         char currentCharacter;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         currentCharacter = pText[index];
         if (!char.IsWhiteSpace(currentCharacter))
            return false;
         startPosition = index;
         index++;
         while (index < length && char.IsWhiteSpace(pText[index])) {
            index++;
         }
         pToken = new Token(TokenKind.Whitespace, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
