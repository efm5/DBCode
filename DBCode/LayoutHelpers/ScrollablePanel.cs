namespace DBCode {
   internal static partial class LayoutHelpers {
      internal class ScrollablePanel : Panel {
         public ScrollablePanel() {
            AutoScroll = true;
            Dock = DockStyle.Fill;
         }
      }
   }
}
