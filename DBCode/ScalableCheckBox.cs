namespace DBCode {
   internal class ScalableCheckBox : Button {
#pragma warning disable IDE0032
      private bool mChecked = false;
#pragma warning restore IDE0032
      private bool mInterface = false;
      private Theme mTheme;
      private readonly string mLabel;

      private const string mUncheckedPrefix = "☐ ";
      private const string mCheckedPrefix = "☑ ";

      public event EventHandler? CheckedChanged;

      public bool Checked {
         get => mChecked;
         set {
            if (mChecked == value)
               return;
            mChecked = value;
            UpdateText();
            CheckedChanged?.Invoke(this, EventArgs.Empty);
         }
      }

      public ScalableCheckBox(string pText, Theme pTheme, bool pInterface) {
         mTheme = pTheme;
         mInterface = pInterface;
         mLabel = pText;
         AutoSize = true;
         AutoSizeMode = AutoSizeMode.GrowAndShrink;
         TextAlign = ContentAlignment.MiddleLeft;
         FlatStyle = FlatStyle.Flat;
         FlatAppearance.BorderSize = 0;
         Font = CreateNewFont();
         ApplyColors();
         UpdateText();
         Click += ScalableCheckBox_Click;
      }

      public void SetFontAndColors(Theme pTheme) {
         mTheme = pTheme;
         MainForm.DisposeFontIfOwned(Font);
         Font = CreateNewFont();
         ApplyColors();
         Invalidate();
      }

      private void ApplyColors() {
         Color textColor = mInterface
            ? mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont]
            : mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
         Color backColor = mInterface
            ? mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground]
            : mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
         ForeColor = textColor;
         BackColor = backColor;
         FlatAppearance.BorderColor = textColor;
         FlatAppearance.MouseOverBackColor = backColor;
         FlatAppearance.MouseDownBackColor = backColor;
      }

      private void UpdateText() {
         Text = (mChecked ? mCheckedPrefix : mUncheckedPrefix) + mLabel;
      }

      private void ScalableCheckBox_Click(object? pSender, EventArgs pEventArgs) {
         Checked = !mChecked;
      }

      protected override void OnGotFocus(EventArgs pEventArgs) {
         base.OnGotFocus(pEventArgs);
         Invalidate();
      }

      protected override void OnLostFocus(EventArgs pEventArgs) {
         base.OnLostFocus(pEventArgs);
         Invalidate();
      }

      //protected override void OnPaint(PaintEventArgs pPaintEventArgs) {
      //   base.OnPaint(pPaintEventArgs);
      //   if (Focused) {
      //      using Pen focusPen = new Pen(ForeColor);
      //      pPaintEventArgs.Graphics.DrawRectangle(focusPen, 1, 1, Width - 3, Height - 3);
      //   }
      //}

      protected override void Dispose(bool pDisposing) {
         if (pDisposing)
            Click -= ScalableCheckBox_Click;
         base.Dispose(pDisposing);
      }
   }
}
