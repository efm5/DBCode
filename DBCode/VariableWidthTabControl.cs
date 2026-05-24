namespace DBCode {
   public class VariableWidthTabControl : TabControl {
      public readonly List<int> TabHeaderWidths = [];
      private Color mCapturedBackColor = SystemColors.Control;

      public void SetStripBackColor(Color pColor) {
         mCapturedBackColor = pColor;
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
         ItemSize = new Size(ItemSize.Width, tabHeight);
         TabHeaderWidths.Clear();
         for (int i = 0; i < TabCount; i++)
            TabHeaderWidths.Add(GetTabRect(i).Width);
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

      // DrawItem fires inside base.WndProc and paints every tab header; we then fill only
      // the gaps between headers so the themed background shows without overwriting them.
      // This works identically for single-line and multiline — no flag state needed.
      protected override void WndProc(ref Message pMessage) {
         if (pMessage.Msg == 0x0201) {
            int lp = pMessage.LParam.ToInt32();
            int hitTab = HitTestTabHeaders(new Point(lp & 0xFFFF, (lp >> 16) & 0xFFFF));
            if (hitTab >= 0) {
               SelectedIndex = hitTab;
               Focus();
               return;
            }
         }
         base.WndProc(ref pMessage);
         if (pMessage.Msg == 0x000F && TabCount > 0 && DrawMode == TabDrawMode.OwnerDrawFixed) {
            Rectangle stripRect = new Rectangle(0, 0, Width, DisplayRectangle.Top);
            using Graphics g = CreateGraphics();
            using SolidBrush brush = new SolidBrush(mCapturedBackColor);
            using Region stripRegion = new Region(stripRect);
            for (int i = 0; i < TabCount; i++)
               stripRegion.Exclude(GetTabRect(i));
            g.FillRegion(brush, stripRegion);
         }
      }

      private int HitTestTabHeaders(Point pMouseLocation) {
         for (int index = 0; index < TabCount; index++) {
            if (GetTabRect(index).Contains(pMouseLocation))
               return index;
         }
         return -1;
      }
   }
}
