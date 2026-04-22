namespace DBCode.Themes {
   public static class ThemeBinder {
      public static void ApplyTheme(Control pControl) => ApplyTheme(pControl, true, null);

      public static void ApplyTheme(Control pControl, bool pRecurse) => ApplyTheme(pControl, pRecurse, null);

      public static void ApplyTheme(Control pControl, List<Control>? pExclusions) => ApplyTheme(pControl, true, pExclusions);

      public static void ApplyThemeToControl(Control pControl) => ApplyThemeInternal(pControl, GetTheme());

      public static void ApplyThemeToControls(List<Control> pControls) {
         Theme theme = GetTheme();
         foreach (Control control in pControls)
            ApplyThemeInternal(control, theme);
      }

      public static void ApplyTheme(Control pControl, bool pRecurse, List<Control>? pExclusions) {
         if (pControl == null)
            return;
         if (pExclusions != null && pExclusions.Contains(pControl))
            return;
         Theme theme = GetTheme();

         ApplyThemeInternal(pControl, theme);
         if (pRecurse) {
            foreach (Control child in pControl.Controls) {
               if (pExclusions != null && pExclusions.Contains(child))
                  continue;
               ApplyTheme(child, true, pExclusions);
            }
         }
         if (pControl is MenuStrip menuStrip)
            ApplyMenuStrip(menuStrip, theme, pExclusions);
         if (pControl is ContextMenuStrip contextMenu)
            ApplyContextMenu(contextMenu, theme, pExclusions);
         if (pControl is ToolStrip toolStrip)
            ApplyToolStrip(toolStrip, theme, pExclusions);
      }

      private static Theme GetTheme() => ThemeRegistry.GetCurrentThemeClone();

      private static void ApplyThemeInternal(Control pControl, Theme pTheme) {
         ThemeTag? tag = pControl.Tag as ThemeTag;
         if (tag == null) {
            string name = pControl.Name ?? "<no name>";
            string text = pControl.Text ?? "<no text>";
            throw new Exception($"Control '{name}' ('{text}') is missing a ThemeTag.");
         }

         Font font = pTheme.mFonts[(int)tag.mFontUsage];
         Color foreColor = pTheme.mInterfaceColors[(int)tag.mForeColorUsage];
         Color backColor = pTheme.mInterfaceColors[(int)tag.mBackColorUsage];

         pControl.Font = font;
         pControl.ForeColor = foreColor;
         if (tag.mAllowTransparent && backColor == Color.Transparent)
            pControl.BackColor = Color.Transparent;
         else
            pControl.BackColor = backColor;
      }

      private static void ApplyMenuStrip(MenuStrip pMenu, Theme pTheme, List<Control>? pExclusions) {
         foreach (ToolStripMenuItem item in pMenu.Items)
            ApplyMenuItem(item, pTheme, pExclusions);
      }

      private static void ApplyContextMenu(ContextMenuStrip pMenu, Theme pTheme, List<Control>? pExclusions) {
         foreach (ToolStripItem item in pMenu.Items) {
            if (item is ToolStripMenuItem menuItem)
               ApplyMenuItem(menuItem, pTheme, pExclusions);
         }
      }

      private static void ApplyToolStrip(ToolStrip pStrip, Theme pTheme, List<Control>? pExclusions) {
         foreach (ToolStripItem item in pStrip.Items) {
            if (item is ToolStripMenuItem menuItem)
               ApplyMenuItem(menuItem, pTheme, pExclusions);
            else
               ApplyToolStripItem(item, pTheme);
         }
      }

      private static void ApplyMenuItem(ToolStripMenuItem pItem, Theme pTheme, List<Control>? pExclusions) {
         ThemeTag? tag = pItem.Tag as ThemeTag;
         if (tag == null) {
            string name = pItem.Name ?? "<no name>";
            string text = pItem.Text ?? "<no text>";
            throw new Exception($"Menu item '{name}' ('{text}') is missing a ThemeTag.");
         }
         Font font = pTheme.mFonts[(int)tag.mFontUsage];
         Color foreColor = pTheme.mInterfaceColors[(int)tag.mForeColorUsage];
         Color backColor = pTheme.mInterfaceColors[(int)tag.mBackColorUsage];

         pItem.Font = font;
         pItem.ForeColor = foreColor;
         pItem.BackColor = backColor;
         foreach (ToolStripItem child in pItem.DropDownItems) {
            if (child is ToolStripMenuItem childItem)
               ApplyMenuItem(childItem, pTheme, pExclusions);
         }
      }

      private static void ApplyToolStripItem(ToolStripItem pItem, Theme pTheme) {
         ThemeTag? tag = pItem.Tag as ThemeTag;
         if (tag == null) {
            string name = pItem.Name ?? "<no name>";
            string text = pItem.Text ?? "<no text>";
            throw new Exception($"ToolStrip item '{name}' ('{text}') is missing a ThemeTag.");
         }
         Font font = pTheme.mFonts[(int)tag.mFontUsage];
         Color foreColor = pTheme.mInterfaceColors[(int)tag.mForeColorUsage];
         Color backColor = pTheme.mInterfaceColors[(int)tag.mBackColorUsage];

         pItem.Font = font;
         pItem.ForeColor = foreColor;
         pItem.BackColor = backColor;
      }
   }
}
