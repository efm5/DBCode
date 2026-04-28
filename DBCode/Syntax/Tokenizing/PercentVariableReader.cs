namespace DBCode.Syntax.Tokenizing {
   internal sealed class PercentVariableReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         if (pText[index] != '%')
            return false;
         startPosition = index;
         index++;
         if (index >= length) {
            pToken = new Token(TokenKind.Identifier, startPosition, 1);
            pNewIndex = index;
            return true;
         }
         if (pText[index] == '%') {
            index++;
            if (index < length && (char.IsLetter(pText[index]) || pText[index] == '_')) {
               index++;
            }
            pToken = new Token(TokenKind.Identifier, startPosition, index - startPosition);
            pNewIndex = index;
            return true;
         }
         if (char.IsLetter(pText[index]) || pText[index] == '_') {
            while (index < length && pText[index] != '%' && pText[index] != '\n' && pText[index] != '\r') {
               index++;
            }
            if (index < length && pText[index] == '%') {
               index++;
            }
            pToken = new Token(TokenKind.Identifier, startPosition, index - startPosition);
            pNewIndex = index;
            return true;
         }
         return false;
      }
   }
}
