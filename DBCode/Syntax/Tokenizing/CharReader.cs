namespace DBCode.Syntax.Tokenizing {
   internal sealed class CharReader : ITokenReader {
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

         if (pText[index] != '\'')
            return false;

         int start = index;
         index++;

         if (index >= length)
            return false;

         char c = pText[index];

         if (c == '\\') {
            index++;

            if (index >= length)
               return false;

            char esc = pText[index];

            if (esc == 'u') {
               index++;
               int count = 0;
               while (index < length && count < 4 && IsHexDigit(pText[index])) {
                  index++;
                  count++;
               }
               if (count != 4)
                  return false;
            }
            else if (esc == 'x') {
               index++;
               int count = 0;
               while (index < length && count < 4 && IsHexDigit(pText[index])) {
                  index++;
                  count++;
               }
               if (count == 0)
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

         pToken = new Token(TokenKind.CharLiteral, start, index - start);
         pNewIndex = index;
         return true;
      }

      private static bool IsHexDigit(char pChar) {
         if (pChar >= '0' && pChar <= '9')
            return true;
         if (pChar >= 'a' && pChar <= 'f')
            return true;
         if (pChar >= 'A' && pChar <= 'F')
            return true;
         return false;
      }
   }
}