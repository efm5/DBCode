using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonCluster : BaseCluster {
         internal Label mLabel { get; private set; }
         internal Button mButton { get; private set; }
         internal Color? mBackgroundColor;

         internal LabeledButtonCluster(Theme pTheme, string pLabelText, string pButtonText, LabelPosition pLabelPosition,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonTextBoxCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont]
            };
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            ApplyLabelPosition(mLabel as Label, mButton);
            mLabel.Invalidate();
            mButton.Invalidate();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mButton.Font = CreateNewFont(poFont);
            mButton.ForeColor = poForeColor;
            mButton.BackColor = poBackColor;
         }
      }
   }
}
