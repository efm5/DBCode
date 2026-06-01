namespace DBCode.Syntax {
   internal sealed class PowerShellLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "begin", "break", "catch", "class", "continue", "data", "define",
         "do", "dynamicparam", "else", "elseif", "end", "enum", "exit",
         "filter", "finally", "for", "foreach", "from", "function", "hidden",
         "if", "in", "inlinescript", "param", "parallel", "process", "return",
         "sequence", "static", "switch", "throw", "trap", "try", "until",
         "using", "var", "while", "workflow"
      ];

      public LanguageKind Language => LanguageKind.PowerShell;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.OrdinalIgnoreCase;
   }
}
