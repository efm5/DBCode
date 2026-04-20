
namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabelCluster : BaseCluster {
         internal Label mLabel;

         internal LabelCluster(string pText, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {

            mLabel = new Label() {
               Name = $"LabelCluster{nameof(mLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };

            Controls.Add(mLabel);
         }

         protected override void LayoutCluster() {
            // No-op: BaseCluster handles positioning and sizing.
         }
      }
   }
}
