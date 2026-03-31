namespace DBCode.Syntax {
   internal sealed class HighlighterEngine {
      private readonly RichTextBox mRichTextBox;
      private LanguageKind mLanguage;
      internal LanguageKind Language {
         get { return mLanguage; }
      }

      internal RichTextBox Editor {
         get { return mRichTextBox; }
      }

      internal HighlighterEngine(RichTextBox pRichTextBox, LanguageKind pLanguage) {
         mRichTextBox = pRichTextBox;
         mLanguage = pLanguage;
         mTimer?.Tick += OnTimerTick;
      }

      internal void SetLanguage(LanguageKind pLanguage) {
         mLanguage = pLanguage;
      }

      private void OnTimerTick(object? pSender, EventArgs pArgs) {
         mTimer?.Stop();
         HighlightNow();
      }

      private void HighlightNow() {
         if (mIsHighlighting)
            return;
         mIsHighlighting = true;
         mSuppressTextChanged = true;
         mTimer?.Enabled = false;
         try {
            string text = mRichTextBox.Text;
            if (text.Length == 0)
               return;
            ITokenizer tokenizer = LanguageRegistry.GetTokenizer(mLanguage);
            IHighlighter highlighter = LanguageRegistry.GetHighlighter(mLanguage);
            IReadOnlyList<Token> tokens = tokenizer.Tokenize(text);
            int selectionStart = mRichTextBox.SelectionStart;
            int selectionLength = mRichTextBox.SelectionLength;

            mRichTextBox.SuspendLayout();
            try {
               mRichTextBox.Select(0, mRichTextBox.TextLength);
               if (mRichTextBox.SelectionColor != Color.Black)
                  mRichTextBox.SelectionColor = Color.Black;
               highlighter.ApplyHighlighting(mRichTextBox, tokens);
               mRichTextBox.Select(selectionStart, selectionLength);
            }
            finally {
               mRichTextBox.ResumeLayout();
            }
         }
         finally {
            mIsHighlighting = false;
            mTimer?.Enabled = true;
         }
      }
   }
}
