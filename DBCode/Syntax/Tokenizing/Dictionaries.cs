namespace DBCode.Syntax {
   internal class Dictionaries {
      private static readonly Dictionary<TokenKind, ColorRole> sCSharpColors = new()
{
   { TokenKind.CharLiteral,            ColorRole.CharLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sCColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.CharLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sCppColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.CharLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sBasicColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sFSharpColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.Identifier},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Keyword},
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sHtmlColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Identifier},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sCssColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Identifier},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sXmlColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Identifier},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sJsonColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Identifier},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sPowerShellColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Keyword},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sBatchColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sSqlColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sMarkdownColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Identifier},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Keyword},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};
      private static readonly Dictionary<TokenKind, ColorRole> sPythonColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral},
   { TokenKind.Comment,                ColorRole.Comment},
   { TokenKind.Identifier,             ColorRole.Identifier},
   { TokenKind.Keyword,                ColorRole.Keyword},
   { TokenKind.Number,                 ColorRole.Number},
   { TokenKind.Operator,               ColorRole.Operator},
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier},
   { TokenKind.Punctuation,            ColorRole.Punctuation},
   { TokenKind.StringLiteral,          ColorRole.StringLiteral},
   { TokenKind.Unknown,                ColorRole.Unknown},
   { TokenKind.Whitespace,             ColorRole.Whitespace}
};

   }

   internal static class ThemeColorProvider {
      private static readonly Dictionary<ColorRole, Color> sRoleColors = new()
      {
      { ColorRole.CharLiteral,            Color.Empty },
      { ColorRole.Comment,                Color.Empty },
      { ColorRole.Identifier,             Color.Empty },
      { ColorRole.Keyword,                Color.Empty },
      { ColorRole.Number,                 Color.Empty },
      { ColorRole.Operator,               Color.Empty },
      { ColorRole.PreprocessorDirective,  Color.Empty },
      { ColorRole.Punctuation,            Color.Empty },
      { ColorRole.StringLiteral,          Color.Empty },
      { ColorRole.Unknown,                Color.Empty },
      { ColorRole.Whitespace,             Color.Empty }
   };

      internal static Color Get(ColorRole role)
         => sRoleColors.TryGetValue(role, out var color) ? color : Color.Empty;

      internal static void Set(ColorRole role, Color color)
         => sRoleColors[role] = color;
   }

   internal static class LanguageColorRegistry {
      private static readonly Dictionary<TokenKind, ColorRole> sBatchColors = new()
{
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sBasicColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sCColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.CharLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sCppColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.CharLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sCSharpColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.CharLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sCssColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Identifier },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sFSharpColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.Identifier },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Keyword },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sHtmlColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Identifier },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sJsonColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Identifier },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sMarkdownColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Identifier },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Keyword },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sPowerShellColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Keyword },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sPythonColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sSqlColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Keyword },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.Identifier },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};

      private static readonly Dictionary<TokenKind, ColorRole> sXmlColors = new()
      {
   { TokenKind.CharLiteral,            ColorRole.StringLiteral },
   { TokenKind.Comment,                ColorRole.Comment },
   { TokenKind.Identifier,             ColorRole.Identifier },
   { TokenKind.Keyword,                ColorRole.Identifier },
   { TokenKind.Number,                 ColorRole.Number },
   { TokenKind.Operator,               ColorRole.Operator },
   { TokenKind.PreprocessorDirective,  ColorRole.PreprocessorDirective },
   { TokenKind.Punctuation,            ColorRole.Punctuation },
   { TokenKind.StringLiteral,          ColorRole.StringLiteral },
   { TokenKind.Unknown,                ColorRole.Unknown },
   { TokenKind.Whitespace,             ColorRole.Whitespace }
};
      private static readonly Dictionary<LanguageKind, Dictionary<TokenKind, ColorRole>> sRegistry = new()
      {
      { LanguageKind.Batch,      sBatchColors },
      { LanguageKind.Basic,      sBasicColors },
      { LanguageKind.C,          sCColors },
      { LanguageKind.Cpp,        sCppColors },
      { LanguageKind.CSharp,     sCSharpColors },
      { LanguageKind.Css,        sCssColors },
      { LanguageKind.FSharp,     sFSharpColors },
      { LanguageKind.Html,       sHtmlColors },
      { LanguageKind.Json,       sJsonColors },
      { LanguageKind.Markdown,   sMarkdownColors },
      { LanguageKind.PowerShell, sPowerShellColors },
      { LanguageKind.Python,     sPythonColors },
      { LanguageKind.Sql,        sSqlColors },
      { LanguageKind.Xml,        sXmlColors }
   };

      internal static Dictionary<TokenKind, ColorRole> Get(LanguageKind language)
         => sRegistry.TryGetValue(language, out var map) ? map : sRegistry[LanguageKind.CSharp];
   }

   internal abstract class SyntaxHighlighterBase : IHighlighter {
      protected readonly Dictionary<TokenKind, ColorRole> mColorRoles;
      protected readonly LanguageKind mLanguage;

      protected SyntaxHighlighterBase(LanguageKind language) {
         mLanguage = language;
         mColorRoles = LanguageColorRegistry.Get(language);
      }

      public LanguageKind Language => mLanguage;

      public void ApplyHighlighting(RichTextBox box, IReadOnlyList<Token> tokens) {
         if (tokens == null || tokens.Count == 0)
            return;

         var originalSelectionStart = box.SelectionStart;
         var originalSelectionLength = box.SelectionLength;

         box.SuspendLayout();

         foreach (var token in tokens) {
            if (!mColorRoles.TryGetValue(token.Kind, out var role))
               role = ColorRole.Unknown;

            var color = ThemeColorProvider.Get(role);

            box.SelectionStart = token.StartIndex;
            box.SelectionLength = token.Length;
            box.SelectionColor = color;
         }

         box.SelectionStart = originalSelectionStart;
         box.SelectionLength = originalSelectionLength;

         box.ResumeLayout();
      }
   }

   internal static class HighlighterFactory {
      private static readonly Dictionary<LanguageKind, IHighlighter> sHighlighters = new()
      {
         { LanguageKind.Batch,      new BatchHighlighter() },
         { LanguageKind.Basic,      new BasicHighlighter() },
         { LanguageKind.C,          new CHighlighter() },
         { LanguageKind.Cpp,        new CppHighlighter() },
         { LanguageKind.CSharp,     new CSharpHighlighter() },
         { LanguageKind.Css,        new CssHighlighter() },
         { LanguageKind.FSharp,     new FSharpHighlighter() },
         { LanguageKind.Html,       new HtmlHighlighter() },
         { LanguageKind.Json,       new JsonHighlighter() },
         { LanguageKind.Markdown,   new MarkdownHighlighter() },
         { LanguageKind.PowerShell, new PowerShellHighlighter() },
         { LanguageKind.Python,     new PythonHighlighter() },
         { LanguageKind.Sql,        new SqlHighlighter() },
         { LanguageKind.Xml,        new XmlHighlighter() }
      };

      private static readonly Dictionary<string, LanguageKind> sExtensionMap = new(StringComparer.OrdinalIgnoreCase)
      {
         { ".bat",  LanguageKind.Batch },
         { ".cmd",  LanguageKind.Batch },
         { ".bas",  LanguageKind.Basic },
         { ".vb",   LanguageKind.Basic },
         { ".c",    LanguageKind.C },
         { ".h",    LanguageKind.C },
         { ".cpp",  LanguageKind.Cpp },
         { ".hpp",  LanguageKind.Cpp },
         { ".cs",   LanguageKind.CSharp },
         { ".css",  LanguageKind.Css },
         { ".fs",   LanguageKind.FSharp },
         { ".html", LanguageKind.Html },
         { ".htm",  LanguageKind.Html },
         { ".json", LanguageKind.Json },
         { ".md",   LanguageKind.Markdown },
         { ".ps1",  LanguageKind.PowerShell },
         { ".py",   LanguageKind.Python },
         { ".sql",  LanguageKind.Sql },
         { ".xml",  LanguageKind.Xml }
      };

      internal static IHighlighter GetHighlighter(string extension) {
         if (!sExtensionMap.TryGetValue(extension, out var language))
            language = LanguageKind.CSharp;

         return sHighlighters[language];
      }

      internal static IHighlighter GetHighlighter(LanguageKind language)
         => sHighlighters.TryGetValue(language, out var highlighter)
               ? highlighter
               : sHighlighters[LanguageKind.CSharp];
   }
   namespace DBCode.Syntax {
      internal sealed class SyntaxHighlighterEngine {
         private IHighlighter mHighlighter;
         private ITokenizer mTokenizer;
         private readonly RichTextBox mBox;

         private bool mIsHighlighting;

         internal SyntaxHighlighterEngine(RichTextBox box, LanguageKind language) {
            mBox = box;
            mHighlighter = HighlighterFactory.GetHighlighter(language);
            mTokenizer = LanguageRegistry.GetTokenizer(language);
         }

         internal void SetLanguage(LanguageKind language) {
            mHighlighter = HighlighterFactory.GetHighlighter(language);
            mTokenizer = LanguageRegistry.GetTokenizer(language);
         }

         internal void Highlight() {
            if (mIsHighlighting)
               return;

            mIsHighlighting = true;

            try {
               var text = mBox.Text;
               if (text.Length == 0)
                  return;

               var tokens = mTokenizer.Tokenize(text);
               mHighlighter.ApplyHighlighting(mBox, tokens);
            }
            finally {
               mIsHighlighting = false;
            }
         }
      }
   }
}
