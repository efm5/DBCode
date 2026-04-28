namespace DBCode.Syntax.Tokenizing {
   internal sealed class DollarVariableReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         if (pText[index] != '$')
            return false;
         if (index + 1 >= length)
            return false;
         if (!char.IsLetter(pText[index + 1]) && pText[index + 1] != '_')
            return false;
         startPosition = index;
         index++;
         while (index < length && (char.IsLetterOrDigit(pText[index]) || pText[index] == '_')) {
            index++;
         }
         pToken = new Token(TokenKind.Identifier, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
