namespace DBCode.Syntax {
   internal enum TokenKind {
      [DisplayText("Unknown Token Color")]
      Unknown,
      [DisplayText("Whitespace Token Color")]
      Whitespace,
      [DisplayText("Identifier Token Color")]
      Identifier,
      [DisplayText("Keyword Token Color")]
      Keyword,
      [DisplayText("Number Token Color")]
      Number,
      [DisplayText("String Literal Token Color")]
      StringLiteral,
      [DisplayText("Character Literal Token Color")]
      CharLiteral,
      [DisplayText("Comment Token Color")]
      Comment,
      [DisplayText("Preprocessor Directive Token Color")]
      PreprocessorDirective,
      [DisplayText("Operator Token Color")]
      Operator,
      [DisplayText("Punctuation Token Color")]
      Punctuation
   }
}
