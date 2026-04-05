namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool PanelHasControls(Panel? pPanel) {
         if (pPanel == null)
            return false;
         if (pPanel.Controls == null)
            return false;
         return pPanel.Controls.Count > 0;
      }

      internal static bool PanelHasNoControls(Panel? pPanel) {
         if (pPanel == null)
            return true;
         if (pPanel.Controls == null)
            return true;
         return pPanel.Controls.Count == 0;
      }

      internal static int PanelControlCount(Panel? pPanel) {
         if (pPanel == null)
            return 0;
         if (pPanel.Controls == null)
            return 0;
         return pPanel.Controls.Count;
      }

      internal static void PanelClearControls(Panel? pPanel) {
         if (pPanel == null)
            return;
         if (pPanel.Controls == null)
            return;
         pPanel.Controls.Clear();
      }

      internal static void PanelAddControl(Panel? pPanel, Control? pControl) {
         if (pPanel == null)
            return;
         if (pControl == null)
            return;
         pPanel.Controls.Add(pControl);
      }

      internal static void PanelRemoveControl(Panel? pPanel, Control? pControl) {
         if (pPanel == null)
            return;
         if (pControl == null)
            return;
         pPanel.Controls.Remove(pControl);
      }

      internal static bool PanelContainsControl(Panel? pPanel, Control? pControl) {
         if (pPanel == null)
            return false;
         if (pControl == null)
            return false;
         if (pPanel.Controls == null)
            return false;
         return pPanel.Controls.Contains(pControl);
      }

      internal static void PanelEnable(Panel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Enabled = true;
      }

      internal static void PanelDisable(Panel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Enabled = false;
      }

      internal static bool PanelIsEnabled(Panel? pPanel) {
         if (pPanel == null)
            return false;
         return pPanel.Enabled;
      }

      internal static bool PanelIsDisabled(Panel? pPanel) {
         if (pPanel == null)
            return true;
         return !pPanel.Enabled;
      }

      internal static void PanelShow(Panel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Visible = true;
      }

      internal static void PanelHide(Panel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Visible = false;
      }

      internal static bool PanelIsVisible(Panel? pPanel) {
         if (pPanel == null)
            return false;
         return pPanel.Visible;
      }

      internal static bool PanelIsHidden(Panel? pPanel) {
         if (pPanel == null)
            return true;
         return !pPanel.Visible;
      }

      internal static void PanelSetBackColor(Panel? pPanel, Color pColor) {
         if (pPanel == null)
            return;
         pPanel.BackColor = pColor;
      }

      internal static void PanelSetPadding(Panel? pPanel, Padding pPadding) {
         if (pPanel == null)
            return;
         pPanel.Padding = pPadding;
      }

      internal static void PanelSetBorderStyle(Panel? pPanel, BorderStyle pStyle) {
         if (pPanel == null)
            return;
         pPanel.BorderStyle = pStyle;
      }
   }
}
