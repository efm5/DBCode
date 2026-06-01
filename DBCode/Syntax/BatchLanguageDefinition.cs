namespace DBCode.Syntax {
   internal sealed class BatchLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "call", "cd", "chdir", "choice", "cls", "cmd", "color", "copy",
         "date", "del", "dir", "echo", "endlocal", "erase", "exit", "find",
         "findstr", "for", "format", "ftype", "goto", "help", "if", "label",
         "md", "mkdir", "mklink", "more", "move", "net", "pause", "path",
         "popd", "print", "prompt", "pushd", "rd", "rem", "ren", "rename",
         "rmdir", "set", "setlocal", "shift", "sort", "start", "time",
         "title", "tree", "type", "ver", "verify", "vol", "where", "xcopy"
      ];

      public LanguageKind Language => LanguageKind.Batch;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.OrdinalIgnoreCase;
   }
}
