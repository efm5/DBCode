namespace DBCode.Syntax.Tokenizing {
   internal sealed class CharReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition, hexDigitCount;
         char currentCharacter, escapeCharacter;

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
         if (index >= length)
            return false;
         currentCharacter = pText[index];
         if (currentCharacter == '\\') {
            index++;
            if (index >= length)
               return false;
            escapeCharacter = pText[index];
            if (escapeCharacter == 'u') {
               index++;
               hexDigitCount = 0;
               while (index < length && hexDigitCount < 4 && IsHexDigit(pText[index])) {
                  index++;
                  hexDigitCount++;
               }
               if (hexDigitCount != 4)
                  return false;
            }
            else if (escapeCharacter == 'x') {
               index++;
               hexDigitCount = 0;
               while (index < length && hexDigitCount < 4 && IsHexDigit(pText[index])) {
                  index++;
                  hexDigitCount++;
               }
               if (hexDigitCount == 0)
                  return false;
            }
            else {
               index++;
            }
         }
         else {
            index++;
         }
         if (index >= length)
            return false;
         if (pText[index] != '\'')
            return false;
         index++;
         pToken = new Token(TokenKind.CharLiteral, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }

      private static bool IsHexDigit(char pCharacter) {
         if (pCharacter >= '0' && pCharacter <= '9')
            return true;
         if (pCharacter >= 'a' && pCharacter <= 'f')
            return true;
         if (pCharacter >= 'A' && pCharacter <= 'F')
            return true;
         return false;
      }
   }
}
