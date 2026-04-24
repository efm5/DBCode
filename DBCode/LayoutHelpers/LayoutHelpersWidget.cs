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

      internal static int WidgetLayout(List<Control> pControlList, int pMaxWidth, int pTop = 10, int pLeft = 10) {
         if ((pControlList == null) || (pControlList.Count == 0))
            return 0;
         List<Control> rowList = [];
         int tooWide = pMaxWidth, top = pTop, left = pLeft, bottom = pControlList[0].Bottom;

         for (int i = 0; i < pControlList.Count; i++) {
            pControlList[i].Location = new Point(left, top);
            if (pControlList[i].Bottom > bottom)
               bottom = pControlList[i].Bottom;
            left = pControlList[i].Right + mEm;
            if (left > tooWide) {
               top = Bottommost(rowList)!.Bottom + mEmHalf;
               rowList.Clear();
               pControlList[i].Location = new Point(pControlList[0].Left, top);
               left = pControlList[i].Right + mEm;
               bottom = pControlList[i].Bottom;
            }
            rowList.Add(pControlList[i]);
         }
         return bottom;
      }

      internal static void PaintMenuItem(ToolStripMenuItem pTSMI) {
         pTSMI.Font = CreateMenuFont();
         pTSMI.ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.MenuFont];
         pTSMI.BackColor = mCurrentTheme.mInterfaceColors[(int)ColorUsage.MenuBackground];
      }

      internal static void PaintMenuItemsRecursive(ToolStripMenuItem pTSMI) {
         pTSMI.Font = CreateMenuFont();
         pTSMI.ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.MenuFont];
         pTSMI.BackColor = mCurrentTheme.mInterfaceColors[(int)ColorUsage.MenuBackground];
         foreach (ToolStripMenuItem tsmi in pTSMI.DropDownItems.OfType<ToolStripMenuItem>()) {
            tsmi.Font = CreateMenuFont();
            tsmi.ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorUsage.MenuFont];
            tsmi.BackColor = mCurrentTheme.mInterfaceColors[(int)ColorUsage.MenuBackground];
            if (tsmi.DropDownItems.Count > 0)
               PaintMenuItemsRecursive(tsmi);
         }
      }
   }
}
