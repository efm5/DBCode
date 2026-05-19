namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ComboBoxCluster : BaseCluster {
         internal FlattenedButtonCluster? mPrefixButton;
         internal ComboBox? mComboBox;

         internal ComboBoxCluster(Theme pTheme, string pText, Color pBackgroundColor)
            : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            if (!pText.EndsWith(':'))
               pText += ':';
            mComboBox = new ComboBox() {
               Name = $"ComboBoxCluster{mTabIndex}",
               TabIndex = mTabIndex++,
               DropDownStyle = ComboBoxStyle.DropDown,
               Width = FIND_WIDTH,
               Font = CreateNewFont(pTheme.mFonts[(int)FontUsage.Interface])
            };
            mPrefixButton = new FlattenedButtonCluster(pTheme, pText, mComboBox, pBackgroundColor);
            Controls.AddRange([mPrefixButton, mComboBox]);
         }

         internal override void LayoutCluster() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mComboBox, nameof(mComboBox));
            SetFontAndColor();
            LayoutControls();
            mPrefixButton.Invalidate();
            mComboBox.Invalidate();
         }

         internal override void LayoutControlsOnly() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mComboBox, nameof(mComboBox));
            LayoutControls();
            mPrefixButton.Invalidate();
            mComboBox.Invalidate();
         }

         internal void LayoutControls() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mComboBox, nameof(mComboBox));
            mComboBox.Location = new Point(mPrefixButton.Right, mPrefixButton.Top);
         }

         internal override void SetFontAndColor() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mComboBox, nameof(mComboBox));
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mPrefixButton.SetFontAndColor();
            UpdateFont(mComboBox, CreateNewFont(poFont));
            mComboBox.ForeColor = poForeColor;
            mComboBox.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mComboBox != null)
                  MainForm.DisposeFontIfOwned(mComboBox.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
