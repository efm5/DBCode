namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class FlattenedButtonCluster : BaseCluster {
         internal Button? mButton;
         internal Control mAssociatedControl;

         internal FlattenedButtonCluster(Theme pTheme, string pText, Control pAssociatedControl,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            ThrowIfNull(pAssociatedControl, nameof(pAssociatedControl));
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
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            pAssociatedControl.TabIndex = mTabIndex++;
            FlattenButton(mButton, pBackgroundColor);
            mButton.Click += Button_Click;  // added
            Controls.AddRange([mButton, pAssociatedControl]);
         }

         private void Button_Click(object? pSender, EventArgs pEventArguments) {
            mAssociatedControl.Focus();
            mAssociatedControl.Select();
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            LayoutControls();
            mButton?.Invalidate();
         }

         internal void LayoutControls() {
            mAssociatedControl.Location = new Point(mButton!.Right, mButton.Top);
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mButton!.Font = CreateNewFont(poFont);
            mButton.ForeColor = poForeColor;
            mButton.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               mButton!.Click -= Button_Click;
            base.Dispose(pDisposing);
         }
      }
   }
}
