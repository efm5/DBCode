namespace DBCode.Syntax {
   internal sealed class XmlLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [];

      public LanguageKind Language => LanguageKind.Xml;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
