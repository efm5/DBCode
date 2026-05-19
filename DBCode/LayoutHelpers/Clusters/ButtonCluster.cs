namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ButtonCluster : BaseCluster {
         internal Button? mButton;

         internal ButtonCluster(Theme pTheme, string pText, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mButton = new Button() {
               Name = $"ButtonCluster{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               Location = new Point(1, 1),
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(pTheme.mFonts[(int)FontUsage.Interface]),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            Controls.Add(mButton);
         }

         internal override void LayoutCluster() {
            ThrowIfNull(mButton, nameof(mButton));
            SetFontAndColor();
            mButton.Invalidate();
         }

         internal static void LayoutControls() {
            // nothing to do
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
               MainForm.DisposeFontIfOwned(mButton.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
