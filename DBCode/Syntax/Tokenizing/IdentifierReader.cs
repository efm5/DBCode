namespace DBCode.Syntax.Tokenizing {
   internal sealed class IdentifierReader : ITokenReader {
      public bool TryRead(string pText, int pStartIndex, out Token pToken, out int pNewIndex) {
         int length, index, startPosition;
         char currentCharacter, nextCharacter;

         pToken = null!;
         pNewIndex = pStartIndex;
         length = pText.Length;
         index = pStartIndex;
         if (index >= length)
            return false;
         currentCharacter = pText[index];
         if (currentCharacter == '@') {
            if (index + 1 >= length)
               return false;
            nextCharacter = pText[index + 1];
            if (!IsIdentifierStart(nextCharacter))
               return false;
            index++;
         }
         else {
            if (!IsIdentifierStart(currentCharacter))
               return false;
         }
         startPosition = pStartIndex;
         index++;
         while (index < length && IsIdentifierPart(pText[index])) {
            index++;
         }
         pToken = new Token(TokenKind.Identifier, startPosition, index - startPosition);
         pNewIndex = index;
         return true;
      }

      private static bool IsIdentifierStart(char pChar) {
         if (char.IsLetter(pChar))
            return true;
         if (pChar == '_')
            return true;
         return char.GetUnicodeCategory(pChar) == System.Globalization.UnicodeCategory.LetterNumber;
      }

      private static bool IsIdentifierPart(char pChar) {
         if (IsIdentifierStart(pChar))
            return true;
         return char.IsDigit(pChar);
      }
   }
}
