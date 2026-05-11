namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class UpDownCluster : BaseCluster {
         internal FlattenedButtonCluster? mPrefixButton;
         internal NumericUpDown? mNumericUpDown;
         internal Label? mSuffixLabel = null;

         internal UpDownCluster(Theme pTheme, string pText, int pMinimum, int pMaximum, int pValue,
            int pIncrement, Color pBackgroundColor, string? pSuffixText = null)
            : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            if (!pText.EndsWith(':'))
               pText += ':';
            mNumericUpDown = new NumericUpDown() {
               Name = $"UpDownCluster{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = pMinimum,
               Maximum = pMaximum,
               Value = pValue,
               Increment = pIncrement,
               Font = CreateNewFont(pTheme.mFonts[(int)FontUsage.Interface]),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor
            };
            mPrefixButton = new FlattenedButtonCluster(pTheme, pText, mNumericUpDown, pBackgroundColor);
            SetUpDownBoxWidth(mNumericUpDown);
            if (!string.IsNullOrEmpty(pSuffixText)) {
               mSuffixLabel = new Label() {
                  Name = $"UpDownClusterSuffix{mTabIndex}",
                  TabIndex = mTabIndex++,
                  Text = pSuffixText,
                  AutoSize = true,
                  Font = CreateNewFont(pTheme.mFonts[(int)FontUsage.Interface]),
                  ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
                  BackColor = pBackgroundColor
               };
               Controls.AddRange([mPrefixButton, mNumericUpDown, mSuffixLabel]);
            }
            else
               Controls.AddRange([mPrefixButton, mNumericUpDown]);
         }

         internal override void LayoutCluster() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mNumericUpDown, nameof(mNumericUpDown));
            SetFontAndColor();
            LayoutControls();
            mPrefixButton.Invalidate();
            mNumericUpDown.Invalidate();
            if (mSuffixLabel != null)
               mSuffixLabel.Invalidate();
         }

         internal void LayoutControls() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mNumericUpDown, nameof(mNumericUpDown));
            mNumericUpDown.Location = new Point(mPrefixButton.Right, mPrefixButton.Top);
            if (mSuffixLabel != null)
               mSuffixLabel.Location = new Point(mNumericUpDown.Right, mNumericUpDown.Top);
         }

         internal override void SetFontAndColor() {
            ThrowIfNull(mPrefixButton, nameof(mPrefixButton));
            ThrowIfNull(mNumericUpDown, nameof(mNumericUpDown));
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mPrefixButton.Font = CreateNewFont(poFont);
            mPrefixButton.ForeColor = poForeColor;
            mPrefixButton.BackColor = poBackColor;
            mNumericUpDown.Font = CreateNewFont(poFont);
            mNumericUpDown.ForeColor = poForeColor;
            mNumericUpDown.BackColor = poBackColor;
            if (mSuffixLabel != null) {
               mSuffixLabel.Font = CreateNewFont(poFont);
               mSuffixLabel.ForeColor = poForeColor;
               mSuffixLabel.BackColor = poBackColor;
            }
         }
      }
   }
}
