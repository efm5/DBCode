namespace DBCode {
   public sealed partial class MainForm : Form {
      private void EnsureThemePanel() {
         if (mThemePanel != null)
            return;
         mThemePanel = new Panel();
         mThemeCloseButton = new Button();
         mThemePanel.Size = new Size(1000, 800);
         mThemePanel.BackColor = SystemColors.ControlDark;
         mThemePanel.Name = "preferencesPanel";
         mThemeCloseButton.Text = "&Close";
         mThemeCloseButton.AutoSize = true;
         mThemeCloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         mThemeCloseButton.Name = "preferencesCloseButton";
         mThemeCloseButton.Click += ThemeCloseButton_Click;
         mThemePanel.Controls.Add(mThemeCloseButton);
         mThemePanel.Layout += ThemePanel_Layout;
      }

      private void ShowThemePanel() {
         EnsureThemePanel();
         if (mThemePanel == null)
            return;
         Rectangle screenBounds = Screen.FromControl(this).WorkingArea;
         int newWidth = mThemePanel.Width;
         int newHeight = mThemePanel.Height;
         int newX = screenBounds.Left + (screenBounds.Width - newWidth) / 2;
         int newY = screenBounds.Top + (screenBounds.Height - newHeight) / 2;

         mPreThemeBounds = Bounds;
         Bounds = new Rectangle(newX, newY, newWidth, newHeight);
         if (Controls.Contains(mMenuStrip))
            Controls.Remove(mMenuStrip);
         if (Controls.Contains(mRichTextBox))
            Controls.Remove(mRichTextBox);
         if (Controls.Contains(mStatusStrip))
            Controls.Remove(mStatusStrip);
         if (!Controls.Contains(mThemePanel))
            Controls.Add(mThemePanel);

         mThemePanel.Visible = true;
         mThemePanel.BringToFront();
      }

      private void RestoreFromThemePanel() {
         Bounds = mPreThemeBounds;

         if (mThemePanel != null) {
            mThemePanel.Visible = false;
            mThemePanel.SendToBack();
            if (Controls.Contains(mThemePanel))
               Controls.Remove(mThemePanel);
         }

         if (!Controls.Contains(mRichTextBox))
            Controls.Add(mRichTextBox);
         if (!Controls.Contains(mStatusStrip))
            Controls.Add(mStatusStrip);
         if (mCurrentViewMode == ViewMode.Features && !Controls.Contains(mMenuStrip))
            Controls.Add(mMenuStrip);

         mRichTextBox?.Visible = true;
         mStatusStrip?.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip?.Visible = true;
      }

      public static bool IsKnownColor(Color pColor) {
         Color color;

         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            //cast the colorName into a KnownColor
            if (!Enum.TryParse<KnownColor>(colorName, out KnownColor oKnownColor))
               continue;
            //check if the knownColor variable is a System color - 
            if (oKnownColor > KnownColor.Transparent) {//  Transparent -27- is the highest numbered system color
               color = Color.FromName(colorName);
               if (color == pColor)
                  return true;
            }
         }
         return false;
      }

      public static bool IsKnownColor(string pColorName, out Color pOColor) {
         pOColor = Color.Transparent;
         List<string> colors = [];

         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            //cast the colorName into a KnownColor
            if (!Enum.TryParse<KnownColor>(colorName, out KnownColor oKnownColor))
               continue;
            //check if the knownColor variable is a System color
            if (oKnownColor > KnownColor.Transparent) //  Transparent -27- is the highest numbered system color
               colors.Add(colorName);
         }
         if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase)) {
            pOColor = Color.FromName(pColorName);
            return true;
         }
         return false;
      }

      public static bool IsKnownColor(string pColorName) {
         List<string> colors = [];

         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            //cast the colorName into a KnownColor
            if (!Enum.TryParse<KnownColor>(colorName, out KnownColor oKnownColor))
               continue;
            //check if the knownColor variable is a System color
            if (oKnownColor > KnownColor.Transparent)
               colors.Add(colorName);
         }
         if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase))
            return true;
         return false;
      }
   }
}
