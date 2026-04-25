namespace DBCode {
   namespace Themes {
      internal sealed class VariableWidthTabControl : TabControl {
         public readonly List<int> TabHeaderWidths = [];
         private bool mStripBackgroundPainted = false;
         private bool mAllowStripPainting = true;
         private Color mCapturedBackColor = SystemColors.Control;

         public void SetStripBackColor(Color pColor) {
            mCapturedBackColor = pColor;
         }

         public void ResetStripBackgroundPainted() {
            mStripBackgroundPainted = false;
            mAllowStripPainting = true;
         }

         protected override void OnSelectedIndexChanged(EventArgs pEventArgs) {
            mAllowStripPainting = false;
            base.OnSelectedIndexChanged(pEventArgs);
         }

         protected override void WndProc(ref Message pMessage) {
            base.WndProc(ref pMessage);

            // WM_PAINT = 0x000F - Paint strip AFTER base processes the message
            if (pMessage.Msg == 0x000F && mAllowStripPainting && !mStripBackgroundPainted && TabCount > 0 && DrawMode == TabDrawMode.OwnerDrawFixed) {
               using (Graphics g = CreateGraphics()) {
                  Rectangle displayRect = DisplayRectangle;
                  Rectangle stripRect = new Rectangle(0, 0, Width, displayRect.Top);
                  using SolidBrush brush = new SolidBrush(mCapturedBackColor);
                  g.FillRectangle(brush, stripRect);
               }
               mStripBackgroundPainted = true;

               // Invalidate the tab header rectangles so they repaint on top of our strip background
               for (int i = 0; i < TabCount; i++) {
                  Rectangle tabRect = GetTabRect(i);
                  Invalidate(tabRect);
               }
            }
         }

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
