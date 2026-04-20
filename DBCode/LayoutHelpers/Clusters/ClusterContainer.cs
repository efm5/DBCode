namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class ClusterContainer : Panel {
         private readonly List<BaseCluster> mClusters;
         private readonly ClusterLayoutMode mLayoutMode;
         private readonly int mFixedColumns, mFixedRows, mMaxHeight, mMaxWidth;

         internal ClusterContainer(List<BaseCluster> pClusters, ClusterLayoutMode pLayoutMode, int pMaxWidth = 0,
            int pMaxHeight = 0, int pFixedColumns = 0, int pFixedRows = 0) {
            mClusters = pClusters;
            mLayoutMode = pLayoutMode;
            mMaxWidth = pMaxWidth;
            mMaxHeight = pMaxHeight;
            mFixedColumns = pFixedColumns;
            mFixedRows = pFixedRows;
            foreach (BaseCluster currentCluster in mClusters)
               Controls.Add(currentCluster);
         }

         protected override void OnLayout(LayoutEventArgs pLayoutEventArgs) {
            base.OnLayout(pLayoutEventArgs);
            LayoutClusters();
         }

         public void LayoutClusters() {
            switch (mLayoutMode) {
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
               default:
                  LayoutFlow();
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
      }
   }
}
