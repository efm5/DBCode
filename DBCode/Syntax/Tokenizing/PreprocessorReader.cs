namespace DBCode.Syntax.Tokenizing {
   internal sealed class PreprocessorReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, scanIndex, startPosition;
         char currentCharacter;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         if (pText[index] != '#')
            return false;
         scanIndex = index - 1;
         while (scanIndex >= 0) {
            currentCharacter = pText[scanIndex];
            if (currentCharacter == '\n' || currentCharacter == '\r')
               break;
            if (!char.IsWhiteSpace(currentCharacter))
               return false;
            scanIndex--;
         }
         startPosition = index;
         index++;
         while (index < length && pText[index] != '\n' && pText[index] != '\r') {
            index++;
         }
         pToken = new Token(TokenKind.PreprocessorDirective, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
