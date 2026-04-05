namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool TabControlHasPages(TabControl? pTabControl) {
         if (pTabControl == null)
            return false;
         return pTabControl.TabPages.Count > 0;
      }

      internal static bool TabControlHasNoPages(TabControl? pTabControl) {
         if (pTabControl == null)
            return true;
         return pTabControl.TabPages.Count == 0;
      }

      internal static int TabControlPageCount(TabControl? pTabControl) {
         if (pTabControl == null)
            return 0;
         return pTabControl.TabPages.Count;
      }

      internal static void TabControlAddPage(TabControl? pTabControl, TabPage? pPage) {
         if (pTabControl == null)
            return;
         if (pPage == null)
            return;
         pTabControl.TabPages.Add(pPage);
      }

      internal static void TabControlRemovePage(TabControl? pTabControl, TabPage? pPage) {
         if (pTabControl == null)
            return;
         if (pPage == null)
            return;
         pTabControl.TabPages.Remove(pPage);
      }

      internal static bool TabControlContainsPage(TabControl? pTabControl, TabPage? pPage) {
         if (pTabControl == null)
            return false;
         if (pPage == null)
            return false;
         return pTabControl.TabPages.Contains(pPage);
      }

      internal static bool TabControlHasSelection(TabControl? pTabControl) {
         if (pTabControl == null)
            return false;
         return pTabControl.SelectedIndex >= 0;
      }

      internal static bool TabControlHasNoSelection(TabControl? pTabControl) {
         if (pTabControl == null)
            return true;
         return pTabControl.SelectedIndex < 0;
      }

      internal static int TabControlSelectedIndexOrMinusOne(TabControl? pTabControl) {
         if (pTabControl == null)
            return -1;
         return pTabControl.SelectedIndex;
      }

      internal static TabPage? TabControlSelectedPageOrNull(TabControl? pTabControl) {
         if (pTabControl == null)
            return null;
         return pTabControl.SelectedTab;
      }

      internal static void TabControlSelectIndex(TabControl? pTabControl, int pIndex) {
         if (pTabControl == null)
            return;
         if (pIndex < 0)
            return;
         if (pIndex >= pTabControl.TabPages.Count)
            return;
         pTabControl.SelectedIndex = pIndex;
      }

      internal static void TabControlSelectFirst(TabControl? pTabControl) {
         if (pTabControl == null)
            return;
         if (pTabControl.TabPages.Count == 0)
            return;
         pTabControl.SelectedIndex = 0;
      }

      internal static void TabControlSelectLast(TabControl? pTabControl) {
         if (pTabControl == null)
            return;
         int count = pTabControl.TabPages.Count;
         if (count == 0)
            return;
         pTabControl.SelectedIndex = count - 1;
      }

      internal static void TabControlEnable(TabControl? pTabControl) {
         if (pTabControl == null)
            return;
         pTabControl.Enabled = true;
      }

      internal static void TabControlDisable(TabControl? pTabControl) {
         if (pTabControl == null)
            return;
         pTabControl.Enabled = false;
      }

      internal static bool TabControlIsEnabled(TabControl? pTabControl) {
         if (pTabControl == null)
            return false;
         return pTabControl.Enabled;
      }

      internal static bool TabControlIsDisabled(TabControl? pTabControl) {
         if (pTabControl == null)
            return true;
         return !pTabControl.Enabled;
      }

      internal static void TabControlShow(TabControl? pTabControl) {
         if (pTabControl == null)
            return;
         pTabControl.Visible = true;
      }

      internal static void TabControlHide(TabControl? pTabControl) {
         if (pTabControl == null)
            return;
         pTabControl.Visible = false;
      }

      internal static bool TabControlIsVisible(TabControl? pTabControl) {
         if (pTabControl == null)
            return false;
         return pTabControl.Visible;
      }

      internal static bool TabControlIsHidden(TabControl? pTabControl) {
         if (pTabControl == null)
            return true;
         return !pTabControl.Visible;
      }

      internal static void TabControlSetPadding(TabControl? pTabControl, Padding pPadding) {
         if (pTabControl == null)
            return;
         pTabControl.Padding = new Point(pPadding.Left, pPadding.Top);
      }

      internal static void TabControlSetAlignment(TabControl? pTabControl, TabAlignment pAlignment) {
         if (pTabControl == null)
            return;
         pTabControl.Alignment = pAlignment;
      }

      internal static void TabControlSetAppearance(TabControl? pTabControl, TabAppearance pAppearance) {
         if (pTabControl == null)
            return;
         pTabControl.Appearance = pAppearance;
      }

      internal static void TabControlSetMultiline(TabControl? pTabControl, bool pMultiline) {
         if (pTabControl == null)
            return;
         pTabControl.Multiline = pMultiline;
      }
   }
}
