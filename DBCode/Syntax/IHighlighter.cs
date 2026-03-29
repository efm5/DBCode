namespace DBCode.Syntax {
   internal interface IHighlighter {
      LanguageKind Language { get; }

      void ApplyHighlighting(RichTextBox pRichTextBox, IReadOnlyList<Token> pTokens);
   }
}
