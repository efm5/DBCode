namespace DBCode.Syntax.Tokenizing {
   internal sealed class StringReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         bool isVerbatim;
         char currentCharacter, nextCharacter;

         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         isVerbatim = false;
         currentCharacter = pText[index];
         if (currentCharacter == '$') {
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
         startPosition = pStartIndex;
         index++;
         if (isVerbatim) {
            while (index < length) {
               nextCharacter = pText[index];
               if (nextCharacter == '"') {
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
               nextCharacter = pText[index];
               if (nextCharacter == '\\') {
                  if (index + 1 < length)
                     index += 2;
                  else
                     index++;
                  continue;
               }
               if (nextCharacter == '"') {
                  index++;
                  break;
               }
               index++;
            }
         }
         pToken = new Token(TokenKind.StringLiteral, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }
   }
}
