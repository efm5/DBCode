namespace DBCode.Syntax {
   internal static class LanguageRegistry {
      private static readonly Dictionary<LanguageKind, ILanguageDefinition> sDefinitions = [];
      private static readonly Dictionary<LanguageKind, ITokenizer> sTokenizers = [];
      private static readonly Dictionary<LanguageKind, IHighlighter> sHighlighters = [];

      static LanguageRegistry() {
         ILanguageDefinition csharpDefinition = new CSharpLanguageDefinition();
         sDefinitions[LanguageKind.CSharp] = csharpDefinition;
         ITokenizer csharpTokenizer = new CSharpTokenizer(csharpDefinition);
         sTokenizers[LanguageKind.CSharp] = csharpTokenizer;
         IHighlighter csharpHighlighter = new CSharpHighlighter();
         sHighlighters[LanguageKind.CSharp] = csharpHighlighter;
      }

      internal static ILanguageDefinition GetDefinition(LanguageKind pLanguage) {
         return sDefinitions[pLanguage];
      }

      internal static ITokenizer GetTokenizer(LanguageKind pLanguage) {
         return sTokenizers[pLanguage];
      }

      internal static IHighlighter GetHighlighter(LanguageKind pLanguage) {
         return sHighlighters[pLanguage];
      }
   }
}
