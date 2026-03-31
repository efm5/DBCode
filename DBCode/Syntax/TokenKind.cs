namespace DBCode.Syntax {
   internal enum TokenKind {
      Unknown,
      Whitespace,
      Identifier,
      Keyword,
      Number,
      StringLiteral,
      CharLiteral,
      Comment,
      PreprocessorDirective,
      Operator,
      Punctuation
   }
}
