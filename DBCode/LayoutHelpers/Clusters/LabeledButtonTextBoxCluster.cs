namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class LabeledButtonTextBoxCluster : BaseCluster {
         internal Label mLabel { get; private set; }
         internal Button mButton { get; private set; }
         internal TextBox mExampleTextBox { get; private set; }
         internal Color? mBackgroundColor;

         internal LabeledButtonTextBoxCluster(string pLabelText, string pButtonText, LabelPosition pLabelPosition, Color? pBackgroundColor = null)
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

            Controls.Add(mLabel);
            Controls.Add(mButton);
            Controls.Add(mExampleTextBox);
         }

         protected override void LayoutCluster() {
            // Update theme-dependent properties
            mLabel.Font = CreateNewFont();
            mLabel.ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont];
            mLabel.BackColor = mBackgroundColor ?? Color.Transparent;

            mButton.Font = CreateNewFont();
            mButton.ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont];

            mExampleTextBox.Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]);
            mExampleTextBox.ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont];
            mExampleTextBox.BackColor = mBackgroundColor ?? mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceBackground];

            // 1. Label + button: normal label-position logic
            ApplyLabelPosition(mLabel, mButton);

            // 2. Button → TextBox: explicit big gap, one line, fixed orientation
            GlueControlsHorizontally(mButton, mExampleTextBox, mEm);

            // Compute cluster size
            int rightmost = Math.Max(mLabel.Right, Math.Max(mButton.Right, mExampleTextBox.Right));
            int bottommost = Math.Max(mLabel.Bottom, Math.Max(mButton.Bottom, mExampleTextBox.Bottom));

            Size = new Size((rightmost + mEm), (bottommost + mEm));
         }
      }
   }
}
