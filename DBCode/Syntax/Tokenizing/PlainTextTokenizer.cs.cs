namespace DBCode.Syntax.Tokenizing {
   internal sealed class PlainTextTokenizer : ITokenizer {
      public LanguageKind Language => LanguageKind.PlainText;

      public IReadOnlyList<Token> Tokenize(string pText) {
         if (pText.Length == 0)
            return [];
         return [new Token(TokenKind.Whitespace, 0, pText.Length)];
      }
   }
}
