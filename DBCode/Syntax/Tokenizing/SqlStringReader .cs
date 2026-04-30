namespace DBCode.Syntax.Tokenizing {
   internal sealed class SqlStringReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         char nextCharacter;

         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         if (pText[index] != '\'')
            return false;
         startPosition = index;
         index++;
         while (index < length) {
            nextCharacter = pText[index];
            if (nextCharacter == '\'') {
               index++;
               if (index < length && pText[index] == '\'') { // escaped quote ''
                  index++;
                  continue;
               }
               break;
            }
            index++;
         }
         pToken = new Token(TokenKind.StringLiteral, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
