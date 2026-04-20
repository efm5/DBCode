namespace DBCode.Syntax {
   internal sealed class BatchHighlighter : SyntaxHighlighterBase {
      internal BatchHighlighter() : base(LanguageKind.Batch) { }
   }

   internal sealed class BasicHighlighter : SyntaxHighlighterBase {
      internal BasicHighlighter() : base(LanguageKind.Basic) { }
   }

   internal sealed class CHighlighter : SyntaxHighlighterBase {
      internal CHighlighter() : base(LanguageKind.C) { }
   }

   internal sealed class CppHighlighter : SyntaxHighlighterBase {
      internal CppHighlighter() : base(LanguageKind.Cpp) { }
   }

   internal sealed class CSharpHighlighter : SyntaxHighlighterBase {
      internal CSharpHighlighter() : base(LanguageKind.CSharp) { }
   }

   internal sealed class CssHighlighter : SyntaxHighlighterBase {
      internal CssHighlighter() : base(LanguageKind.Css) { }
   }

   internal sealed class FSharpHighlighter : SyntaxHighlighterBase {
      internal FSharpHighlighter() : base(LanguageKind.FSharp) { }
   }

   internal sealed class HtmlHighlighter : SyntaxHighlighterBase {
      internal HtmlHighlighter() : base(LanguageKind.Html) { }
   }

   internal sealed class JsonHighlighter : SyntaxHighlighterBase {
      internal JsonHighlighter() : base(LanguageKind.Json) { }
   }

   internal sealed class MarkdownHighlighter : SyntaxHighlighterBase {
      internal MarkdownHighlighter() : base(LanguageKind.Markdown) { }
   }

   internal sealed class PowerShellHighlighter : SyntaxHighlighterBase {
      internal PowerShellHighlighter() : base(LanguageKind.PowerShell) { }
   }

   internal sealed class PythonHighlighter : SyntaxHighlighterBase {
      internal PythonHighlighter() : base(LanguageKind.Python) { }
   }

   internal sealed class SqlHighlighter : SyntaxHighlighterBase {
      internal SqlHighlighter() : base(LanguageKind.Sql) { }
   }

   internal sealed class XmlHighlighter : SyntaxHighlighterBase {
      internal XmlHighlighter() : base(LanguageKind.Xml) { }
   }
}
