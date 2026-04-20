namespace DBCode {
   namespace Themes {
      internal sealed class VariableWidthTabControl : TabControl {
         public readonly List<int> TabHeaderWidths = [];

         protected override void OnMouseDown(MouseEventArgs pMouseEventArgs) {
            int tabIndex = HitTestTabHeaders(pMouseEventArgs.Location);
            if (tabIndex >= 0)
               SelectedIndex = tabIndex;
            else
               base.OnMouseDown(pMouseEventArgs);
         }

         private int HitTestTabHeaders(Point pMouseLocation) {
            int currentOffset = 0;
            int tabHeaderTop = GetTabRect(0).Y;
            for (int index = 0; index < TabHeaderWidths.Count; index++) {
               int width = TabHeaderWidths[index];
               Rectangle rect = new Rectangle(currentOffset, tabHeaderTop, width, ItemSize.Height);
               if (rect.Contains(pMouseLocation))
                  return index;
               currentOffset += width;
            }
            return -1;
         }
      }
   }
}
