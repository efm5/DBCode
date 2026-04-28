namespace DBCode.Syntax {
   internal abstract class SyntaxHighlighterBase : IHighlighter {
      protected readonly Dictionary<TokenKind, ColorRole> mColorRoles;
      protected readonly LanguageKind mLanguage;

#pragma warning disable IDE0290
      protected SyntaxHighlighterBase(LanguageKind pLanguage) {
         mLanguage = pLanguage;
      }
#pragma warning restore IDE0290

      public LanguageKind Language => mLanguage;

      public void ApplyHighlighting(RichTextBox pBox, IReadOnlyList<Token> pTokens, Theme pTheme) {
         int originalSelectionStart, originalSelectionLength;
         Color color;
         if (pTokens == null || pTokens.Count == 0)
            return;
         originalSelectionStart = pBox.SelectionStart;
         originalSelectionLength = pBox.SelectionLength;
         foreach (Token token in pTokens) {
            color = pTheme.mHighlightColors[(int)mLanguage][(int)token.Kind];
            pBox.SelectionStart = token.StartIndex;
            pBox.SelectionLength = token.Length;
            pBox.SelectionColor = color;
         }
         pBox.SelectionStart = originalSelectionStart;
         pBox.SelectionLength = originalSelectionLength;
      }
   }
}
