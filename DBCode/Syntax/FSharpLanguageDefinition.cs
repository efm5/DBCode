namespace DBCode.Syntax {
   internal sealed class FSharpLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "abstract", "and", "as", "assert", "async", "base", "begin", "class",
         "default", "delegate", "do", "done", "downcast", "downto", "elif",
         "else", "end", "exception", "extern", "false", "finally", "for",
         "fun", "function", "global", "if", "in", "inherit", "inline",
         "interface", "internal", "lazy", "let", "match", "member", "module",
         "mutable", "namespace", "new", "not", "null", "of", "open", "or",
         "override", "private", "public", "rec", "return", "sig", "static",
         "struct", "then", "to", "true", "try", "type", "upcast", "use",
         "val", "void", "when", "while", "with", "yield"
      ];

      public LanguageKind Language => LanguageKind.FSharp;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
