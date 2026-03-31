namespace DBCode.Syntax.Tokenizing {
   internal sealed class PreprocessorReader : ITokenReader {
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

         if (pText[index] != '#')
            return false;

         int scan = index - 1;
         while (scan >= 0) {
            char ch = pText[scan];
            if (ch == '\n' || ch == '\r')
               break;
            if (!char.IsWhiteSpace(ch))
               return false;
            scan--;
         }

         int start = index;
         index++;

         while (index < length && pText[index] != '\n' && pText[index] != '\r') {
            index++;
         }

         pToken = new Token(TokenKind.PreprocessorDirective, start, index - start);
         pNewIndex = index;
         return true;
      }
   }
}
