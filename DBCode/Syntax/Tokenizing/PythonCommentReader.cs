namespace DBCode.Syntax.Tokenizing {
   internal sealed class PythonCommentReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         if (pText[index] != '#')
            return false;
         startPosition = index;
         index++;
         while (index < length && pText[index] != '\n' && pText[index] != '\r') {
            index++;
         }
         pToken = new Token(TokenKind.Comment, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
