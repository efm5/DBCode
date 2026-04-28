namespace DBCode.Syntax {
   internal interface ILanguageDefinition {
      LanguageKind Language { get; }
      IReadOnlyCollection<string> Keywords { get; }
      StringComparer KeywordComparer { get; }
   }
}
