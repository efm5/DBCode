namespace DBCode.Syntax.Tokenizing {
   internal sealed class PythonStringReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         char quote;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         if (pText[index] != '"' && pText[index] != '\'')
            return false;
         startPosition = index;
         quote = pText[index];
         if (index + 2 < length && pText[index + 1] == quote && pText[index + 2] == quote) {
            index += 3;
            while (index + 2 < length) {
               if (pText[index] == quote && pText[index + 1] == quote && pText[index + 2] == quote) {
                  index += 3;
                  pToken = new Token(TokenKind.StringLiteral, startPosition, index - startPosition);
                  pNewIndex = index;
                  return true;
               }
               index++;
            }
            pToken = new Token(TokenKind.StringLiteral, startPosition, length - startPosition);
            pNewIndex = length;
            return true;
         }
         index++;
         while (index < length) {
            if (pText[index] == '\\') {
               if (index + 1 < length)
                  index += 2;
               else
                  index++;
               continue;
            }
            if (pText[index] == quote) {
               index++;
               pToken = new Token(TokenKind.StringLiteral, startPosition, index - startPosition);
               pNewIndex = index;
               return true;
            }
            if (pText[index] == '\n' || pText[index] == '\r')
               break;
            index++;
         }
         pToken = new Token(TokenKind.StringLiteral, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
