namespace DBCode.Syntax {
   internal sealed class HighlighterEngine {
#pragma warning disable IDE0032
      private readonly RichTextBox mRichTextBox;
      private LanguageKind mLanguage;
#pragma warning restore IDE0032
      internal LanguageKind Language {
         get { return mLanguage; }
      }

      internal RichTextBox Editor {
         get { return mRichTextBox; }
      }

      private IReadOnlyList<Token>? mLastTokens = null;

      internal HighlighterEngine(RichTextBox pRichTextBox, LanguageKind pLanguage) {
         mRichTextBox = pRichTextBox;
         mLanguage = pLanguage;
      }

      internal void SetLanguage(LanguageKind pLanguage) {
         mLanguage = pLanguage;
      }

      internal void HighlightNow() {
         string text;
         ITokenizer tokenizer;
         IHighlighter highlighter;
         IReadOnlyList<Token> tokens;
         int selectionStart, selectionLength;
         text = mRichTextBox.Text;
         if ((text.Length == 0) || (mCurrentLanguage == LanguageKind.PlainText)) {
            mLastTokens = null;
            return;
         }
         tokenizer = LanguageRegistry.GetTokenizer(mLanguage);
         highlighter = LanguageRegistry.GetHighlighter(mLanguage);
         tokens = tokenizer.Tokenize(text);
         mLastTokens = tokens;
         selectionStart = mRichTextBox.SelectionStart;
         selectionLength = mRichTextBox.SelectionLength;
         Color defaultBackColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.TextBox];
         SendMessage(mRichTextBox.Handle, WM_SETREDRAW, 0, 0);
         mRichTextBox.SuspendLayout();
         try {
            mRichTextBox.Select(0, mRichTextBox.TextLength);
            mRichTextBox.SelectionBackColor = defaultBackColor;
            if (mRichTextBox.SelectionColor != Color.Black)
               mRichTextBox.SelectionColor = Color.Black;
            highlighter.ApplyHighlighting(mRichTextBox, tokens, mCurrentTheme!);
            BraceMatchingEngine.ColorizeAllBraces(mRichTextBox, mLastTokens, mLanguage);
            mRichTextBox.Select(selectionStart, selectionLength);
         }
         finally {
            mRichTextBox.ResumeLayout();
            SendMessage(mRichTextBox.Handle, WM_SETREDRAW, 1, 0);
            RedrawWindow(mRichTextBox.Handle, IntPtr.Zero, IntPtr.Zero,
               RDW_INVALIDATE | RDW_ERASE | RDW_FRAME | RDW_UPDATENOW);
            mRichTextBox.Refresh();
         }
      }
   }
}
