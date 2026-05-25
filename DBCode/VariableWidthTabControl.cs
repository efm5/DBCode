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

      public int GetTabStripHeight(int pAvailableWidth) {
         Font font = Font ?? SystemFonts.DefaultFont;
         int singleRowHeight = ItemSize.Height;
         if (singleRowHeight == 0) {
            FontFamily family = font.FontFamily;
            float emHeight = family.GetEmHeight(font.Style);
            float ascent = family.GetCellAscent(font.Style);
            float descent = family.GetCellDescent(font.Style);
            singleRowHeight = (int)Math.Ceiling(font.Size * (ascent + descent) / emHeight) + 12;
         }
         if (pAvailableWidth <= 0 || TabCount == 0)
            return singleRowHeight;
         List<int> widths = TabHeaderWidths.Count > 0
            ? TabHeaderWidths
            : ComputeTabWidths(font);
         int rows = 1;
         int rowWidth = 0;
         for (int i = 0; i < widths.Count; i++) {
            rowWidth += widths[i];
            if (rowWidth > pAvailableWidth) {
               rows++;
               rowWidth = widths[i];
            }
         }
         return rows * singleRowHeight;
      }

      private List<int> ComputeTabWidths(Font pFont) {
         List<int> widths = [];
         for (int i = 0; i < TabCount; i++)
            widths.Add(TextRenderer.MeasureText(TabPages[i].Text, pFont).Width + 40);
         return widths;
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
