using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class LabeledButtonTextBoxCluster : BaseCluster {
         internal Label mLabel { get; private set; }
         internal Button mButton { get; private set; }
         internal TextBox mExampleTextBox { get; private set; }
         internal Color? mBackgroundColor;

         internal LabeledButtonTextBoxCluster(string pLabelText, string pButtonText, LabelPosition pLabelPosition,
            Color? pBackgroundColor = null)
            : base(pBackgroundColor) {

            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonTextBoxCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               TextAlign = ContentAlignment.MiddleCenter,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont]
            };
            mExampleTextBox = new TextBox() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mExampleTextBox)}{mTabIndex++}",
               Width = 300,
               Text = mUnicodeSampleString,
               Multiline = false,
               Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceBackground]
            };
            Controls.AddRange([mLabel, mButton, mExampleTextBox]);
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            ApplyLabelPosition(mLabel, mButton);
            GlueControlsHorizontally(mButton, mExampleTextBox, mEm);
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeSimpleThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mButton.Font = poFont;
            mButton.ForeColor = poForeColor;
            mButton.BackColor = poBackColor;
            mExampleTextBox.Font = poFont;
            mExampleTextBox.ForeColor = poForeColor;
            mExampleTextBox.BackColor = poBackColor;
         }
      }
   }
}
