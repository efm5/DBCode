namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonCluster : BaseCluster {
         internal Control LabelControl { get; private set; }
         internal Control ButtonControl { get; private set; }

         internal LabeledButtonCluster(Control pLabelControl, Control pButtonControl, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {
            LabelControl = pLabelControl;
            ButtonControl = pButtonControl;
         }
      }
   }
}
