namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool SplitterHasPanels(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return false;
         return pSplitter.Panel1 != null && pSplitter.Panel2 != null;
      }

      internal static bool SplitterHasNoPanels(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return true;
         return pSplitter.Panel1 == null || pSplitter.Panel2 == null;
      }

      internal static void SplitterEnable(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return;
         pSplitter.Enabled = true;
      }

      internal static void SplitterDisable(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return;
         pSplitter.Enabled = false;
      }

      internal static bool SplitterIsEnabled(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return false;
         return pSplitter.Enabled;
      }

      internal static bool SplitterIsDisabled(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return true;
         return !pSplitter.Enabled;
      }

      internal static void SplitterShow(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return;
         pSplitter.Visible = true;
      }

      internal static void SplitterHide(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return;
         pSplitter.Visible = false;
      }

      internal static bool SplitterIsVisible(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return false;
         return pSplitter.Visible;
      }

      internal static bool SplitterIsHidden(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return true;
         return !pSplitter.Visible;
      }

      internal static void SplitterSetOrientation(SplitContainer? pSplitter, Orientation pOrientation) {
         if (pSplitter == null)
            return;
         pSplitter.Orientation = pOrientation;
      }

      internal static void SplitterSetFixedPanel(SplitContainer? pSplitter, FixedPanel pFixedPanel) {
         if (pSplitter == null)
            return;
         pSplitter.FixedPanel = pFixedPanel;
      }

      internal static void SplitterSetIsSplitterFixed(SplitContainer? pSplitter, bool pFixed) {
         if (pSplitter == null)
            return;
         pSplitter.IsSplitterFixed = pFixed;
      }

      internal static void SplitterSetSplitterDistance(SplitContainer? pSplitter, int pDistance) {
         if (pSplitter == null)
            return;
         if (pDistance < 0)
            return;
         pSplitter.SplitterDistance = pDistance;
      }

      internal static void SplitterSetPanel1Collapsed(SplitContainer? pSplitter, bool pCollapsed) {
         if (pSplitter == null)
            return;
         pSplitter.Panel1Collapsed = pCollapsed;
      }

      internal static void SplitterSetPanel2Collapsed(SplitContainer? pSplitter, bool pCollapsed) {
         if (pSplitter == null)
            return;
         pSplitter.Panel2Collapsed = pCollapsed;
      }

      internal static bool SplitterPanel1IsCollapsed(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return false;
         return pSplitter.Panel1Collapsed;
      }

      internal static bool SplitterPanel2IsCollapsed(SplitContainer? pSplitter) {
         if (pSplitter == null)
            return false;
         return pSplitter.Panel2Collapsed;
      }

      internal static void SplitterSetBackColor(SplitContainer? pSplitter, Color pColor) {
         if (pSplitter == null)
            return;
         pSplitter.BackColor = pColor;
      }

      internal static void SplitterSetPanel1MinSize(SplitContainer? pSplitter, int pSize) {
         if (pSplitter == null)
            return;
         if (pSize < 0)
            return;
         pSplitter.Panel1MinSize = pSize;
      }

      internal static void SplitterSetPanel2MinSize(SplitContainer? pSplitter, int pSize) {
         if (pSplitter == null)
            return;
         if (pSize < 0)
            return;
         pSplitter.Panel2MinSize = pSize;
      }
   }
}
