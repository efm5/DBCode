namespace DBCode {
   internal sealed class LineNumberPanel : Panel {
      private PlainRichTextBox mTextBox = null!;
      private List<int> mLineStarts = [];

      #region public methods
      internal void Initialize(PlainRichTextBox pTextBox) {
         mTextBox = pTextBox;
         mTextBox.TextChanged += OnTextChanged;
         mTextBox.ViewChanged += OnViewChanged;
         DoubleBuffered = true;
         RebuildLineStarts();
      }
      internal void ApplyTheme(Theme pTheme) {
         BackColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
         ForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
         Font = pTheme.mFonts[(int)FontUsage.Text];
         RebuildLineStarts();
      }
      #endregion

      #region private methods
      private void RebuildLineStarts() {
         string text = mTextBox.Text;
         int digitCount = 0;
         string widestNumber = string.Empty;
         int requiredWidth = 0;
         mLineStarts.Clear();
         mLineStarts.Add(0);
         for (int i = 0; i < text.Length; i++) {
            if (text[i] == '\n')
               mLineStarts.Add(i + 1);
         }
         digitCount = mLineStarts.Count.ToString().Length;
         widestNumber = new string('9', digitCount);
         requiredWidth = TextRenderer.MeasureText(widestNumber, Font).Width + mEmHalf * 2;
         if (Width != requiredWidth)
            Width = requiredWidth;
         Invalidate();
      }
      private void OnTextChanged(object? pSender, EventArgs pEventArguments) {
         RebuildLineStarts();
      }
      private void OnViewChanged(object? pSender, EventArgs pEventArguments) {
         Invalidate();
      }
      protected override void OnPaint(PaintEventArgs pEventArguments) {
         base.OnPaint(pEventArguments);
         Graphics graphics = pEventArguments.Graphics;
         Color foreColor = ForeColor;
         Font font = Font;
         int panelHeight = Height;
         int rightEdge = Width - mEmHalf;
         int glyphHeight = TextRenderer.MeasureText("0", font, Size.Empty, TextFormatFlags.NoPadding).Height;
         int rtbLineHeight = 0;
         if (mLineStarts.Count >= 2) {
            int firstY = mTextBox.GetPositionFromCharIndex(mLineStarts[0]).Y;
            int secondY = mTextBox.GetPositionFromCharIndex(mLineStarts[1]).Y;
            rtbLineHeight = secondY - firstY;
         }
         if (rtbLineHeight <= 0)
            rtbLineHeight = glyphHeight;
         int yOffset = Math.Max(0, rtbLineHeight - glyphHeight) + 2;
         for (int i = 0; i < mLineStarts.Count; i++) {
            Point position = mTextBox.GetPositionFromCharIndex(mLineStarts[i]);
            if (position.Y + rtbLineHeight < 0)
               continue;
            if (position.Y > panelHeight)
               break;
            Rectangle drawRectangle = new Rectangle(0, position.Y + yOffset, rightEdge, glyphHeight);
            TextRenderer.DrawText(graphics, (i + 1).ToString(), font, drawRectangle,
               foreColor, TextFormatFlags.Right | TextFormatFlags.NoPadding);
         }
      }
      #endregion
   }
}
