namespace DBCode.Syntax {
   internal sealed class PowerShellLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.PowerShell;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.OrdinalIgnoreCase;
   }
}
