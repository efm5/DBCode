namespace DBCode.Syntax {
   internal sealed class CssLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.Css;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
