namespace DBCode.Syntax {
   internal class Dictionaries {
      private static readonly Dictionary<TokenKind, ColorRole> mCSharpColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCppColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mBasicColors2 = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mFSharpColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mHtmlColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCssColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mXmlColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mJsonColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mPowerShellColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mBatchColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mSqlColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mMarkdownColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mPythonColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };
   }

   internal static class ThemeColorProvider {
      private static readonly Dictionary<ColorRole, Color> mRoleColors = new() {
         { ColorRole.CharLiteral,            Color.Empty},
         { ColorRole.Comment,                Color.Empty},
         { ColorRole.Identifier,             Color.Empty},
         { ColorRole.Keyword,                Color.Empty},
         { ColorRole.Number,                 Color.Empty},
         { ColorRole.Operator,               Color.Empty},
         { ColorRole.PreprocessorDirective,  Color.Empty},
         { ColorRole.Punctuation,            Color.Empty},
         { ColorRole.StringLiteral,          Color.Empty},
         { ColorRole.Unknown,                Color.Empty},
         { ColorRole.Whitespace,             Color.Empty} };

      internal static Color Get(ColorRole pRole)
         => mRoleColors.TryGetValue(pRole, out var color) ? color : Color.Empty;

      internal static void Set(ColorRole pRole, Color pColor)
         => mRoleColors[pRole] = pColor;
   }

   internal static class LanguageColorRegistry {
      private static readonly Dictionary<TokenKind, ColorRole> mBatchColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mBasicColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCppColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCSharpColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mCssColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mFSharpColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mHtmlColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mJsonColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mMarkdownColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mPowerShellColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mPythonColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mSqlColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<TokenKind, ColorRole> mXmlColors = new() {
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
         { TokenKind.Whitespace,             ColorRole.Whitespace} };

      private static readonly Dictionary<LanguageKind, Dictionary<TokenKind, ColorRole>> mRegistry = new() {
         { LanguageKind.Batch,      mBatchColors},
         { LanguageKind.Basic,      mBasicColors},
         { LanguageKind.C,          mCColors},
         { LanguageKind.Cpp,        mCppColors},
         { LanguageKind.CSharp,     mCSharpColors},
         { LanguageKind.Css,        mCssColors},
         { LanguageKind.FSharp,     mFSharpColors},
         { LanguageKind.Html,       mHtmlColors},
         { LanguageKind.Json,       mJsonColors},
         { LanguageKind.Markdown,   mMarkdownColors},
         { LanguageKind.PowerShell, mPowerShellColors},
         { LanguageKind.Python,     mPythonColors},
         { LanguageKind.Sql,        mSqlColors},
         { LanguageKind.Xml,        mXmlColors} };

      internal static Dictionary<TokenKind, ColorRole> Get(LanguageKind pLanguage)
         => mRegistry.TryGetValue(pLanguage, out var map) ? map : mRegistry[LanguageKind.CSharp];
   }

   internal abstract class SyntaxHighlighterBase : IHighlighter {
      protected readonly Dictionary<TokenKind, ColorRole> mColorRoles;
      protected readonly LanguageKind mLanguage;

      protected SyntaxHighlighterBase(LanguageKind pLanguage) {
         mLanguage = pLanguage;
         mColorRoles = LanguageColorRegistry.Get(pLanguage);
      }

      public LanguageKind Language => mLanguage;

      public void ApplyHighlighting(RichTextBox pBox, IReadOnlyList<Token> pTokens) {
         int originalSelectionStart, originalSelectionLength;
         ColorRole role;
         Color color;
         if (pTokens == null || pTokens.Count == 0)
            return;
         originalSelectionStart = pBox.SelectionStart;
         originalSelectionLength = pBox.SelectionLength;
         pBox.SuspendLayout();
         foreach (var token in pTokens) {
            if (!mColorRoles.TryGetValue(token.Kind, out role))
               role = ColorRole.Unknown;
            color = ThemeColorProvider.Get(role);
            pBox.SelectionStart = token.StartIndex;
            pBox.SelectionLength = token.Length;
            pBox.SelectionColor = color;
         }
         pBox.SelectionStart = originalSelectionStart;
         pBox.SelectionLength = originalSelectionLength;
         pBox.ResumeLayout();
      }
   }

   internal static class HighlighterFactory {
      private static readonly Dictionary<LanguageKind, IHighlighter> mHighlighters = new() {
         { LanguageKind.Batch,      new BatchHighlighter()},
         { LanguageKind.Basic,      new BasicHighlighter()},
         { LanguageKind.C,          new CHighlighter()},
         { LanguageKind.Cpp,        new CppHighlighter()},
         { LanguageKind.CSharp,     new CSharpHighlighter()},
         { LanguageKind.Css,        new CssHighlighter()},
         { LanguageKind.FSharp,     new FSharpHighlighter()},
         { LanguageKind.Html,       new HtmlHighlighter()},
         { LanguageKind.Json,       new JsonHighlighter()},
         { LanguageKind.Markdown,   new MarkdownHighlighter()},
         { LanguageKind.PowerShell, new PowerShellHighlighter()},
         { LanguageKind.Python,     new PythonHighlighter()},
         { LanguageKind.Sql,        new SqlHighlighter()},
         { LanguageKind.Xml,        new XmlHighlighter()} };

      private static readonly Dictionary<string, LanguageKind> mExtensionMap = new(StringComparer.OrdinalIgnoreCase) {
         { ".bat",  LanguageKind.Batch},
         { ".cmd",  LanguageKind.Batch},
         { ".bas",  LanguageKind.Basic},
         { ".vb",   LanguageKind.Basic},
         { ".c",    LanguageKind.C},
         { ".h",    LanguageKind.C},
         { ".cpp",  LanguageKind.Cpp},
         { ".hpp",  LanguageKind.Cpp},
         { ".cs",   LanguageKind.CSharp},
         { ".css",  LanguageKind.Css},
         { ".fs",   LanguageKind.FSharp},
         { ".html", LanguageKind.Html},
         { ".htm",  LanguageKind.Html},
         { ".json", LanguageKind.Json},
         { ".md",   LanguageKind.Markdown},
         { ".ps1",  LanguageKind.PowerShell},
         { ".py",   LanguageKind.Python},
         { ".sql",  LanguageKind.Sql},
         { ".xml",  LanguageKind.Xml} };

      internal static IHighlighter GetHighlighter(string pExtension) {
         if (!mExtensionMap.TryGetValue(pExtension, out var language))
            language = LanguageKind.CSharp;

         return mHighlighters[language];
      }

      internal static IHighlighter GetHighlighter(LanguageKind pLanguage)
         => mHighlighters.TryGetValue(pLanguage, out var highlighter)
               ? highlighter
               : mHighlighters[LanguageKind.CSharp];
   }
   namespace DBCode.Syntax {
      internal sealed class SyntaxHighlighterEngine {
         private IHighlighter mHighlighter;
         private ITokenizer mTokenizer;
         private readonly RichTextBox mBox;

         private bool mIsHighlighting;

         internal SyntaxHighlighterEngine(RichTextBox pBox, LanguageKind pLanguage) {
            mBox = pBox;
            mHighlighter = HighlighterFactory.GetHighlighter(pLanguage);
            mTokenizer = LanguageRegistry.GetTokenizer(pLanguage);
         }

         internal void SetLanguage(LanguageKind pLanguage) {
            mHighlighter = HighlighterFactory.GetHighlighter(pLanguage);
            mTokenizer = LanguageRegistry.GetTokenizer(pLanguage);
         }

         internal void Highlight() {
            string text;
            IReadOnlyList<Token> tokens;
            if (mIsHighlighting)
               return;
            mIsHighlighting = true;
            try {
               text = mBox.Text;
               if (text.Length == 0)
                  return;
               tokens = mTokenizer.Tokenize(text);
               mHighlighter.ApplyHighlighting(mBox, tokens);
            }
            finally {
               mIsHighlighting = false;
            }
         }
      }
   }
}

