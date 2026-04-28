namespace DBCode.Syntax.Tokenizing {
   internal sealed class XmlProcessingInstructionReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index + 1 >= length)
            return false;
         if (pText[index] != '<' || pText[index + 1] != '?')
            return false;
         startPosition = index;
         index += 2;
         while (index + 1 < length) {
            if (pText[index] == '?' && pText[index + 1] == '>') {
               index += 2;
               pToken = new Token(TokenKind.PreprocessorDirective, startPosition, index - startPosition);
               pNewIndex = index;
               return true;
            }
            index++;
         }
         pToken = new Token(TokenKind.PreprocessorDirective, startPosition, length - startPosition);
         pNewIndex = length;
         return true;
      }
   }
}
