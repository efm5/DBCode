namespace DBCode.Syntax.Tokenizing {
   internal enum TokenKind {
      Unknown,
      Whitespace,
      Identifier,
      Keyword,
      NumberLiteral,
      StringLiteral,
      CharLiteral,
      Comment,
      PreprocessorDirective,
      Operator,
      Punctuation
   }
}
