namespace DBCode.Syntax {
   internal sealed class PythonLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.Python;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
