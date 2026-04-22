namespace DBCode.Syntax.Tokenizing {
   internal sealed class OperatorReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         char currentCharacter, nextCharacter, thirdCharacter;

         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         currentCharacter = pText[index];
         if (IsPunctuation(currentCharacter)) {
            pToken = new Token(TokenKind.Punctuation, index, 1);
            pNewIndex = index + 1;
            return true;
         }
         if (!IsOperatorStart(currentCharacter))
            return false;
         startPosition = index;
         index++;
         if (index < length) {
            nextCharacter = pText[index];
            if (IsTwoCharOperator(currentCharacter, nextCharacter)) {
               index++;
               if (index < length) {
                  thirdCharacter = pText[index];
                  if (IsThreeCharOperator(currentCharacter, nextCharacter, thirdCharacter)) {
                     index++;
                  }
               }
            }
         }
         pToken = new Token(TokenKind.Operator, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }

      private static bool IsPunctuation(char pChar) {
         return pChar == '(' ||
                pChar == ')' ||
                pChar == '{' ||
                pChar == '}' ||
                pChar == '[' ||
                pChar == ']' ||
                pChar == ',' ||
                pChar == '.' ||
                pChar == ';';
      }

      private static bool IsOperatorStart(char pChar) {
         return pChar == '+' ||
                pChar == '-' ||
                pChar == '*' ||
                pChar == '/' ||
                pChar == '%' ||
                pChar == '&' ||
                pChar == '|' ||
                pChar == '^' ||
                pChar == '!' ||
                pChar == '~' ||
                pChar == '<' ||
                pChar == '>' ||
                pChar == '=' ||
                pChar == '?' ||
                pChar == ':';
      }

      private static bool IsTwoCharOperator(char pFirst, char pSecond) {
         if (pFirst == '=' && pSecond == '=')
            return true;
         if (pFirst == '!' && pSecond == '=')
            return true;
         if (pFirst == '<' && pSecond == '=')
            return true;
         if (pFirst == '>' && pSecond == '=')
            return true;
         if (pFirst == '&' && pSecond == '&')
            return true;
         if (pFirst == '|' && pSecond == '|')
            return true;
         if (pFirst == '+' && pSecond == '+')
            return true;
         if (pFirst == '-' && pSecond == '-')
            return true;
         if (pFirst == '+' && pSecond == '=')
            return true;
         if (pFirst == '-' && pSecond == '=')
            return true;
         if (pFirst == '*' && pSecond == '=')
            return true;
         if (pFirst == '/' && pSecond == '=')
            return true;
         if (pFirst == '%' && pSecond == '=')
            return true;
         if (pFirst == '&' && pSecond == '=')
            return true;
         if (pFirst == '|' && pSecond == '=')
            return true;
         if (pFirst == '^' && pSecond == '=')
            return true;
         if (pFirst == '<' && pSecond == '<')
            return true;
         if (pFirst == '>' && pSecond == '>')
            return true;
         if (pFirst == '?' && pSecond == '?')
            return true;
         if (pFirst == '=' && pSecond == '>')
            return true;

         return false;
      }

      private static bool IsThreeCharOperator(char pFirst, char pSecond, char pThird) {
         if (pFirst == '<' && pSecond == '<' && pThird == '=')
            return true;
         if (pFirst == '>' && pSecond == '>' && pThird == '=')
            return true;
         if (pFirst == '?' && pSecond == '?' && pThird == '=')
            return true;

         return false;
      }
   }
}
