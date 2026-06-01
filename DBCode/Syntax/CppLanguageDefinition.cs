namespace DBCode.Syntax {
   internal sealed class CppLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "alignas", "alignof", "and", "and_eq", "asm", "auto", "bitand",
         "bitor", "bool", "break", "case", "catch", "char", "class",
         "co_await", "co_return", "co_yield", "compl", "concept", "const",
         "const_cast", "consteval", "constexpr", "constinit", "continue",
         "decltype", "default", "delete", "do", "double", "dynamic_cast",
         "else", "enum", "explicit", "export", "extern", "false", "final",
         "float", "for", "friend", "goto", "if", "inline", "int", "long",
         "mutable", "namespace", "new", "noexcept", "not", "not_eq",
         "nullptr", "operator", "or", "or_eq", "override", "private",
         "protected", "public", "register", "reinterpret_cast", "requires",
         "restrict", "return", "short", "signed", "sizeof", "static",
         "static_assert", "static_cast", "struct", "switch", "template",
         "this", "thread_local", "throw", "true", "try", "typedef", "typeid",
         "typename", "union", "unsigned", "using", "virtual", "void",
         "volatile", "wchar_t", "while", "xor", "xor_eq"
      ];

      public LanguageKind Language => LanguageKind.Cpp;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
