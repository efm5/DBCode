namespace DBCode.Syntax {
   internal sealed class JsonLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = ["false", "null", "true"];

      public LanguageKind Language => LanguageKind.Json;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
