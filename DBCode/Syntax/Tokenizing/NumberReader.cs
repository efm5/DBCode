namespace DBCode.Syntax.Tokenizing {
   internal sealed class NumberReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition, dotIndex, exponentIndex;
         char currentCharacter, nextCharacter, signCharacter;

         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         currentCharacter = pText[index];
         if (!IsDigit(currentCharacter))
            return false;
         startPosition = index;
         index++;
         while (index < length) {
            nextCharacter = pText[index];
            if (IsDigit(nextCharacter) || nextCharacter == '_') {
               index++;
               continue;
            }
            break;
         }
         if (index < length && pText[index] == '.') {
            dotIndex = index + 1;
            if (dotIndex < length && IsDigit(pText[dotIndex])) {
               index += 2;
               while (index < length) {
                  nextCharacter = pText[index];
                  if (IsDigit(nextCharacter) || nextCharacter == '_') {
                     index++;
                     continue;
                  }
                  break;
               }
            }
         }
         if (index < length && (pText[index] == 'e' || pText[index] == 'E')) {
            exponentIndex = index + 1;
            if (exponentIndex < length) {
               signCharacter = pText[exponentIndex];
               if (signCharacter == '+' || signCharacter == '-') {
                  exponentIndex++;
               }
               if (exponentIndex < length && IsDigit(pText[exponentIndex])) {
                  index = exponentIndex + 1;
                  while (index < length) {
                     nextCharacter = pText[index];
                     if (IsDigit(nextCharacter) || nextCharacter == '_') {
                        index++;
                        continue;
                     }
                     break;
                  }
               }
            }
         }
         pToken = new Token(TokenKind.Number, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }

      private static bool IsDigit(char pChar) {
         return pChar >= '0' && pChar <= '9';
      }
   }
}
