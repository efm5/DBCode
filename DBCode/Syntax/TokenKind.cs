namespace DBCode.Syntax {
   internal enum TokenKind {
      [DisplayText("Unknown")]
      Unknown,
      [DisplayText("Whitespace")]
      Whitespace,
      [DisplayText("Identifier")]
      Identifier,
      [DisplayText("Keyword")]
      Keyword,
      [DisplayText("Number")]
      Number,
      [DisplayText("String")]
      StringLiteral,
      [DisplayText("Character")]
      CharLiteral,
      [DisplayText("Comment")]
      Comment,
      [DisplayText("Processor Directive")]
      PreprocessorDirective,
      [DisplayText("Operator")]
      Operator,
      [DisplayText("Punctuation")]
      Punctuation
   }
}
