namespace DBCode.Syntax.Tokenizing {
   internal sealed class BatchCommentReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index + 1 >= length)
            return false;
         if (pText[index] != ':' || pText[index + 1] != ':')
            return false;
         startPosition = index;
         index += 2;
         while (index < length && pText[index] != '\n' && pText[index] != '\r') {
            index++;
         }
         pToken = new Token(TokenKind.Comment, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
