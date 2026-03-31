namespace DBCode.Syntax.Tokenizing {
   internal sealed class NumberReader : ITokenReader {
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
         if (!IsDigit(c))
            return false;

         int start = index;
         index++;

         while (index < length) {
            char ch = pText[index];
            if (IsDigit(ch) || ch == '_') {
               index++;
               continue;
            }
            break;
         }

         if (index < length && pText[index] == '.') {
            int dotIndex = index + 1;
            if (dotIndex < length && IsDigit(pText[dotIndex])) {
               index += 2;

               while (index < length) {
                  char ch = pText[index];
                  if (IsDigit(ch) || ch == '_') {
                     index++;
                     continue;
                  }
                  break;
               }
            }
         }

         if (index < length && (pText[index] == 'e' || pText[index] == 'E')) {
            int expIndex = index + 1;
            if (expIndex < length) {
               char sign = pText[expIndex];
               if (sign == '+' || sign == '-') {
                  expIndex++;
               }

               if (expIndex < length && IsDigit(pText[expIndex])) {
                  index = expIndex + 1;

                  while (index < length) {
                     char ch = pText[index];
                     if (IsDigit(ch) || ch == '_') {
                        index++;
                        continue;
                     }
                     break;
                  }
               }
            }
         }

         pToken = new Token(TokenKind.Number, start, index - start);
         pNewIndex = index;
         return true;
      }

      private static bool IsDigit(char pChar) {
         return pChar >= '0' && pChar <= '9';
      }
   }
}
