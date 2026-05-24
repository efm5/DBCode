namespace DBCode {
   public sealed partial class MainForm : Form {
      private static void ViewScrollingEdgeTop_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         panel.AutoScrollPosition = new Point(-panel.AutoScrollPosition.X, 0);
      }

      private static void ViewScrollingEdgeBottom_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         panel.AutoScrollPosition = new Point(-panel.AutoScrollPosition.X, panel.VerticalScroll.Maximum);
      }

      private static void ViewScrollingEdgeLeft_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         panel.AutoScrollPosition = new Point(0, -panel.AutoScrollPosition.Y);
      }

      private static void ViewScrollingEdgeRight_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         panel.AutoScrollPosition = new Point(panel.HorizontalScroll.Maximum, -panel.AutoScrollPosition.Y);
      }

      private static void ViewScrollingScrollUp_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int y = Math.Max(0, -panel.AutoScrollPosition.Y - mEm);
         panel.AutoScrollPosition = new Point(-panel.AutoScrollPosition.X, y);
      }

      private static void ViewScrollingScrollDown_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int y = Math.Min(panel.VerticalScroll.Maximum, -panel.AutoScrollPosition.Y + mEm);
         panel.AutoScrollPosition = new Point(-panel.AutoScrollPosition.X, y);
      }

      private static void ViewScrollingScrollLeft_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int x = Math.Max(0, -panel.AutoScrollPosition.X - mEm);
         panel.AutoScrollPosition = new Point(x, -panel.AutoScrollPosition.Y);
      }

      private static void ViewScrollingScrollRight_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int x = Math.Min(panel.HorizontalScroll.Maximum, -panel.AutoScrollPosition.X + mEm);
         panel.AutoScrollPosition = new Point(x, -panel.AutoScrollPosition.Y);
      }

      private static void ViewScrollingPageUp_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int y = Math.Max(0, -panel.AutoScrollPosition.Y - panel.VerticalScroll.LargeChange);
         panel.AutoScrollPosition = new Point(-panel.AutoScrollPosition.X, y);
      }

      private static void ViewScrollingPageDown_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int y = Math.Min(panel.VerticalScroll.Maximum, -panel.AutoScrollPosition.Y + panel.VerticalScroll.LargeChange);
         panel.AutoScrollPosition = new Point(-panel.AutoScrollPosition.X, y);
      }

      private static void ViewScrollingPageLeft_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int x = Math.Max(0, -panel.AutoScrollPosition.X - panel.HorizontalScroll.LargeChange);
         panel.AutoScrollPosition = new Point(x, -panel.AutoScrollPosition.Y);
      }

      private static void ViewScrollingPageRight_Click(object? pSender, EventArgs pArgs) {
         Panel? panel = mActiveScrollablePanel;
         if (panel == null)
            return;
         int x = Math.Min(panel.HorizontalScroll.Maximum, -panel.AutoScrollPosition.X + panel.HorizontalScroll.LargeChange);
         panel.AutoScrollPosition = new Point(x, -panel.AutoScrollPosition.Y);
      }
   }
}
