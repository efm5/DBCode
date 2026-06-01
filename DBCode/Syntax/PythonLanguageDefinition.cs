namespace DBCode.Syntax {
   internal sealed class PythonLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "False", "None", "True", "and", "as", "assert", "async", "await",
         "break", "class", "continue", "def", "del", "elif", "else", "except",
         "finally", "for", "from", "global", "if", "import", "in", "is",
         "lambda", "nonlocal", "not", "or", "pass", "raise", "return",
         "try", "while", "with", "yield"
      ];

      public LanguageKind Language => LanguageKind.Python;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
