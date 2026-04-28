namespace DBCode.Syntax.Tokenizing {
   internal sealed class HtmlCommentReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index + 3 >= length)
            return false;
         if (pText[index] != '<' || pText[index + 1] != '!' ||
             pText[index + 2] != '-' || pText[index + 3] != '-')
            return false;
         startPosition = index;
         index += 4;
         while (index + 2 < length) {
            if (pText[index] == '-' && pText[index + 1] == '-' && pText[index + 2] == '>') {
               index += 3;
               pToken = new Token(TokenKind.Comment, startPosition, index - startPosition);
               pNewIndex = index;
               return true;
            }
            index++;
         }
         pToken = new Token(TokenKind.Comment, startPosition, length - startPosition);
         pNewIndex = length;
         return true;
      }
   }
}
