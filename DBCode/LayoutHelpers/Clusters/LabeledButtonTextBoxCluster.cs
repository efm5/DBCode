namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonTextBoxCluster : BaseCluster {
         internal Label mLabel { get; private set; }
         internal Button mButton { get; private set; }
         internal TextBox mExampleTextBox { get; private set; }
         internal Color? mBackgroundColor;

         internal LabeledButtonTextBoxCluster(string pLabelText, string pButtonText, LabelPosition pLabelPosition,
            Color? pBackgroundColor = null) : base(pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED,
               Name = $"LabeledButtonTextBoxCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               Top = mEm,
               TextAlign = ContentAlignment.MiddleCenter,
               Height = 40,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               Top = mEm,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont]
               //BackColor = pBackgroundColor ?? Color.Transparent
            };
            mExampleTextBox = new TextBox() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mLabel)}{mTabIndex++}",
               Top = mEm,
               Width = 300,
               Text = mUnicodeSampleString,
               Multiline = false,
               Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont]
            };
            mExampleTextBox.BackColor = pBackgroundColor ?? mCurrentTheme!.mColors[(int)ColorUsage.InterfaceBackground];
            Controls.AddRange(mLabel, mButton, mExampleTextBox);
            ApplyLabelPosition(mLabel, mButton);
         }

         protected override void LayoutCluster() {
         }
      }
   }
}
