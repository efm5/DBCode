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
         if ((text.Length == 0) || (mCurrentLanguage == LanguageKind.PlainText))
            return;
         tokenizer = LanguageRegistry.GetTokenizer(mLanguage);
         highlighter = LanguageRegistry.GetHighlighter(mLanguage);
         tokens = tokenizer.Tokenize(text);
         selectionStart = mRichTextBox.SelectionStart;
         selectionLength = mRichTextBox.SelectionLength;
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
   }
}
