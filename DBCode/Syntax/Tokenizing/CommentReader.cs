namespace DBCode.Syntax.Tokenizing {
   internal sealed class CommentReader : ITokenReader {
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

         if (index + 1 >= length)
            return false;

         char c1 = pText[index];
         char c2 = pText[index + 1];

         if (c1 == '/' && c2 == '/') {
            int start = index;
            index += 2;

            while (index < length && pText[index] != '\n' && pText[index] != '\r') {
               index++;
            }

            pToken = new Token(TokenKind.Comment, start, index - start);
            pNewIndex = index;
            return true;
         }

         if (c1 == '/' && c2 == '*') {
            int start = index;
            index += 2;

            while (index + 1 < length) {
               if (pText[index] == '*' && pText[index + 1] == '/') {
                  index += 2;
                  pToken = new Token(TokenKind.Comment, start, index - start);
                  pNewIndex = index;
                  return true;
               }
               index++;
            }

            pToken = new Token(TokenKind.Comment, start, length - start);
            pNewIndex = length;
            return true;
         }

         return false;
      }
   }
}
