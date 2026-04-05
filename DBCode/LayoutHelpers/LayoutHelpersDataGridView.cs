namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool GridHasRows(DataGridView? pGrid) {
         if (pGrid == null)
            return false;
         return pGrid.Rows.Count > 0;
      }

      internal static bool GridHasNoRows(DataGridView? pGrid) {
         if (pGrid == null)
            return true;
         return pGrid.Rows.Count == 0;
      }

      internal static int GridRowCount(DataGridView? pGrid) {
         if (pGrid == null)
            return 0;
         return pGrid.Rows.Count;
      }

      internal static bool GridHasColumns(DataGridView? pGrid) {
         if (pGrid == null)
            return false;
         return pGrid.Columns.Count > 0;
      }

      internal static bool GridHasNoColumns(DataGridView? pGrid) {
         if (pGrid == null)
            return true;
         return pGrid.Columns.Count == 0;
      }

      internal static int GridColumnCount(DataGridView? pGrid) {
         if (pGrid == null)
            return 0;
         return pGrid.Columns.Count;
      }

      internal static void GridClearRows(DataGridView? pGrid) {
         if (pGrid == null)
            return;
         pGrid.Rows.Clear();
      }

      internal static void GridClearColumns(DataGridView? pGrid) {
         if (pGrid == null)
            return;
         pGrid.Columns.Clear();
      }

      internal static void GridAddColumn(DataGridView? pGrid, DataGridViewColumn? pColumn) {
         if (pGrid == null)
            return;
         if (pColumn == null)
            return;
         pGrid.Columns.Add(pColumn);
      }

      internal static void GridRemoveColumn(DataGridView? pGrid, DataGridViewColumn? pColumn) {
         if (pGrid == null)
            return;
         if (pColumn == null)
            return;
         pGrid.Columns.Remove(pColumn);
      }

      internal static bool GridContainsColumn(DataGridView? pGrid, string? pName) {
         if (pGrid == null)
            return false;
         if (string.IsNullOrWhiteSpace(pName))
            return false;
         return pGrid.Columns.Contains(pName);
      }
      internal static void GridAddRow(DataGridView? pGrid, params object?[]? pValues) {
         if (pGrid == null || pValues == null)
            return;
         int count = pValues.Length;
         object[] values = new object[count];
         for (int index = 0; index < count; index++)
            values[index] = pValues[index] ?? string.Empty;
         _ = pGrid.Rows.Add(values);
      }

      internal static void GridRemoveRowAt(DataGridView? pGrid, int pIndex) {
         if (pGrid == null)
            return;
         if (pIndex < 0)
            return;
         if (pIndex >= pGrid.Rows.Count)
            return;
         pGrid.Rows.RemoveAt(pIndex);
      }

      internal static bool GridHasSelection(DataGridView? pGrid) {
         if (pGrid == null)
            return false;
         return pGrid.SelectedCells.Count > 0;
      }

      internal static bool GridHasNoSelection(DataGridView? pGrid) {
         if (pGrid == null)
            return true;
         return pGrid.SelectedCells.Count == 0;
      }

      internal static int GridSelectedRowIndexOrMinusOne(DataGridView? pGrid) {
         if (pGrid == null)
            return -1;
         if (pGrid.SelectedCells.Count == 0)
            return -1;
         return pGrid.SelectedCells[0].RowIndex;
      }

      internal static int GridSelectedColumnIndexOrMinusOne(DataGridView? pGrid) {
         if (pGrid == null)
            return -1;
         if (pGrid.SelectedCells.Count == 0)
            return -1;
         return pGrid.SelectedCells[0].ColumnIndex;
      }

      internal static object? GridSelectedValueOrNull(DataGridView? pGrid) {
         if (pGrid == null)
            return null;
         if (pGrid.SelectedCells.Count == 0)
            return null;
         return pGrid.SelectedCells[0].Value;
      }

      internal static void GridSelectCell(DataGridView? pGrid, int pRow, int pColumn) {
         if (pGrid == null)
            return;
         if (pRow < 0)
            return;
         if (pColumn < 0)
            return;
         if (pRow >= pGrid.Rows.Count)
            return;
         if (pColumn >= pGrid.Columns.Count)
            return;
         pGrid.ClearSelection();
         pGrid.Rows[pRow].Cells[pColumn].Selected = true;
      }

      internal static void GridEnable(DataGridView? pGrid) {
         if (pGrid == null)
            return;
         pGrid.Enabled = true;
      }

      internal static void GridDisable(DataGridView? pGrid) {
         if (pGrid == null)
            return;
         pGrid.Enabled = false;
      }

      internal static bool GridIsEnabled(DataGridView? pGrid) {
         if (pGrid == null)
            return false;
         return pGrid.Enabled;
      }

      internal static bool GridIsDisabled(DataGridView? pGrid) {
         if (pGrid == null)
            return true;
         return !pGrid.Enabled;
      }

      internal static void GridShow(DataGridView? pGrid) {
         if (pGrid == null)
            return;
         pGrid.Visible = true;
      }

      internal static void GridHide(DataGridView? pGrid) {
         if (pGrid == null)
            return;
         pGrid.Visible = false;
      }

      internal static bool GridIsVisible(DataGridView? pGrid) {
         if (pGrid == null)
            return false;
         return pGrid.Visible;
      }

      internal static bool GridIsHidden(DataGridView? pGrid) {
         if (pGrid == null)
            return true;
         return !pGrid.Visible;
      }

      internal static void GridSetForeColor(DataGridView? pGrid, Color pColor) {
         if (pGrid == null)
            return;
         pGrid.ForeColor = pColor;
      }

      internal static void GridSetBackColor(DataGridView? pGrid, Color pColor) {
         if (pGrid == null)
            return;
         pGrid.BackgroundColor = pColor;
      }

      internal static void GridSetRowHeadersVisible(DataGridView? pGrid, bool pVisible) {
         if (pGrid == null)
            return;
         pGrid.RowHeadersVisible = pVisible;
      }

      internal static void GridSetColumnHeadersVisible(DataGridView? pGrid, bool pVisible) {
         if (pGrid == null)
            return;
         pGrid.ColumnHeadersVisible = pVisible;
      }

      internal static void GridAutoSizeColumns(DataGridView? pGrid, DataGridViewAutoSizeColumnsMode pMode) {
         if (pGrid == null)
            return;
         pGrid.AutoSizeColumnsMode = pMode;
      }

      internal static void GridAutoSizeRows(DataGridView? pGrid, DataGridViewAutoSizeRowsMode pMode) {
         if (pGrid == null)
            return;
         pGrid.AutoSizeRowsMode = pMode;
      }

      internal static void GridSetReadOnly(DataGridView? pGrid, bool pReadOnly) {
         if (pGrid == null)
            return;
         pGrid.ReadOnly = pReadOnly;
      }

      internal static bool GridIsReadOnly(DataGridView? pGrid) {
         if (pGrid == null)
            return false;
         return pGrid.ReadOnly;
      }
   }
}
