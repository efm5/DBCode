namespace DBCode.Syntax {
   internal sealed class CLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "auto", "break", "case", "char", "const", "continue", "default",
         "do", "double", "else", "enum", "extern", "float", "for", "goto",
         "if", "inline", "int", "long", "register", "restrict", "return",
         "short", "signed", "sizeof", "static", "struct", "switch", "typedef",
         "union", "unsigned", "void", "volatile", "while"
      ];

      public LanguageKind Language => LanguageKind.C;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
