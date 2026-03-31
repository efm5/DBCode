namespace DBCode.Syntax.Tokenizing {
   internal sealed class IdentifierReader : ITokenReader {
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

         char c = pText[index];

         if (c == '@') {
            if (index + 1 >= length)
               return false;
            char next = pText[index + 1];
            if (!IsIdentifierStart(next))
               return false;
            index++;
         }
         else {
            if (!IsIdentifierStart(c))
               return false;
         }

         int start = pStartIndex;
         index++;

         while (index < length && IsIdentifierPart(pText[index])) {
            index++;
         }

         pToken = new Token(TokenKind.Identifier, start, index - start);
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
