namespace DBCode {
   public class VariableWidthTabControl : TabControl {
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

      public void RecalculateItemSize(Font pFont) {
         if (TabCount == 0 || !IsHandleCreated)
            return;
         FontFamily family = pFont.FontFamily;
         float emHeight = family.GetEmHeight(pFont.Style);
         float ascent = family.GetCellAscent(pFont.Style);
         float descent = family.GetCellDescent(pFont.Style);
         float cellHeight = pFont.Size * (ascent + descent) / emHeight;
         int tabHeight = (int)Math.Ceiling(cellHeight) + 12;
         SendMessage(Handle, TCM_SETPADDING, 0, MakeLParam(20, 0));
         if (ItemSize.Height != tabHeight)
            ItemSize = new Size(ItemSize.Width, tabHeight);
         TabHeaderWidths.Clear();
         for (int i = 0; i < TabCount; i++)
            TabHeaderWidths.Add(GetTabRect(i).Width);
         ResetStripBackgroundPainted();
         Invalidate();
      }

      protected override void OnHandleCreated(EventArgs pEventArgs) {
         base.OnHandleCreated(pEventArgs);
         RecalculateItemSize(Font ?? SystemFonts.DefaultFont);
      }

      protected override void OnFontChanged(EventArgs pEventArgs) {
         base.OnFontChanged(pEventArgs);
         RecalculateItemSize(Font ?? SystemFonts.DefaultFont);
      }

      protected override void OnSelectedIndexChanged(EventArgs pEventArgs) {
         mAllowStripPainting = false;
         base.OnSelectedIndexChanged(pEventArgs);
      }

      protected override void WndProc(ref Message pMessage) {
         base.WndProc(ref pMessage);
         if (pMessage.Msg == 0x000F && mAllowStripPainting && !mStripBackgroundPainted && TabCount > 0 && DrawMode == TabDrawMode.OwnerDrawFixed) {
            using (Graphics g = CreateGraphics()) {
               Rectangle displayRect = DisplayRectangle;
               Rectangle stripRect = new Rectangle(0, 0, Width, displayRect.Top);
               using SolidBrush brush = new SolidBrush(mCapturedBackColor);
               g.FillRectangle(brush, stripRect);
            }
            mStripBackgroundPainted = true;
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
         if (TabCount == 0)
            return -1;
         int currentOffset = GetTabRect(0).X; // account for native left margin
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
