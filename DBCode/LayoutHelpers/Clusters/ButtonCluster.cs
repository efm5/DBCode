namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ButtonCluster : BaseCluster {
         internal Button mButton;

         internal ButtonCluster(string pText, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {

            mButton = new Button() {
               Name = $"ButtonCluster{nameof(mButton)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
         }

         protected override void LayoutCluster() {
            // No-op: BaseCluster handles positioning and sizing.
         }
      }
   }
}
