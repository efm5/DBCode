namespace DBCode.Syntax.Tokenizing {
   internal sealed class OperatorReader : ITokenReader {
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

         if (IsPunctuation(c)) {
            pToken = new Token(TokenKind.Punctuation, index, 1);
            pNewIndex = index + 1;
            return true;
         }

         if (!IsOperatorStart(c))
            return false;

         int start = index;
         index++;

         if (index < length) {
            char next = pText[index];

            if (IsTwoCharOperator(c, next)) {
               index++;

               if (index < length) {
                  char third = pText[index];
                  if (IsThreeCharOperator(c, next, third)) {
                     index++;
                  }
               }
            }
         }

         pToken = new Token(TokenKind.Operator, start, index - start);
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