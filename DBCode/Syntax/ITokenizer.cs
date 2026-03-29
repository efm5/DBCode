namespace DBCode.Syntax {
   internal interface ITokenizer {
      LanguageKind Language { get; }

      IReadOnlyList<Token> Tokenize(string pText);
   }
}
