namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class CheckBoxCluster : BaseCluster {
         internal CheckBox mCheckBox;

         internal CheckBoxCluster(string pText, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {

            mCheckBox = new CheckBox() {
               Name = $"CheckBoxCluster{nameof(mCheckBox)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };

            Controls.Add(mCheckBox);
         }

         protected override void LayoutCluster() {
            // No-op: BaseCluster handles positioning and sizing.
         }
      }
   }
}
