namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class FlattenedButtonCluster : BaseCluster {
         internal Button? mButton;
         internal Control mAssociatedControl;

         internal FlattenedButtonCluster(Theme pTheme, string pText, Control pAssociatedControl,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            ThrowIfNull(pAssociatedControl, nameof(pAssociatedControl));
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mAssociatedControl = pAssociatedControl;
            if (!pText.EndsWith(':'))
               pText += ':';
            mButton = new Button() {
               Name = $"FlattenedButtonCluster{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               Location = new Point(1, 1),
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(pTheme.mFonts[(int)FontUsage.Interface]),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            pAssociatedControl.TabIndex = mTabIndex++;
            FlattenButton(mButton, pBackgroundColor);
            mButton.Click += Button_Click;
            Controls.AddRange([mButton, pAssociatedControl]);
            LayoutControls();
         }

         private void Button_Click(object? pSender, EventArgs pEventArguments) {
            mAssociatedControl.Focus();
            switch (mAssociatedControl) {
               case TextBox textBox:
                  textBox.SelectAll();
                  break;
               case NumericUpDown numericUpDown:
                  numericUpDown.Select(0, numericUpDown.Text.Length);
                  break;
               case RichTextBox richTextBox:
                  richTextBox.SelectAll();
                  break;
               case ComboBox comboBox when comboBox.Items.Count > 0:
                  comboBox.DroppedDown = true;
                  break;
               default:
                  mAssociatedControl.Select();
                  break;
            }
         }

         internal override void LayoutCluster() {
            ThrowIfNull(mButton, nameof(mButton));
            SetFontAndColor();
            LayoutControls();
            mButton.Invalidate();
         }

         internal override void LayoutControlsOnly() => LayoutControls();

         internal void LayoutControls() {
            ThrowIfNull(mButton, nameof(mButton));
            int verticalOffset = (mButton.Height - mAssociatedControl.Height) / 2;
            mAssociatedControl.Location = new Point(mButton.Right + mEmHalf, mButton.Top + verticalOffset);
         }

         internal override void SetFontAndColor() {
            ThrowIfNull(mButton, nameof(mButton));
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            UpdateFont(mButton, CreateNewFont(poFont));
            mButton.ForeColor = poForeColor;
            mButton.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               ThrowIfNull(mButton, nameof(mButton));
               mButton.Click -= Button_Click;
               MainForm.DisposeFontIfOwned(mButton.Font);
               Controls.Remove(mAssociatedControl);  // don't let BaseCluster dispose what we don't own
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
