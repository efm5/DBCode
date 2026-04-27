namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class ClusterContainer : Panel, IEnumerable<BaseCluster> {
         private readonly List<BaseCluster> mClusters;
         private readonly ClusterLayoutMode mLayoutMode;
         private readonly int mFixedColumns, mFixedRows, mMaxHeight, mMaxWidth;
         private Panel mPanelParent;

         internal ClusterContainer(Panel pParent, List<BaseCluster> pClusters, ClusterLayoutMode pLayoutMode, int pMaxWidth = 0,
            int pMaxHeight = 0, int pFixedColumns = 0, int pFixedRows = 0) {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mPanelParent = pParent;
            mClusters = pClusters;
            mLayoutMode = pLayoutMode;
            mMaxWidth = pMaxWidth;
            mMaxHeight = pMaxHeight;
            mFixedColumns = pFixedColumns;
            mFixedRows = pFixedRows;
            foreach (BaseCluster currentCluster in mClusters)
               Controls.Add(currentCluster);
         }

         public IEnumerator<BaseCluster> GetEnumerator() {
            return mClusters.GetEnumerator();
         }

         System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
         }

         public void LayoutClusters() {
            switch (mLayoutMode) {
               default:
               case ClusterLayoutMode.FlowLayout:
                  LayoutFlow();
                  break;
               case ClusterLayoutMode.MaxWidth:
                  LayoutMaxWidth();
                  break;
               case ClusterLayoutMode.MaxHeight:
                  LayoutMaxHeight();
                  break;
               case ClusterLayoutMode.FixedColumns:
                  LayoutFixedColumns();
                  break;
               case ClusterLayoutMode.FixedRows:
                  LayoutFixedRows();
                  break;
               case ClusterLayoutMode.AutoSquareGrid:
                  LayoutAutoSquareGrid();
                  break;
            }
         }

         private void LayoutFlow() {
            int usableWidth = mPanelParent.ClientSize.Width - mEm - mRightPad, x = mIndent, y = 0, rowHeight = 0;

            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width, clusterHeight = currentCluster.Height;

               if (x > mIndent && (x + clusterWidth > usableWidth)) {
                  x = mIndent;
                  y += rowHeight + mBottomPad;
                  rowHeight = 0;
               }
               currentCluster.Left = x;
               currentCluster.Top = y;
               x += clusterWidth + mEm;
               if (clusterHeight > rowHeight)
                  rowHeight = clusterHeight;
            }
         }

         private void LayoutMaxWidth() {
            int x = mIndent, y = 0;

            foreach (BaseCluster currentCluster in mClusters) {
               currentCluster.Left = x;
               currentCluster.Top = y;
               y += currentCluster.Height + mEm;
            }
         }

         private void LayoutMaxHeight() {
            int x = mIndent, y = 0, columnWidth = 0;

            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width, clusterHeight = currentCluster.Height;

               currentCluster.Left = x;
               currentCluster.Top = y;
               y += clusterHeight + mEm;
               if (clusterWidth > columnWidth)
                  columnWidth = clusterWidth;
            }
         }

         private void LayoutFixedColumns() {
            int columns = (mFixedColumns > 0) ? mFixedColumns : 1, x = mIndent, y = 0;

            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width, clusterHeight = currentCluster.Height, rowHeight = 0, columnIndex = 0;

               currentCluster.Left = x;
               currentCluster.Top = y;
               if (clusterHeight > rowHeight)
                  rowHeight = clusterHeight;
               columnIndex++;
               if (columnIndex >= columns) {
                  columnIndex = 0;
                  x = mIndent;
                  y += rowHeight + mBottomPad;
                  rowHeight = 0;
               }
               else
                  x += clusterWidth + mEm;
            }
         }

         private void LayoutFixedRows() {
            int rows = (mFixedRows > 0) ? mFixedRows : 1, x = mIndent, y = 0, rowIndex = 0, columnWidth = 0;

            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width, clusterHeight = currentCluster.Height;

               currentCluster.Left = x;
               currentCluster.Top = y;
               if (clusterWidth > columnWidth)
                  columnWidth = clusterWidth;
               rowIndex++;
               if (rowIndex >= rows) {
                  rowIndex = 0;
                  y = 0;
                  x += columnWidth + mEm;
                  columnWidth = 0;
               }
               else
                  y += clusterHeight + mBottomPad;
            }
         }

         private void LayoutAutoSquareGrid() {
            int count = mClusters.Count, columns = (int)Math.Ceiling(Math.Sqrt(count)),
               rows = (int)Math.Ceiling(count / (double)columns), maxClusterWidth = 0, maxClusterHeight = 0;

            foreach (BaseCluster currentCluster in mClusters) {
               if (currentCluster.Width > maxClusterWidth)
                  maxClusterWidth = currentCluster.Width;
               if (currentCluster.Height > maxClusterHeight)
                  maxClusterHeight = currentCluster.Height;
            }
            int cellWidth = maxClusterWidth + mEm, cellHeight = maxClusterHeight + mEm, xStart = mIndent, yStart = 0,
               x = xStart, y = yStart, index = 0;

            for (int currentRow = 0; currentRow < rows; currentRow++) {
               x = xStart;
               for (int currentColumn = 0; currentColumn < columns; currentColumn++) {
                  if (index >= count)
                     break;
                  BaseCluster currentCluster = mClusters[index];
                  currentCluster.Left = x;
                  currentCluster.Top = y;
                  x += cellWidth;
                  index++;
               }
               y += cellHeight;
            }
         }

         public void ArrangeControlsInRows(int pSpacing = 0) {
            int y = 0;

            foreach (Control control in Controls) {
               control.Location = new Point(0, y);
               y += control.Height + pSpacing;
            }
         }

         public void ArrangeControlsInColumns(int pSpacing = 0) {
            int x = 0;

            foreach (Control control in Controls) {
               control.Location = new Point(x, 0);
               x += control.Width + pSpacing;
            }
         }

         public void ArrangeControlsInGrid(int pColumns, int pRows, int pSpacing = 0) {
            if (pColumns <= 0)
               throw new ArgumentException("Number of columns must be greater than zero.", nameof(pColumns));
            if (pRows <= 0)
               throw new ArgumentException("Number of rows must be greater than zero.", nameof(pRows));
            int controlCount = Controls.Count;
            int requiredRows = (int)Math.Ceiling((double)controlCount / pColumns);
            if (requiredRows > pRows)
               pRows = requiredRows;
            int left = 0, top = 0, columnIndex = 0, rowIndex = 0;
            List<int> columnWidths = [];
            List<int> rowHeights = [];
            for (int i = 0; i < pColumns; i++)
               columnWidths.Add(0);
            for (int i = 0; i < pRows; i++)
               rowHeights.Add(0);
            foreach (Control control in Controls) {
               if (columnIndex >= pColumns) {
                  columnIndex = 0;
                  rowIndex++;
               }
               if (control.Width > columnWidths[columnIndex])
                  columnWidths[columnIndex] = control.Width;
               if (control.Height > rowHeights[rowIndex])
                  rowHeights[rowIndex] = control.Height;
               columnIndex++;
            }
            columnIndex = 0;
            rowIndex = 0;
            left = 0;
            top = 0;
            foreach (Control control in Controls) {
               if (columnIndex >= pColumns) {
                  columnIndex = 0;
                  rowIndex++;
                  left = 0;
                  top += rowHeights[rowIndex - 1] + pSpacing;
               }
               control.Location = new Point(left, top);
               left += columnWidths[columnIndex] + pSpacing;
               columnIndex++;
            }
         }

         public void ArrangeControlsFlow(int pSpacing = 0) {
            List<Control> rowList = [];
            int tooWide = mPanelParent.ClientSize.Width, top = 0, left = 0;

            for (int i = 0; i < Controls.Count; i++) {
               Controls[i].Location = new Point(left, top);
               left = Controls[i].Right + pSpacing;
               if (left > tooWide) {
                  top = Bottommost(rowList)!.Bottom + mEmHalf;
                  rowList.Clear();
                  Controls[i].Location = new Point(Controls[0].Left, top);
                  left = Controls[i].Right + pSpacing;
               }
               rowList.Add(Controls[i]);
            }
         }
      }
   }
}
