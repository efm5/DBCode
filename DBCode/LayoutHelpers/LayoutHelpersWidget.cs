namespace DBCode {
   internal static partial class LayoutHelpers {

      internal static void RightAlign(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return;
         int rightmost = 0;
         for (int i = 0; i < pControls.Count; i++) {
            if (pControls[i].Right > rightmost)
               rightmost = pControls[i].Right;
         }
         for (int i = 0; i < pControls.Count; i++) {
            Control control = pControls[i];
            if (control.Right < rightmost)
               control.Left += (rightmost - control.Right);
         }
      }

      internal static void CenterControlHorizontally(Control? pContainer, Control? pChildControl) {
         if (pContainer == null)
            return;
         if (pChildControl == null)
            return;
         if (pContainer.Width < pChildControl.Width) {
            pContainer.Width = pChildControl.Width + 2;
            pChildControl.Left = 1;
            return;
         }
         pChildControl.Left = (pContainer.Width - pChildControl.Width) / 2;
      }

      internal static void CenterControlVertically(Control? pContainer, Control? pChildControl) {
         if (pContainer == null)
            return;
         if (pChildControl == null)
            return;
         if (pContainer.Height < pChildControl.Height) {
            pChildControl.Top = 1;
            return;
         }
         pChildControl.Top = (pContainer.Height - pChildControl.Height) / 2;
      }

      internal static void CenterControl(Control? pContainer, Control? pChildControl) {
         CenterControlHorizontally(pContainer, pChildControl);
         CenterControlVertically(pContainer, pChildControl);
      }

      internal static bool EnsureWindowFitsMonitor(Form? pForm, bool pControlBox = true) {
         if (pForm == null)
            return false;
         Screen formScreen = Screen.FromControl(pForm);
         Rectangle workingArea = formScreen.WorkingArea;
         Size size = pForm.Size;
         int controlBoxSpace = pControlBox ? 4 : 1;
         bool changed = false;

         SizeF titleSize;
         using (Graphics graphics = pForm.CreateGraphics()) {
            titleSize = graphics.MeasureString(pForm.Text, CreateNewFont());
         }
         int wantedTitleWidth = (int)((titleSize.Width * 0.86f) +
            (SystemInformation.CaptionButtonSize.Width * controlBoxSpace));

         if (size.Width < wantedTitleWidth) {
            size.Width = wantedTitleWidth;
            changed = true;
         }
         if (size.Width > workingArea.Width) {
            size.Width = workingArea.Width - 10;
            changed = true;
         }
         if (size.Height > workingArea.Height) {
            size.Height = workingArea.Height - 10;
            changed = true;
         }
         pForm.Size = size;

         int x = pForm.Left;
         int y = pForm.Top;

         if (pForm.Right > workingArea.Right)
            x = workingArea.Right - size.Width - 5;
         if (pForm.Bottom > workingArea.Bottom)
            y = workingArea.Bottom - size.Height - 5;

         pForm.Location = new Point(x, y);

         if (IsOffScreen(pForm))
            pForm.Location = new Point(workingArea.Left + 5, workingArea.Top + 5);

         if (IsPartiallyHidden(pForm)) {
            pForm.Location = new Point(workingArea.Left + 5, workingArea.Top + 5);
            if (pForm.Width > workingArea.Width - 10)
               pForm.Width = workingArea.Width - 10;
            if (pForm.Height > workingArea.Height - 10)
               pForm.Height = workingArea.Height - 10;
            changed = true;
         }
         return changed;
      }

      internal static bool IsOffScreen(Form? pForm) {
         if (pForm == null)
            return true;
         Screen[] screens = Screen.AllScreens;
         for (int i = 0; i < screens.Length; i++) {
            if (screens[i].WorkingArea.Contains(new Point(pForm.Left, pForm.Top)))
               return false;
         }
         return true;
      }

      internal static bool IsPartiallyHidden(Form? pForm) {
         if (pForm == null)
            return true;
         Screen[] screens = Screen.AllScreens;
         for (int i = 0; i < screens.Length; i++) {
            if (screens[i].WorkingArea.Contains(new Point(pForm.Right, pForm.Bottom)))
               return false;
         }
         return true;
      }

      internal static void CenterFormOnMonitor(Form? pForm) {
         if (pForm == null)
            return;
         Screen screen = Screen.FromControl(pForm);
         Rectangle workingArea = screen.WorkingArea;
         pForm.Left = workingArea.X + (workingArea.Width - pForm.Width) / 2;
         pForm.Top = workingArea.Y + (workingArea.Height - pForm.Height) / 2;
      }

      internal static void CenterDialog(Form? pForm, Panel? pPanel, Control? pBorderPanel) {
         if (pForm == null)
            return;
         if (pPanel == null)
            return;
         if (pBorderPanel == null)
            return;

         Size wantedSize = new Size(
            (int)(pBorderPanel.Width * 1.2f),
            (int)(pBorderPanel.Height * 1.2f) + 30);

         bool resize = false;

         if (pForm.Width < wantedSize.Width) {
            pForm.Width = wantedSize.Width;
            resize = true;
         }
         if (pForm.Height < wantedSize.Height) {
            pForm.Height = wantedSize.Height;
            resize = true;
         }

         if (resize)
            EnsureWindowFitsMonitor(pForm);

         CenterControl(pForm, pBorderPanel);
         pBorderPanel.BringToFront();
         pBorderPanel.Show();
         pBorderPanel.Refresh();
      }

      internal static void CenterDialogPanel(Form? pForm, Panel? pPanel, Panel? pBorderPanel) {
         if (pForm == null)
            return;
         if (pPanel == null)
            return;
         if (pBorderPanel == null)
            return;

         Rectangle workingArea = Screen.GetWorkingArea(pForm);
         int maxWidth = (int)(workingArea.Width * 0.99f);
         int maxHeight = (int)(workingArea.Height * 0.99f);

         pBorderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
         pPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;

         if (pBorderPanel.Controls.Contains(pPanel))
            pBorderPanel.Controls.Remove(pPanel);
         if (pForm.Controls.Contains(pBorderPanel))
            pForm.Controls.Remove(pBorderPanel);

         pBorderPanel.Size = new Size(
            pPanel.Width + 10,
            pPanel.Height + 10);

         pForm.Controls.Add(pBorderPanel);
         pBorderPanel.Controls.Add(pPanel);

         pPanel.Location = new Point(5, 5);
         pBorderPanel.Location = new Point(5, 5);

         pForm.Size = new Size(
            pBorderPanel.Width + 20,
            pBorderPanel.Height + 40);

         bool resize = false;
         int proposedWidth = pForm.Width;
         int proposedHeight = pForm.Height;

         if (pForm.Width > maxWidth) {
            proposedWidth = maxWidth;
            resize = true;
         }
         if (pForm.Height > maxHeight) {
            proposedHeight = maxHeight;
            resize = true;
         }

         if (resize) {
            pForm.Size = new Size(proposedWidth, proposedHeight);
            EnsureWindowFitsMonitor(pForm);
            pBorderPanel.Size = new Size(
               pForm.ClientSize.Width - pBorderPanel.Left - 5,
               pForm.ClientSize.Height - pBorderPanel.Top - 5);
            pPanel.Size = new Size(
               pBorderPanel.Width - 10,
               pBorderPanel.Height - 10);
         }

         pBorderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
         pPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

         CenterFormOnMonitor(pForm);
         pBorderPanel.BringToFront();
         pBorderPanel.Show();
         pBorderPanel.Refresh();
      }

      internal static void AdjustForResolution(Form? pForm) {
         if (pForm == null)
            return;

         Screen screen = Screen.FromControl(pForm);
         int scaling = (int)Math.Ceiling((float)screen.Bounds.Width / 96f * 100f);

         int tall = 600;
         int wide = 1040;

         if (scaling >= 125 && scaling < 150) {
            tall = 720;
            wide = 1140;
         }
         else if (scaling >= 150 && scaling < 175) {
            tall = 950;
            wide = 1310;
         }
         else if (scaling >= 175 && scaling < 200) {
            tall = 1020;
            wide = 1560;
         }
         else if (scaling >= 200 && scaling < 225) {
            tall = 1170;
            wide = 1760;
         }
         else if (scaling >= 225 && scaling < 250) {
            tall = 1320;
            wide = 1940;
         }
         else if (scaling >= 250 && scaling < 275) {
            tall = 1480;
            wide = 2190;
         }
         else if (scaling >= 275 && scaling < 300) {
            tall = 1580;
            wide = 2690;
         }
         else if (scaling >= 300 && scaling < 325) {
            tall = 1740;
            wide = 2920;
         }
         else if (scaling >= 325 && scaling < 350) {
            tall = 1900;
            wide = 2800;
         }
         else if (scaling >= 350 && scaling < 375) {
            tall = 1650;
            wide = 3020;
         }
         else if (scaling >= 375 && scaling < 400) {
            tall = 2080;
            wide = 3250;
         }
         else if (scaling >= 400) {
            tall = 2500;
            wide = 3490;
         }

         pForm.Size = new Size(wide, tall);
         CenterFormOnMonitor(pForm);
      }
   }
}
