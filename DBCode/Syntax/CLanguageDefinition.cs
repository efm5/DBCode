namespace DBCode.Syntax {
   internal sealed class CLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.C;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
