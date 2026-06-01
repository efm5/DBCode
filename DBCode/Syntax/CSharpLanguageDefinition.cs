namespace DBCode.Syntax {
   internal sealed class CSharpLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "abstract", "as", "base", "bool", "break", "byte", "case", "catch",
         "char", "checked", "class", "const", "continue", "decimal", "default",
         "delegate", "do", "double", "else", "enum", "event", "explicit",
         "extern", "false", "finally", "fixed", "float", "for", "foreach",
         "goto", "if", "implicit", "in", "int", "interface", "internal", "is",
         "lock", "long", "namespace", "new", "null", "object", "operator", "out",
         "override", "params", "private", "protected", "public", "readonly",
         "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc",
         "static", "string", "struct", "switch", "this", "throw", "true", "try",
         "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
         "virtual", "void", "volatile", "while",
         "add", "alias", "ascending", "async", "await", "by", "descending",
         "dynamic", "equals", "from", "get", "global", "group", "into", "join",
         "let", "managed", "nameof", "notnull", "on", "orderby", "partial",
         "record", "remove", "required", "scoped", "select", "set", "unmanaged",
         "value", "var", "when", "where", "with", "yield",
         "Boolean", "Byte", "Char", "Decimal", "Double", "Int16", "Int32",
         "Int64", "IntPtr", "Object", "SByte", "Single", "String", "UInt16",
         "UInt32", "UInt64", "UIntPtr", "nint", "nuint"
      ];

      public LanguageKind Language => LanguageKind.CSharp;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
