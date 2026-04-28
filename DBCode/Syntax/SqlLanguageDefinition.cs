namespace DBCode.Syntax {
   internal sealed class SqlLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.Sql;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.OrdinalIgnoreCase;
   }
}
