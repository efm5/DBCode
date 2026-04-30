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
         if (currentCharacter == '0' && index < length) {
            nextCharacter = pText[index];
            if (nextCharacter == 'x' || nextCharacter == 'X') { // hex: 0xFF
               index++;
               while (index < length && IsHexDigit(pText[index]))
                  index++;
               ConsumeIntegerSuffix(pText, length, ref index);
               pToken = new Token(TokenKind.Number, startPosition, index - startPosition);
               pNewIndex = index;
               return true;
            }
            if (nextCharacter == 'b' || nextCharacter == 'B') { // binary: 0b1010
               index++;
               while (index < length && (pText[index] == '0' || pText[index] == '1' || pText[index] == '_'))
                  index++;
               ConsumeIntegerSuffix(pText, length, ref index);
               pToken = new Token(TokenKind.Number, startPosition, index - startPosition);
               pNewIndex = index;
               return true;
            }
            // Note: 0o octal is not valid C# — no octal branch.
         }
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
               if (signCharacter == '+' || signCharacter == '-')
                  exponentIndex++;
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

      private static void ConsumeIntegerSuffix(string pText, int pLength, ref int pIndex) {
         // Consumes optional C# integer suffixes: U, L, UL, LU (case-insensitive)
         if (pIndex >= pLength)
            return;
         char first = char.ToUpperInvariant(pText[pIndex]);
         if (first == 'U') {
            pIndex++;
            if (pIndex < pLength && char.ToUpperInvariant(pText[pIndex]) == 'L')
               pIndex++;
         }
         else if (first == 'L') {
            pIndex++;
            if (pIndex < pLength && char.ToUpperInvariant(pText[pIndex]) == 'U')
               pIndex++;
         }
      }

      private static bool IsDigit(char pChar) {
         return pChar >= '0' && pChar <= '9';
      }

      private static bool IsHexDigit(char pChar) {
         return (pChar >= '0' && pChar <= '9') ||
                (pChar >= 'a' && pChar <= 'f') ||
                (pChar >= 'A' && pChar <= 'F') ||
                pChar == '_'; // digit separators valid in hex literals
      }
   }
}
