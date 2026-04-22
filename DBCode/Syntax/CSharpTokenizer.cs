namespace DBCode.Syntax {
   internal sealed class CSharpTokenizer : ITokenizer {
      private readonly ILanguageDefinition mDefinition;

      internal CSharpTokenizer(ILanguageDefinition pDefinition) {
         mDefinition = pDefinition;
      }

      public LanguageKind Language => LanguageKind.CSharp;

      public IReadOnlyList<Token> Tokenize(string pText) {
         int index, startPosition, length;
         char currentCharacter;
         string value;
         TokenKind kind;
         var tokens = new List<Token>();
         index = 0;
         while (index < pText.Length) {
            currentCharacter = pText[index];
            if (char.IsWhiteSpace(currentCharacter)) {
               startPosition = index;
               while (index < pText.Length && char.IsWhiteSpace(pText[index])) {
                  index++;
               }
               tokens.Add(new Token(TokenKind.Whitespace, startPosition, index - startPosition));
               continue;
            }
            if (IsIdentifierStart(currentCharacter)) {
               startPosition = index;
               index++;
               while (index < pText.Length && IsIdentifierPart(pText[index])) {
                  index++;
               }
               length = index - startPosition;
               value = pText.Substring(startPosition, length);
               kind = mDefinition.Keywords.Contains(value)
                  ? TokenKind.Keyword
                  : TokenKind.Identifier;
               tokens.Add(new Token(kind, startPosition, length));
               continue;
            }
            tokens.Add(new Token(TokenKind.Unknown, index, 1));
            index++;
         }
         return tokens;
      }

      private static bool IsIdentifierStart(char pChar) {
         return char.IsLetter(pChar) || pChar == '_' ||
            char.GetUnicodeCategory(pChar) == System.Globalization.UnicodeCategory.LetterNumber;
      }

      private static bool IsIdentifierPart(char pChar) {
         if (IsIdentifierStart(pChar)) {
            return true;
         }

         return char.IsDigit(pChar);
      }
   }
}
