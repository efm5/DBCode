namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabelCluster : BaseCluster {
         internal Label mLabel;

         internal LabelCluster(Theme pTheme, string pText, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mLabel = new Label() {
               Name = $"LabelCluster{nameof(mLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent,
               Location = new Point(0, 0)
            };
            Controls.Add(mLabel);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            UpdateFont(mLabel, CreateNewFont(poFont));
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               MainForm.DisposeFontIfOwned(mLabel.Font);
            base.Dispose(pDisposing);
         }
      }
   }
}
