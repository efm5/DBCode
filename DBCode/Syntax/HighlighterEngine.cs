namespace DBCode.Syntax {
   internal sealed class HighlighterEngine {
      private readonly RichTextBox mRichTextBox;
      private readonly System.Windows.Forms.Timer mTimer;
      private LanguageKind mLanguage;

      internal HighlighterEngine(RichTextBox pRichTextBox, LanguageKind pLanguage) {
         mRichTextBox = pRichTextBox;
         mLanguage = pLanguage;

         mTimer = new System.Windows.Forms.Timer {
            Interval = 400
         };
         mTimer.Tick += OnTimerTick;
      }

      internal void SetLanguage(LanguageKind pLanguage) {
         mLanguage = pLanguage;
      }

      internal void OnTextChanged() {
         mTimer.Stop();
         mTimer.Start();
      }

      private void OnTimerTick(object pSender, EventArgs pArgs) {
         mTimer.Stop();
         HighlightNow();
      }

      private void HighlightNow() {
         string text = mRichTextBox.Text;
         if (text.Length == 0) {
            return;
         }

         ITokenizer tokenizer = LanguageRegistry.GetTokenizer(mLanguage);
         IHighlighter highlighter = LanguageRegistry.GetHighlighter(mLanguage);

         IReadOnlyList<Token> tokens = tokenizer.Tokenize(text);

         int selectionStart = mRichTextBox.SelectionStart;
         int selectionLength = mRichTextBox.SelectionLength;

         mRichTextBox.SuspendLayout();
         try {
            mRichTextBox.Select(0, mRichTextBox.TextLength);
            mRichTextBox.SelectionColor = System.Drawing.Color.Black;

            highlighter.ApplyHighlighting(mRichTextBox, tokens);

            mRichTextBox.Select(selectionStart, selectionLength);
         }
         finally {
            mRichTextBox.ResumeLayout();
         }
      }
   }
}
