namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonCluster : BaseCluster {
         internal Control mLabel { get; private set; }
         internal Control mButton { get; private set; }
         internal Color? mBackgroundColor;

         internal LabeledButtonCluster(string pLabelText, string pButtonText, LabelPosition pLabelPosition,
            Color? pBackgroundColor = null) : base(pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonTextBoxCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
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
         }

         protected override void LayoutCluster() {
         }
      }
   }
}
