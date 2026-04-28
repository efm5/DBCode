namespace DBCode.Syntax {
   internal static class LanguageRegistry {
      private static readonly Dictionary<LanguageKind, ILanguageDefinition> sDefinitions = [];
      private static readonly Dictionary<LanguageKind, ITokenizer> sTokenizers = [];
      private static readonly Dictionary<LanguageKind, IHighlighter> sHighlighters = [];

      static LanguageRegistry() {
         Register(new BasicLanguageDefinition(), pLanguageDefinition => new Tokenizing.BasicTokenizer(pLanguageDefinition), new BasicHighlighter());
         Register(new BatchLanguageDefinition(), pLanguageDefinition => new Tokenizing.BatchTokenizer(pLanguageDefinition), new BatchHighlighter());
         Register(new CLanguageDefinition(), pLanguageDefinition => new Tokenizing.CTokenizer(pLanguageDefinition), new CHighlighter());
         Register(new CppLanguageDefinition(), pLanguageDefinition => new Tokenizing.CppTokenizer(pLanguageDefinition), new CppHighlighter());
         Register(new CSharpLanguageDefinition(), pLanguageDefinition => new Tokenizing.CSharpTokenizer(pLanguageDefinition), new CSharpHighlighter());
         Register(new CssLanguageDefinition(), pLanguageDefinition => new Tokenizing.CssTokenizer(pLanguageDefinition), new CssHighlighter());
         Register(new FSharpLanguageDefinition(), pLanguageDefinition => new Tokenizing.FSharpTokenizer(pLanguageDefinition), new FSharpHighlighter());
         Register(new HtmlLanguageDefinition(), pLanguageDefinition => new Tokenizing.HtmlTokenizer(pLanguageDefinition), new HtmlHighlighter());
         Register(new JsonLanguageDefinition(), pLanguageDefinition => new Tokenizing.JsonTokenizer(pLanguageDefinition), new JsonHighlighter());
         Register(new MarkdownLanguageDefinition(), pLanguageDefinition => new Tokenizing.MarkdownTokenizer(), new MarkdownHighlighter());
         Register(new PlainTextLanguageDefinition(), pLanguageDefinition => new Tokenizing.PlainTextTokenizer(), null);
         Register(new PowerShellLanguageDefinition(), pLanguageDefinition => new Tokenizing.PowerShellTokenizer(pLanguageDefinition), new PowerShellHighlighter());
         Register(new PythonLanguageDefinition(), pLanguageDefinition => new Tokenizing.PythonTokenizer(pLanguageDefinition), new PythonHighlighter());
         Register(new SqlLanguageDefinition(), pLanguageDefinition => new Tokenizing.SqlTokenizer(pLanguageDefinition), new SqlHighlighter());
         Register(new XmlLanguageDefinition(), pLanguageDefinition => new Tokenizing.XmlTokenizer(pLanguageDefinition), new XmlHighlighter());
      }

      private static void Register(ILanguageDefinition pDefinition,
         Func<ILanguageDefinition, ITokenizer> pTokenizerFactory,
         IHighlighter? pHighlighter) {
         LanguageKind language = pDefinition.Language;
         sDefinitions[language] = pDefinition;
         sTokenizers[language] = pTokenizerFactory(pDefinition);
         if (pHighlighter != null)
            sHighlighters[language] = pHighlighter;
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
