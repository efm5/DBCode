namespace DBCode.Syntax {
   internal sealed class BatchLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.Batch;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.OrdinalIgnoreCase;
   }
}
