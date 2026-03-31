namespace DBCode.Syntax.Tokenizing {
   internal sealed class StringReader : ITokenReader {
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

         bool isVerbatim = false;

         char c = pText[index];

         if (c == '$') {
            if (index + 1 < length && (pText[index + 1] == '"' || pText[index + 1] == '@')) {
               index++;
            }
            else {
               return false;
            }
         }

         if (pText[index] == '@') {
            if (index + 1 < length && pText[index + 1] == '"') {
               isVerbatim = true;
               index++;
            }
            else {
               return false;
            }
         }

         if (pText[index] != '"')
            return false;

         int start = pStartIndex;
         index++;

         if (isVerbatim) {
            while (index < length) {
               char ch = pText[index];

               if (ch == '"') {
                  if (index + 1 < length && pText[index + 1] == '"') {
                     index += 2;
                     continue;
                  }
                  index++;
                  break;
               }

               index++;
            }
         }
         else {
            while (index < length) {
               char ch = pText[index];

               if (ch == '\\') {
                  if (index + 1 < length)
                     index += 2;
                  else
                     index++;
                  continue;
               }

               if (ch == '"') {
                  index++;
                  break;
               }

               index++;
            }
         }

         pToken = new Token(TokenKind.StringLiteral, start, index - start);
         pNewIndex = index;
         return true;
      }
   }
}
