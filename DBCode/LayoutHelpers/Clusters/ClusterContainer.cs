namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class ClusterContainer : Panel, IEnumerable<BaseCluster> {
         private readonly List<BaseCluster> mClusters;
         private readonly ClusterLayoutMode mLayoutMode;
         private readonly int mFixedColumns, mFixedRows, mMaxHeight, mMaxWidth;

         internal ClusterContainer(List<BaseCluster> pClusters, ClusterLayoutMode pLayoutMode, int pMaxWidth = 0,
            int pMaxHeight = 0, int pFixedColumns = 0, int pFixedRows = 0) {
            AutoSize = true;//DEBUG efm5 2026 04 25 testing
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
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

         //DEBUG efm5 2026 04 25 testing
         //public void SetSize() {
         //   Point wantedSize = BottomRight(Controls.Cast<Control>());

         //   if (Dock == DockStyle.None) {
         //      Width = wantedSize.X + 2;
         //      Height = wantedSize.Y + 2;
         //   }
         //   else {
         //      MinimumSize = new Size(wantedSize.X + 2, wantedSize.Y + 2);
         //      AutoScrollMinSize = new Size(wantedSize.X + 2, wantedSize.Y + 2);
         //   }
         //}

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

         private int ResolveContainerWidth() {
            int width = (mMaxWidth > 0) ? mMaxWidth : ClientSize.Width;
            if (width <= 0)
               width = (Width > 0) ? Width : 1;
            return width;
         }

         private int ResolveContainerHeight() {
            int height = (mMaxHeight > 0) ? mMaxHeight : ClientSize.Height;
            if (height <= 0)
               height = (Height > 0) ? Height : 1;
            return height;
         }

         private void LayoutFlow() {
            int spacing = mEm;
            int containerWidth = ResolveContainerWidth();
            int usableWidth = containerWidth - mIndent - mRightPad;
            if (usableWidth <= mIndent)
               usableWidth = mIndent + 1;
            int x = mIndent;
            int y = 0;
            int rowHeight = 0;
            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width;
               int clusterHeight = currentCluster.Height;
               if (x > mIndent && (x + clusterWidth > usableWidth)) {
                  x = mIndent;
                  y += rowHeight + spacing;
                  rowHeight = 0;
               }
               currentCluster.Left = x;
               currentCluster.Top = y;
               x += clusterWidth + spacing;
               if (clusterHeight > rowHeight)
                  rowHeight = clusterHeight;
            }
            Width = containerWidth;
            Height = y + rowHeight + mBottomPad;
         }

         private void LayoutMaxWidth() {
            int spacing = mEm;
            int containerWidth = ResolveContainerWidth();
            int x = mIndent;
            int y = 0;
            foreach (BaseCluster currentCluster in mClusters) {
               currentCluster.Left = x;
               currentCluster.Top = y;
               y += currentCluster.Height + spacing;
            }
            Width = containerWidth;
            Height = y + mBottomPad;
         }

         private void LayoutMaxHeight() {
            int spacing = mEm;
            int containerHeight = ResolveContainerHeight();
            int x = mIndent;
            int y = 0;
            int columnWidth = 0;
            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width;
               int clusterHeight = currentCluster.Height;
               if (y > 0 && (y + clusterHeight > containerHeight)) {
                  y = 0;
                  x += columnWidth + spacing;
                  columnWidth = 0;
               }
               currentCluster.Left = x;
               currentCluster.Top = y;
               y += clusterHeight + spacing;
               if (clusterWidth > columnWidth)
                  columnWidth = clusterWidth;
            }
            Width = x + columnWidth + mRightPad;
            Height = containerHeight + mBottomPad;
         }

         private void LayoutFixedColumns() {
            int spacing = mEm;
            int columns = (mFixedColumns > 0) ? mFixedColumns : 1;
            int containerWidth = ResolveContainerWidth();
            int x = mIndent;
            int y = 0;
            int columnIndex = 0;
            int rowHeight = 0;
            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width;
               int clusterHeight = currentCluster.Height;
               currentCluster.Left = x;
               currentCluster.Top = y;
               if (clusterHeight > rowHeight)
                  rowHeight = clusterHeight;
               columnIndex++;
               if (columnIndex >= columns) {
                  columnIndex = 0;
                  x = mIndent;
                  y += rowHeight + spacing;
                  rowHeight = 0;
               }
               else
                  x += clusterWidth + spacing;
            }
            Width = containerWidth;
            Height = y + rowHeight + mBottomPad;
         }

         private void LayoutFixedRows() {
            int spacing = mEm;
            int rows = (mFixedRows > 0) ? mFixedRows : 1;
            int containerWidth = ResolveContainerWidth();
            int x = mIndent;
            int y = 0;
            int rowIndex = 0;
            int columnWidth = 0;
            foreach (BaseCluster currentCluster in mClusters) {
               int clusterWidth = currentCluster.Width;
               int clusterHeight = currentCluster.Height;
               currentCluster.Left = x;
               currentCluster.Top = y;
               if (clusterWidth > columnWidth)
                  columnWidth = clusterWidth;
               rowIndex++;
               if (rowIndex >= rows) {
                  rowIndex = 0;
                  y = 0;
                  x += columnWidth + spacing;
                  columnWidth = 0;
               }
               else
                  y += clusterHeight + spacing;
            }
            Width = x + columnWidth + mRightPad;
            Height = y + mBottomPad;
         }

         private void LayoutAutoSquareGrid() {
            int spacing = mEm;
            int count = mClusters.Count;
            if (count == 0) {
               Width = 1;
               Height = 1;
               return;
            }
            int containerWidth = ResolveContainerWidth();
            int columns = (int)Math.Ceiling(Math.Sqrt(count));
            int rows = (int)Math.Ceiling(count / (double)columns);
            int maxClusterWidth = 0;
            int maxClusterHeight = 0;
            foreach (BaseCluster currentCluster in mClusters) {
               if (currentCluster.Width > maxClusterWidth)
                  maxClusterWidth = currentCluster.Width;
               if (currentCluster.Height > maxClusterHeight)
                  maxClusterHeight = currentCluster.Height;
            }
            int cellWidth = maxClusterWidth + spacing;
            int cellHeight = maxClusterHeight + spacing;
            int xStart = mIndent;
            int yStart = 0;
            int x = xStart;
            int y = yStart;
            int index = 0;
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
            Width = containerWidth;
            Height = y + mBottomPad;
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

         public void ArrangeControlsFlow(int pMaxWidth, int pSpacing = 0) {
            List<Control> rowList = [];
            int tooWide = pMaxWidth, top = 0, left = 0;

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
