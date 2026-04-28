namespace DBCode.Syntax {
   internal sealed class FSharpLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.FSharp;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
