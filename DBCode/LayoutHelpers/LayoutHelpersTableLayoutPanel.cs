namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool TablePanelHasControls(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return false;
         if (pPanel.Controls == null)
            return false;
         return pPanel.Controls.Count > 0;
      }

      internal static bool TablePanelHasNoControls(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return true;
         if (pPanel.Controls == null)
            return true;
         return pPanel.Controls.Count == 0;
      }

      internal static int TablePanelControlCount(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return 0;
         if (pPanel.Controls == null)
            return 0;
         return pPanel.Controls.Count;
      }

      internal static void TablePanelClearControls(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return;
         if (pPanel.Controls == null)
            return;
         pPanel.Controls.Clear();
      }

      internal static void TablePanelAddControl(TableLayoutPanel? pPanel, Control? pControl, int pColumn, int pRow) {
         if (pPanel == null)
            return;
         if (pControl == null)
            return;
         if (pColumn < 0)
            return;
         if (pRow < 0)
            return;
         pPanel.Controls.Add(pControl, pColumn, pRow);
      }

      internal static void TablePanelRemoveControl(TableLayoutPanel? pPanel, Control? pControl) {
         if (pPanel == null)
            return;
         if (pControl == null)
            return;
         pPanel.Controls.Remove(pControl);
      }

      internal static bool TablePanelContainsControl(TableLayoutPanel? pPanel, Control? pControl) {
         if (pPanel == null)
            return false;
         if (pControl == null)
            return false;
         if (pPanel.Controls == null)
            return false;
         return pPanel.Controls.Contains(pControl);
      }

      internal static void TablePanelEnable(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Enabled = true;
      }

      internal static void TablePanelDisable(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Enabled = false;
      }

      internal static bool TablePanelIsEnabled(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return false;
         return pPanel.Enabled;
      }

      internal static bool TablePanelIsDisabled(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return true;
         return !pPanel.Enabled;
      }

      internal static void TablePanelShow(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Visible = true;
      }

      internal static void TablePanelHide(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return;
         pPanel.Visible = false;
      }

      internal static bool TablePanelIsVisible(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return false;
         return pPanel.Visible;
      }

      internal static bool TablePanelIsHidden(TableLayoutPanel? pPanel) {
         if (pPanel == null)
            return true;
         return !pPanel.Visible;
      }

      internal static void TablePanelSetBackColor(TableLayoutPanel? pPanel, Color pColor) {
         if (pPanel == null)
            return;
         pPanel.BackColor = pColor;
      }

      internal static void TablePanelSetPadding(TableLayoutPanel? pPanel, Padding pPadding) {
         if (pPanel == null)
            return;
         pPanel.Padding = pPadding;
      }

      internal static void TablePanelSetColumnCount(TableLayoutPanel? pPanel, int pCount) {
         if (pPanel == null)
            return;
         if (pCount < 0)
            return;
         pPanel.ColumnCount = pCount;
      }

      internal static void TablePanelSetRowCount(TableLayoutPanel? pPanel, int pCount) {
         if (pPanel == null)
            return;
         if (pCount < 0)
            return;
         pPanel.RowCount = pCount;
      }

      internal static void TablePanelSetColumnStyle(TableLayoutPanel? pPanel, int pColumn, SizeType pSizeType, float pWidth) {
         if (pPanel == null)
            return;
         if (pColumn < 0)
            return;
         if (pColumn >= pPanel.ColumnStyles.Count)
            return;
         pPanel.ColumnStyles[pColumn].SizeType = pSizeType;
         pPanel.ColumnStyles[pColumn].Width = pWidth;
      }

      internal static void TablePanelSetRowStyle(TableLayoutPanel? pPanel, int pRow, SizeType pSizeType, float pHeight) {
         if (pPanel == null)
            return;
         if (pRow < 0)
            return;
         if (pRow >= pPanel.RowStyles.Count)
            return;
         pPanel.RowStyles[pRow].SizeType = pSizeType;
         pPanel.RowStyles[pRow].Height = pHeight;
      }
   }
}
