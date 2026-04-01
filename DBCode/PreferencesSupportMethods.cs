namespace DBCode {
   public sealed partial class MainForm : Form {
      private void EnsurePreferencesPanel() {
         if (mPreferencesPanel != null)
            return;
         mPreferencesPanel = new Panel();
         mPreferencesCloseButton = new Button();
         mPreferencesPanel.Size = new Size(1000, 800);
         mPreferencesPanel.BackColor = SystemColors.ControlDark;
         mPreferencesPanel.Name = "preferencesPanel";
         mPreferencesCloseButton.Text = "&Close";
         mPreferencesCloseButton.AutoSize = true;
         mPreferencesCloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         mPreferencesCloseButton.Name = "preferencesCloseButton";
         mPreferencesCloseButton.Click += PreferencesCloseButton_Click;
         mPreferencesPanel.Controls.Add(mPreferencesCloseButton);
         mPreferencesPanel.Layout += PreferencesPanel_Layout;
      }

      private void ShowPreferencesPanel() {
         EnsurePreferencesPanel();
         if (mPreferencesPanel == null)
            return;
         Rectangle screenBounds = Screen.FromControl(this).WorkingArea;
         int newWidth = mPreferencesPanel.Width;
         int newHeight = mPreferencesPanel.Height;
         int newX = screenBounds.Left + (screenBounds.Width - newWidth) / 2;
         int newY = screenBounds.Top + (screenBounds.Height - newHeight) / 2;

         mPrePreferencesBounds = Bounds;
         Bounds = new Rectangle(newX, newY, newWidth, newHeight);
         if (Controls.Contains(mMenuStrip))
            Controls.Remove(mMenuStrip);
         if (Controls.Contains(mRichTextBox))
            Controls.Remove(mRichTextBox);
         if (Controls.Contains(mStatusStrip))
            Controls.Remove(mStatusStrip);
         if (!Controls.Contains(mPreferencesPanel))
            Controls.Add(mPreferencesPanel);

         mPreferencesPanel.Visible = true;
         mPreferencesPanel.BringToFront();
      }

      private void RestoreFromPreferencesPanel() {
         Bounds = mPrePreferencesBounds;

         if (mPreferencesPanel != null) {
            mPreferencesPanel.Visible = false;
            mPreferencesPanel.SendToBack();
            if (Controls.Contains(mPreferencesPanel))
               Controls.Remove(mPreferencesPanel);
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
