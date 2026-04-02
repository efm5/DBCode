namespace DBCode {
   internal class LayoutHelpers {
#pragma warning disable IDE1006
#pragma warning disable IDE0028
      private const int OFFSET = 5, DOUBLE_OFFSET = (OFFSET * 2);
      private const int POST_CLIP_DELAY = 300, SHORT_DELAY = 50, LONG_DELAY = 450, CLIPBOARD_DELAY = 350,
         FIND_WIDTH = 200, WINDOW_REDUCER = 7,
         //efm5 PANEL_BORDER should be evenly divisible by 4
         PANEL_BORDER = 12, MAIN_BORDER = 4, HALF_BORDER = PANEL_BORDER / 2, INSET_BORDER = PANEL_BORDER / 4,
         BORDER = 4, DOUBLE_BORDER = BORDER * 2,
         DETAILS_VERTICAL_PADDING = 1, DETAILS_HORIZONTAL_PADDING = 1, ADDRESS_BAR_PADDING = 30;
#pragma warning disable CS8602
      //DEBUG efm5 2026 03 30 resolve this needing a warning disable
      public static readonly int COMBOBOX_MAXIMUM_DROPDOWN_WIDTH = Screen.PrimaryScreen.WorkingArea.Width - 100;
#pragma warning restore CS8602

      public static int Largest(List<int> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         int largest = pNumbers[0];

         foreach (int number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static float Largest(List<float> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         float largest = pNumbers[0];

         foreach (float number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static double Largest(List<double> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         double largest = pNumbers[0];

         foreach (double number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static int Smallest(List<int> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         int smallest = pNumbers[0];

         foreach (int number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static float Smallest(List<float> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         float smallest = pNumbers[0];

         foreach (float number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static double Smallest(List<double> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         double smallest = pNumbers[0];

         foreach (double number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static void RightAlign(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return;
         int rightmost = Rightmost(pControls);

         foreach (Control control in pControls)
            if (control.Right < rightmost)
               control.Left += (rightmost - control.Right);
      }

      public static int Tallest(List<Control> pControls) {
         int tallest = 0;

         foreach (Control control in pControls)
            if (control.Height > tallest)
               tallest = control.Height;
         return tallest;
      }

      public static int Tallest(Control.ControlCollection pControls) {
         int tallest = 0;

         foreach (Control control in pControls)
            if (control.Height > tallest)
               tallest = control.Height;
         return tallest;
      }

      public static int Shortest(List<Control> pControls) {
         int shortest = 0;

         foreach (Control control in pControls)
            if (control.Height < shortest)
               shortest = control.Height;
         return shortest;
      }

      public static int Shortest(Control.ControlCollection pControls) {
         int shortest = 0;

         foreach (Control control in pControls)
            if (control.Height < shortest)
               shortest = control.Height;
         return shortest;
      }

      public static int Widest(List<Control> pControls) {
         int widest = 0;

         foreach (Control control in pControls)
            if (control.Width > widest)
               widest = control.Width;
         return widest;
      }

      public static int Widest(Control.ControlCollection pControls) {
         int widest = 0;

         foreach (Control control in pControls)
            if (control.Width > widest)
               widest = control.Width;
         return widest;
      }

      public static int Rightmost(List<Control> pControls) {
         int rightmost = 0;

         foreach (Control control in pControls)
            if (control.Right > rightmost)
               rightmost = control.Right;
         return rightmost;
      }

      public int Rightmost(Control.ControlCollection pControls) {
         int rightmost = 0;

         foreach (Control control in pControls)
            if (control.Right > rightmost)
               rightmost = control.Right;
         return rightmost;
      }

      public static int Leftmost(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int leftmost = pControls[0].Left;

         foreach (Control control in pControls)
            if (control.Left < leftmost)
               leftmost = control.Left;
         return leftmost;
      }

      public static int Leftmost(Control.ControlCollection pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int leftmost = pControls[0].Left;

         foreach (Control control in pControls)
            if (control.Left < leftmost)
               leftmost = control.Left;
         return leftmost;
      }

      public static int Topmost(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int topmost = pControls[0].Top;

         foreach (Control control in pControls)
            if (control.Top < topmost)
               topmost = control.Top;
         return topmost;
      }

      public static int Topmost(Control.ControlCollection pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int topmost = pControls[0].Top;

         foreach (Control control in pControls)
            if (control.Top < topmost)
               topmost = control.Top;
         return topmost;
      }

      public static int Bottommost(List<Control> pControls) {
         int bottommost = 0;

         foreach (Control control in pControls)
            if (control.Bottom > bottommost)
               bottommost = control.Bottom;
         return bottommost;
      }

      public static int Bottommost(Control.ControlCollection pControls) {
         int bottommost = 0;

         foreach (Control control in pControls)
            if (control.Bottom > bottommost)
               bottommost = control.Bottom;
         return bottommost;
      }

      private static void SetFontComboBoxWidth(ComboBox? pComboBox) {
         if (pComboBox == null)
            return;
         float boxWidth = 30f, boxHeight = 30f;
         SizeF stringSize = new SizeF();

         using (Graphics graphics = pComboBox.CreateGraphics()) {
            foreach (string fontName in pComboBox.Items) {
               //string fontName = ((FontFamily)item).Name;
               if (!String.IsNullOrEmpty(fontName)) {
                  stringSize = graphics.MeasureString(fontName, pComboBox.Font);
                  if (stringSize.Width > boxWidth)
                     boxWidth = stringSize.Width;
                  if (stringSize.Height > boxHeight)
                     boxHeight = stringSize.Height;
               }
            }
         }
         pComboBox.Width = (int)(boxWidth + SystemInformation.VerticalScrollBarWidth);
         pComboBox.Height = (int)(boxHeight + mIndent);
      }

      private static void SetFontComboBoxDropDownWidth(ComboBox? pComboBox) {
         if (pComboBox == null)
            return;
         if (pComboBox.Items.Count == 0)
            return;//don't change the width
         float boxWidth = 30f;
         SizeF stringSize = new SizeF();

         using (Graphics graphics = pComboBox.CreateGraphics()) {
            foreach (string fontName in pComboBox.Items) {
               //string fontName = ((FontFamily)item).Name;
               if (!String.IsNullOrEmpty(fontName)) {
                  stringSize = graphics.MeasureString(fontName, pComboBox.Font);
                  if (stringSize.Width > boxWidth)
                     boxWidth = stringSize.Width;
               }
            }
         }
         if (boxWidth > pComboBox.Width) {
            if (boxWidth > mComboBoxMaxDropdownWidth)
               boxWidth = mComboBoxMaxDropdownWidth;
            pComboBox.DropDownWidth = (int)boxWidth;
         }
      }

      public static void CenterControlHorizontally(Control? pContainer, Control? pChildControl) {
         if ((pContainer == null) || (pChildControl == null))
            return;
         if (pContainer.Width < pChildControl.Width) {
            pContainer.Width = pChildControl.Width + 2;
            pChildControl.Left = 1;
            TimedMessage(string.Format("CenterControlHorizontally() error:{0}" +
                   "container: {1} is narrower than{0}" +
                   "the control to be centered: {2}",
                   Environment.NewLine, pContainer.Name, pChildControl.Name),
                "Code VIOLATION", 0);
         }
         else
            pChildControl.Left = (pContainer.Width - pChildControl.Width) / 2;
      }

      public static void CenterControlVertically(Control pContainer, Control pChildControl) {
         if (pContainer.Height < pChildControl.Height) {
            pChildControl.Width = pContainer.Height + 2;
            pChildControl.Top = 1;
            TimedMessage(string.Format("CenterControlVertically() error:{0}" +
                   "container: {1} is shorter than{0}" +
                   "the control to be centered: {2}",
                   Environment.NewLine, pContainer.Name, pChildControl.Name),
                "Code VIOLATION", 0);
         }
         else
            pChildControl.Top = (pContainer.Height - pChildControl.Height) / 2;
      }

      public static void CenterControl(Control pContainer, Control pChildControl) {
         CenterControlHorizontally(pContainer, pChildControl);
         CenterControlVertically(pContainer, pChildControl);
      }

      //public void CenterDialog(Panel pPanel, BorderFrame pBorderPanel) {//Floating, Non-modal
      //   //Rectangle workingArea = Screen.GetWorkingArea(this);
      //   //int maxWidth = (int)(workingArea.Width * 0.99f),
      //   //   maxHeight = (int)(workingArea.Height * 0.99f), proposedWidth, proposedHeight;
      //   bool resize = false;
      //   Size wantedSize = new Size(
      //      (int)(pBorderPanel.Width * 1.2f),
      //      (int)(pBorderPanel.Height * 1.2f) + sTitleBarHeight);

      //   pBorderPanel.RemoveAnchors();
      //   pBorderPanel.SizesAndLocations();
      //   if (Width < wantedSize.Width) {
      //      Width = wantedSize.Width;
      //      resize = true;
      //   }
      //   if (Height < wantedSize.Height) {
      //      Height = wantedSize.Height;
      //      resize = true;
      //   }
      //   if (resize) {
      //      if (!EnsureWindowFitsMonitor(this)) {
      //         pBorderPanel.Size = new Size(ClientSize.Width - HALF_BORDER - pBorderPanel.Left,
      //            ClientSize.Height - HALF_BORDER - pBorderPanel.Top);
      //         pPanel.Size = new Size(pBorderPanel.Width - PANEL_BORDER,
      //            pBorderPanel.Height - PANEL_BORDER);
      //      }
      //   }
      //   pBorderPanel.RestoreAnchors();
      //   CenterControl(this, pBorderPanel);
      //   pBorderPanel.BringToFront();
      //   pBorderPanel.Show();
      //   pBorderPanel.Refresh();
      //}

      //public void CenterDialogPanel(Panel pPanel, BorderFrame pBorderPanel) {
      //   Rectangle workingArea = Screen.GetWorkingArea(this);
      //   int maxWidth = (int)(workingArea.Width * 0.99f),
      //      maxHeight = (int)(workingArea.Height * 0.99f), proposedWidth, proposedHeight;
      //   bool resize = false;

      //   pBorderPanel.RemoveAnchors();
      //   pBorderPanel.SizesAndLocations();
      //   Size = new Size(
      //      pBorderPanel.Width + (pBorderPanel.Left * 2) + SystemInformation.VerticalScrollBarWidth,
      //      pBorderPanel.Height + sTitleBarHeight + (pBorderPanel.Top * 2) +
      //         SystemInformation.HorizontalScrollBarHeight);
      //   if (Size.Width > maxWidth) {
      //      proposedWidth = maxWidth;
      //      resize = true;
      //   }
      //   else
      //      proposedWidth = Size.Width;
      //   if (Size.Height > maxHeight) {
      //      proposedHeight = maxHeight;
      //      resize = true;
      //   }
      //   else
      //      proposedHeight = Size.Height;
      //   if (resize) {
      //      Size = new Size(proposedWidth, proposedHeight);
      //      _ = EnsureWindowFitsMonitor(this);
      //      pBorderPanel.Size = new Size(ClientSize.Width - HALF_BORDER - pBorderPanel.Left,
      //         ClientSize.Height - HALF_BORDER - pBorderPanel.Top);
      //      pPanel.Size = new Size(pBorderPanel.Width - PANEL_BORDER, pBorderPanel.Height - PANEL_BORDER);
      //   }
      //   else {
      //      if (EnsureWindowFitsMonitor(this)) {//efm5 if this returns true the title was wider than the border panel
      //         pBorderPanel.Size = new Size(ClientSize.Width - HALF_BORDER - pBorderPanel.Left,
      //            ClientSize.Height - HALF_BORDER - pBorderPanel.Top);
      //         pPanel.Size = new Size(pBorderPanel.Width - PANEL_BORDER, pBorderPanel.Height - PANEL_BORDER);
      //      }
      //   }
      //   pBorderPanel.RestoreAnchors();
      //   CenterToScreen();
      //   pBorderPanel.BringToFront();
      //   pBorderPanel.Show();
      //   pBorderPanel.Refresh();
      //}

      public static bool EnsureWindowFitsMonitor(Form pForm, bool pControlBox = true) {
#pragma warning disable CS8600
         Screen formScreen = Screen.FromControl(pForm), primaryScreen = Screen.PrimaryScreen;
#pragma warning restore CS8600
         Rectangle workingArea = formScreen.WorkingArea;
         Size size = pForm.Size;
         SizeF titleSize;
         Size workingSize = new Size(formScreen.WorkingArea.Width, formScreen.WorkingArea.Height);
         int x = pForm.Location.X, y = pForm.Location.Y, wantedTitleBarWidth = 0, controlBoxSpace = 1;
         bool changeSize = false;
         //Make sure the window was wide enough to display the title if possible

         if (pControlBox)
            controlBoxSpace = 4;

         using (Graphics graphics = pForm.CreateGraphics()) {
#pragma warning disable CS8604
            titleSize = graphics.MeasureString(pForm.Text, SystemFonts.CaptionFont);
#pragma warning restore CS8604
         }
         wantedTitleBarWidth = (int)((titleSize.Width * 0.86f) + (SystemInformation.CaptionButtonSize.Width * controlBoxSpace));
         //efm5 four small icons = leading gap + 3 ControlBox buttons
         if (size.Width < wantedTitleBarWidth) {
            size.Width = wantedTitleBarWidth;
            changeSize = true;
         }
         if (size.Width > workingSize.Width) {
            size.Width = workingSize.Width - WINDOW_REDUCER;
            changeSize = true;
         }
         if (size.Height > workingSize.Height) {
            size.Height = workingSize.Height - WINDOW_REDUCER;
            changeSize = true;
         }
         pForm.Size = size;

         if (pForm.Right > workingArea.Right)
            x = workingArea.Right - size.Width - WINDOW_REDUCER;
         if (pForm.Bottom > workingArea.Bottom)
            y = workingArea.Bottom - size.Height - WINDOW_REDUCER;
         pForm.Location = new Point(x, y);
         if (IsOffScreen(pForm))
            pForm.Location = new Point(formScreen.Bounds.Left + OFFSET, formScreen.Bounds.Top + OFFSET);
         if (IsPartiallyHidden(pForm)) {
            pForm.Location = new Point(formScreen.Bounds.Left + OFFSET, formScreen.Bounds.Top + OFFSET);
            if (pForm.Size.Width > formScreen.WorkingArea.Width - DOUBLE_OFFSET)
               pForm.Width = formScreen.WorkingArea.Width - DOUBLE_OFFSET;
            if (pForm.Size.Height > formScreen.WorkingArea.Height - DOUBLE_OFFSET)
               pForm.Height = formScreen.WorkingArea.Height - DOUBLE_OFFSET;
            changeSize = true;
         }
         return changeSize;
      }

      //private void AdjustForResolution(Form pForm) {
      //   Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
      //   Screen fromscreen = Screen.FromPoint(Location);

      //   GetDpi(fromscreen, DpiType.Effective, out uint x, out uint y);
      //   sScaling = (int)Math.Ceiling((float)x / 96f * 100f);
      //   sResolution = new Size(fromscreen.Bounds.Width, fromscreen.Bounds.Height);
      //   sComboBoxMaxDropdownHeight = (int)(sResolution.Height * 0.8f);
      //   sComboBoxMaxDropdownWidth = (int)(sResolution.Width * 0.8f);
      //   if (sScaling < 125)
      //      sScalePad = 1;
      //   else if ((sScaling >= 125) && (sScaling < 150))
      //      sScalePad = 22;
      //   else if ((sScaling >= 150) && (sScaling < 175))
      //      sScalePad = 28;
      //   else if ((sScaling >= 175) && (sScaling < 200))
      //      sScalePad = 34;
      //   else if ((sScaling >= 200) && (sScaling < 225))
      //      sScalePad = 40;
      //   else if ((sScaling >= 225) && (sScaling < 250))
      //      sScalePad = 46;
      //   else if ((sScaling >= 250) && (sScaling < 275))
      //      sScalePad = 52;
      //   else if ((sScaling >= 275) && (sScaling < 300))
      //      sScalePad = 58;
      //   else if ((sScaling >= 300) && (sScaling < 325))
      //      sScalePad = 64;
      //   else if ((sScaling >= 325) && (sScaling < 350))
      //      sScalePad = 70;
      //   else if ((sScaling >= 350) && (sScaling < 375))
      //      sScalePad = 76;
      //   else if ((sScaling >= 375) && (sScaling < 400))
      //      sScalePad = 82;
      //   else if ((sScaling >= 400) && (sScaling < 450))
      //      sScalePad = 88;
      //   else if (sScaling >= 450)
      //      sScalePad = 92;
      //   else
      //      sScalePad = 98;
      //}

      public static void AdjustForThemeFont(Font pFont) {
         float fontSize = pFont.SizeInPoints;

         if (fontSize < 15f)
            mFontWidthAdjustment = 0.75f;
         else if ((fontSize >= 15f) && (fontSize < 20f))
            mFontWidthAdjustment = 0.8f;
         else if ((fontSize >= 20f) && (fontSize < 30f))
            mFontWidthAdjustment = 0.85f;
         else if ((fontSize >= 30f) && (fontSize < 50f))
            mFontWidthAdjustment = 0.9f;
         else
            mFontWidthAdjustment = 0.95f;
         RecalculateAssociatedOffsets(pFont);
      }

      public static bool IsOffScreen(Form pForm) {
         Screen[] screens = Screen.AllScreens;
         foreach (Screen screen in screens) {
            Point formTopLeft = new Point(pForm.Left, pForm.Top);

            if (screen.WorkingArea.Contains(formTopLeft))
               return false;
         }
         return true;
      }

      public static bool IsPartiallyHidden(Form pForm) {
         Screen[] screens = Screen.AllScreens;
         foreach (Screen screen in screens) {
            if (screen.WorkingArea.Contains(new Point(pForm.Right, pForm.Bottom)))
               return false;
         }
         return true;
      }

      public static void CenterFormOnMonitor(Form pForm) {
         Screen screen = Screen.FromControl(pForm);
         Rectangle workingArea = screen.WorkingArea;
         pForm.Left = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - pForm.Width) / 2);
         pForm.Top = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - pForm.Height) / 2);
      }

      public static void RecalculateAssociatedOffsets(Font pFont) {
         float fontSize = pFont.SizeInPoints;

         mGroupLeftPad = (int)Math.Ceiling(fontSize * 2.5f);// 25
         mAssociatedButtonPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);//  3
         mAssociatedButtonPostLabelHorizontalSpace = (int)Math.Ceiling(fontSize * 0.3f);//  3
         mAssociatedButtonPostLabelVerticalOffset = (int)Math.Ceiling(fontSize * -0.4f);//  -4
         mAssociatedButtonPostTextBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);// 0
         mAssociatedCheckBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedCheckBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);// 4
         mAssociatedComboBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedComboBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);//  4
         mAssociatedLabelPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedLabelPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.5f);//  5
         mAssociatedLabelPostCheckBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.1f);//  1
         mAssociatedLabelPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.1f);//  1
         mAssociatedLabelPostPanelVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);//  3
         mAssociatedLabelPostUpDownHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedLabelPostUpDownVerticalOffset = (int)Math.Ceiling(fontSize * 0.1f);//  1
         mAssociatedPostVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedSliderPostUpDownHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedSliderPostUpDownVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);//  3
         mAssociatedTextBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);// 0
         mAssociatedTextBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.5f);//  5
         mAssociatedTextBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.2f);//  2
         mAssociatedTextBoxPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedTextBoxPostComboBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.1f);//  1
         mAssociatedTextBoxPostComboBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedTextPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.5f);//  5
         mAssociatedUpDownPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedUpDownPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);//  4
         mAssociatedUpDownPostCheckBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
         mAssociatedUpDownPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * -0.2f);//  -2
         mIndent = (int)Math.Ceiling(fontSize * 0.5f);//  5 
         mCancelOffset = (int)Math.Ceiling(fontSize * 1.5f);//  15
         mOkOffset = (int)Math.Ceiling(fontSize * 1.5f);//  15 
         mWidgetHorizontalSpace = (int)Math.Ceiling(fontSize * 0.3f);//  3 
         mWidgetBigHorizontalSpace = (int)Math.Ceiling(fontSize * 1.0f);//  10
         mWidgetVerticalOffset = (int)Math.Ceiling(fontSize * 0.2f);//  2 
         mWidgetBigVerticalOffset = (int)Math.Ceiling(fontSize * 0.7f);//  7 
         mAssociatedPostVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0 
         mEM = (int)Math.Ceiling(fontSize/* * 1.0f*/);//  10
         mMenuLeftOffset = (int)Math.Ceiling(fontSize * 2.5f);//  30
         mGroupTopPad = (int)Math.Ceiling(fontSize * 0.2f);//  2
         GroupBox groupBox = new GroupBox() {
            Font = CreateNewFont(pFont),
            Text = "the quick brown fox",//efm5 do not localize
            AutoSize = true
         };
         Panel panel = new Panel() {
            Font = CreateNewFont(pFont),
            Dock = DockStyle.Fill
         };
         Label label = new Label() {
            Font = CreateNewFont(pFont),
            Text = "the quick brown fox"//efm5 do not localize
         };
         panel.Controls.Add(label);
         groupBox.Controls.Add(panel);
         mGroupRightPad = groupBox.Width - panel.Width + (panel.Left * 2) + mIndent;
         mGroupBottomPad = groupBox.Height - panel.Height - panel.Top + mIndent + 2;
         label.Dispose();
         panel.Dispose();
         groupBox.Dispose();
      }

      public static void SizeGroupBox(GroupBox pGroupBox, bool pGroupPad = true) {
         int right = 0, bottom = 0, rightPad = 0, bottomPad = 0, wide = mMenuLeftOffset,
            menuCount = 0;
         SizeF stringSize = new SizeF(0, 0);

         using (Graphics graphics = pGroupBox.CreateGraphics()) {
            if (!string.IsNullOrEmpty(pGroupBox.Text))
               stringSize = graphics.MeasureString("M" + pGroupBox.Text, pGroupBox.Font);
         }
         if (pGroupPad) {
            rightPad = mGroupRightPad;
            bottomPad = mGroupBottomPad;
         }
         foreach (Control control in pGroupBox.Controls) {
            if (control is MenuStrip strip) {
               int menuWidth = 0;
               menuCount++;
               foreach (ToolStripMenuItem tsmi in strip.Items)
                  menuWidth += (tsmi.Width + mCancelOffset);
               if (menuWidth > wide)
                  wide = menuWidth;
            }
         }
         if (menuCount > 0)
            right = wide;
         foreach (Control control in pGroupBox.Controls) {
            if (control is MenuStrip)
               continue;
            if (control.Right > right)
               right = control.Right;
            if (control.Bottom > bottom)
               bottom = control.Bottom;
         }
         if (right < stringSize.Width)
            right = (int)Math.Ceiling(stringSize.Width);
         pGroupBox.Size = new Size(right + rightPad, bottom + bottomPad);
      }

      //public static void SizeGroupBox(GroupBox pGroupBox, Panel pPanel, bool pGroupPad = true) {
      //   int  rightPad = 0, bottomPad = 0;

      //   if (pGroupPad) {
      //      rightPad = sGroupRightPad;
      //      bottomPad = sGroupBottomPad;
      //   }
      //   pGroupBox.Size = new Size(
      //      pPanel.Width + pPanel.Left + rightPad + SystemInformation.VerticalScrollBarWidth + 4,
      //      pPanel.Height + pPanel.Top + bottomPad + SystemInformation.HorizontalScrollBarHeight);
      //}

      public static void SizePanel(Panel pPanel, int pLeftPad = 10, bool pScrollbarPad = true) {
         int right = 0, bottom = 0, scrollbarWidth = 0, scrollbarHeight = 0;

         if (pScrollbarPad) {
            scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
            scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;
         }
         foreach (Control control in pPanel.Controls) {
            if (control.Right > right)
               right = control.Right;
            if (control.Bottom > bottom)
               bottom = control.Bottom;
         }
         pPanel.Size = new Size(pLeftPad + right + scrollbarWidth, bottom + scrollbarHeight);
      }

      public static void SizePanelWidthToAccommodateBottomPanel(Panel pPanel, Button pRightmostButton) {
         if (pPanel.Width < (pRightmostButton.Right + mCancelOffset))
            pPanel.Width = pRightmostButton.Right + mCancelOffset;
         if (pPanel.Width > (int)(mMonitorSize.Width * 0.95f)) {
            pPanel.Width = (int)(mMonitorSize.Width * 0.95f);
            pPanel.AutoScroll = true;
         }
      }

      public static void SizeTextBoxToFitString(out SizeF pSize, RichTextBox pTextBox,
         string pExample = "", bool pDoWidth = true, bool pDoHeight = true, bool pPadWidth = true) {
         Font font = pTextBox.Font;
         SizeF stringSize = new SizeF(0, 0);
         pSize = stringSize;

         using (Graphics graphics = pTextBox.CreateGraphics()) {
            if (!string.IsNullOrEmpty(pExample)) //Prefer example
               stringSize = graphics.MeasureString(pExample, font);
            else if (!string.IsNullOrEmpty(pTextBox.Text))
               stringSize = graphics.MeasureString(pTextBox.Text, font);
            else//Worst-case
               stringSize = graphics.MeasureString("The quick brown fox", font);
         }
         if (pDoWidth) {
            if (pPadWidth)
               pSize.Width = stringSize.Width + mEM;
            else
               pSize.Width = stringSize.Width;
         }
         if (pDoHeight)
            pSize.Height = (int)Math.Ceiling(stringSize.Height * 1.3f);
      }

      public static void SizeTextBoxToFitString(out SizeF pSize, TextBox pTextBox, string pExample = "",
         bool pDoWidth = true, bool pDoHeight = true, bool pPadWidth = true) {
         Font font = pTextBox.Font;
         SizeF stringSize = new SizeF(0, 0);
         pSize = stringSize;

         using (Graphics graphics = pTextBox.CreateGraphics()) {
            if (!string.IsNullOrEmpty(pExample)) //Prefer example
               stringSize = graphics.MeasureString(pExample, font);
            else if (!string.IsNullOrEmpty(pTextBox.Text))
               stringSize = graphics.MeasureString(pTextBox.Text, font);
            else//Worst-case
               stringSize = graphics.MeasureString("The quick brown fox", font);
         }
         if (pDoWidth) {
            if (pPadWidth)
               pSize.Width = stringSize.Width + mEM;
            else
               pSize.Width = stringSize.Width;
         }
         if (pDoHeight)
            pSize.Height = stringSize.Height;
      }

      public static void SetComboBoxSize(out SizeF pSize, ComboBox pComboBox, string pExample = "") {
         SizeF stringSize = new SizeF(0, 0),
            paddingSize = new SizeF(0, 0);
         pSize = stringSize;
         Font font = pComboBox.Font;

         using (Graphics graphics = pComboBox.CreateGraphics()) {
            paddingSize = graphics.MeasureString("0yÑ", font);
            paddingSize.Height += (font.SizeInPoints / 2.7f);
            if (!string.IsNullOrEmpty(pExample)) //Prefer example
               stringSize = graphics.MeasureString(pExample, font);
            else if (pComboBox.Items.Count > 0) {
               foreach (string phrase in pComboBox.Items) {
                  if (!string.IsNullOrEmpty(phrase)) {
                     SizeF temporaryStringSize = new SizeF(0, 0);
                     temporaryStringSize = graphics.MeasureString(phrase, font);
                     if (temporaryStringSize.Width > stringSize.Width)
                        stringSize.Width = temporaryStringSize.Width;
                  }
               }
            }
            else//Worst-case
               stringSize = graphics.MeasureString("The quick brown fox", font);
         }
         pSize.Width = stringSize.Width + paddingSize.Width + SystemInformation.VerticalScrollBarWidth;
         pSize.Height = stringSize.Height + paddingSize.Height;
      }

      public static void SetComboBoxDropDownWidth(ComboBox pComboBox, int pMinimumWidth = 50) {
         if (pComboBox.Items.Count == 0)
            return;//don't change the width
         Font font = pComboBox.Font;
         float boxWidth = 0;
         SizeF stringSize = new SizeF();
         int minWidth = pMinimumWidth;

         if (minWidth == 0)
            minWidth = pComboBox.Width;
         else {
            try {
               using (Graphics graphics = pComboBox.CreateGraphics()) {
                  foreach (string phrase in pComboBox.Items) {
                     if (!string.IsNullOrEmpty(phrase)) {
                        stringSize = graphics.MeasureString(phrase, font);
                        if (stringSize.Width > boxWidth)
                           boxWidth = stringSize.Width;
                     }
                  }
               }
               if (boxWidth < minWidth)
                  boxWidth = minWidth;
               if (boxWidth > COMBOBOX_MAXIMUM_DROPDOWN_WIDTH)
                  boxWidth = COMBOBOX_MAXIMUM_DROPDOWN_WIDTH;
               pComboBox.DropDownWidth = (int)boxWidth;
            }
            catch (Exception) {
               //_ = AskingAsync(new TM("SetComboBoxDropDownWidth; exception caught and handled", pException));
               pComboBox.DropDownWidth = 200;
            }
         }
      }

      //public static void SetComboBoxWidth(ComboBox pComboBox) {
      //   float boxWidth = 0f;
      //   SizeF stringSize = new SizeF();

      //   using (Graphics graphics = pComboBox.CreateGraphics()) {
      //      foreach (object item in pComboBox.Items) {
      //         if (item.GetType() == typeof(string)) {
      //            if (!String.IsNullOrEmpty((string)item)) {
      //               stringSize = graphics.MeasureString((string)item, Settings.Default.InterfaceFont);
      //               if (stringSize.Width > boxWidth)
      //                  boxWidth = stringSize.Width;
      //            }
      //         }
      //      }
      //   }
      //   boxWidth = boxWidth + gIndent + SystemInformation.VerticalScrollBarWidth;
      //   pComboBox.Width = (int)boxWidth;
      //}

      public static void SetUpDownBoxWidth(NumericUpDown pNumericUpDown) {
         float boxWidth = 0f, boxHeight = 0f;
         SizeF stringSize = new SizeF();
         string minimumValue = string.Format("{0}", pNumericUpDown.Minimum),
            maximumValue = string.Format("{0}", pNumericUpDown.Maximum);

         using (Graphics graphics = pNumericUpDown.CreateGraphics()) {
            if (maximumValue.Length > minimumValue.Length)
               stringSize = graphics.MeasureString(maximumValue + "0", pNumericUpDown.Font);
            else
               stringSize = graphics.MeasureString(minimumValue + "0", pNumericUpDown.Font);
            if (stringSize.Width > boxWidth)
               boxWidth = stringSize.Width;
            if (stringSize.Height > boxHeight)
               boxHeight = stringSize.Height;
         }
         //The Up/Down arrows is about the same width as the scrollbar width
         pNumericUpDown.Width = (int)(boxWidth + SystemInformation.VerticalScrollBarWidth);
         pNumericUpDown.Height = (int)(boxHeight + mIndent);
      }

      public static bool IsSizeBigger(Size pOriginal, Size pProposed) {
         if ((pProposed.Height > pOriginal.Height) || (pProposed.Width > pOriginal.Width))
            return true;
         return false;
      }

      public static bool RectangleContainsPoint(Rectangle pRectangle, Point pPoint) {
         return (pPoint.X >= pRectangle.Left) && (pPoint.X <= pRectangle.Right) && (pPoint.Y >= pRectangle.Top) &&
            (pPoint.Y <= pRectangle.Bottom);
      }

      public static string MassageColorName(string pCompressedName) {
         string expandedName = string.Empty;

         if (!IsKnownColor(pCompressedName, out Color color))
            return pCompressedName;
         foreach (char c in pCompressedName) {
            if (char.IsUpper(c))
               expandedName += " " + string.Format("{0}", c);
            else
               expandedName += string.Format("{0}", c);
         }
         expandedName = expandedName.Trim(' ');
         return expandedName;
      }

      public static Color SubtlyDifferent(Color pColor) {
         int r = 128, g = 128, b = 128;

         if ((pColor.R + pColor.G + pColor.B) < 382) {//Dark
            r = (int)Math.Floor(pColor.R * 1.1f);
            if (r > 255)
               r = 255;
            g = (int)Math.Floor(pColor.G * 1.1f);
            if (g > 255)
               g = 255;
            b = (int)Math.Floor(pColor.B * 1.1f);
            if (b > 255)
               b = 255;
         }
         else {
            r = (int)Math.Floor(pColor.R * 0.9f);
            if (r < 0)
               r = 0;
            g = (int)Math.Floor(pColor.G * 0.9f);
            if (g > 0)
               g = 0;
            b = (int)Math.Floor(pColor.B * 0.9f);
            if (b > 0)
               b = 0;
         }
         return Color.FromArgb(r, g, b);
      }

      public static bool ColorsAreSimilar(Color pColor1, Color pColor2) {
         int rDist = Math.Abs(pColor1.R - pColor2.R),
        gDist = Math.Abs(pColor1.G - pColor2.G),
        bDist = Math.Abs(pColor1.B - pColor2.B);

         if ((rDist + gDist + bDist) > 260)
            return false;
         return true;
      }

      public static Color ContrastingColor(Color pColor) {
         if ((pColor.R == pColor.G) && (pColor.R == pColor.B)) {
            if ((pColor.R + pColor.G + pColor.B) < 382)
               return Color.LightBlue;
            else
               return Color.DarkBlue;
         }
         else {
            if ((pColor.R + pColor.G + pColor.B) < 382)
               return Color.LightGray;
            else
               return Color.DarkGray;
         }
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

      //public static bool IsKnownColor(Color pColor) {
      //   Color color;

      //   foreach (string colorName in Enum.GetNames<KnownColor>()) {
      //      //cast the colorName into a KnownColor
      //      if (!Enum.TryParse<KnownColor>(colorName, out KnownColor oKnownColor))
      //         continue;
      //      //check if the knownColor variable is a System color - 
      //      if (oKnownColor > KnownColor.Transparent) {//  Transparent -27- is the highest numbered system color
      //         color = Color.FromName(colorName);
      //         if (color == pColor)
      //            return true;
      //      }
      //   }
      //   return false;
      //}

      //public static bool IsKnownColor(string pColorName, out Color pOColor) {
      //   pOColor = Color.Transparent;
      //   List<string> colors = new List<string>();

      //   foreach (string colorName in Enum.GetNames<KnownColor>()) {
      //      //cast the colorName into a KnownColor
      //      if (!Enum.TryParse<KnownColor>(colorName, out KnownColor oKnownColor))
      //         continue;
      //      //check if the knownColor variable is a System color
      //      if (oKnownColor > KnownColor.Transparent) //  Transparent -27- is the highest numbered system color
      //         colors.Add(colorName);
      //   }
      //   if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase)) {
      //      pOColor = Color.FromName(pColorName);
      //      return true;
      //   }
      //   return false;
      //}

      //public static bool IsKnownColor(string pColorName) {
      //   List<string> colors = new List<string>();

      //   foreach (string colorName in Enum.GetNames<KnownColor>()) {
      //      //cast the colorName into a KnownColor
      //      if (!Enum.TryParse<KnownColor>(colorName, out KnownColor oKnownColor))
      //         continue;
      //      //check if the knownColor variable is a System color
      //      if (oKnownColor > KnownColor.Transparent)
      //         colors.Add(colorName);
      //   }
      //   if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase))
      //      return true;
      //   return false;
      //}

      //public static Color GroupBoxBackgroundColor(Color pColor) {
      //   int red = 127, green = 127, blue = 127;

      //   if (pColor.R <= 127)
      //      red = pColor.R + 50;
      //   else
      //      red = pColor.R - 50;
      //   if (pColor.G <= 127)
      //      green = pColor.G + 50;
      //   else
      //      green = pColor.G - 50;
      //   if (pColor.B <= 127)
      //      blue = pColor.B + 50;
      //   else
      //      blue = pColor.B - 50;

      //   return Color.FromArgb(red, green, blue);
      //}

      //public static Color GroupBoxTextColor(Color pBackgroundColor) {
      //   if (ColorsAreSimilar(sProposedInterfaceTextColor, pBackgroundColor)) {
      //      int red = 0, green = 0, blue = 0;

      //      if (pBackgroundColor.R <= 127)
      //         red = 255;
      //      if (pBackgroundColor.G <= 127)
      //         green = 255;
      //      if (pBackgroundColor.B <= 127)
      //         blue = 255;
      //      return Color.FromArgb(red, green, blue);
      //   }
      //   return sProposedInterfaceTextColor;
      //}

      public static int GetGroupBoxFirstLineOffset(GroupBox pGroupBox) {
         SizeF stringSize = new SizeF();

         using (Graphics graphics = pGroupBox.CreateGraphics())
            stringSize = graphics.MeasureString(pGroupBox.Text + "Ñçg", pGroupBox.Font);
         return (int)stringSize.Height + mGroupTopPad + mScalingGroupBoxTopLinePad;
      }

      //public void HidePanel(Panel pPanel) {
      //   if (pPanel == null)
      //      return;
      //   pPanel.SendToBack();
      //   pPanel.Hide();
      //   if (Controls.Contains(pPanel))
      //      Controls.Remove(pPanel);
      //}

      //public void ShowPanel(Panel pPanel) {
      //   if (pPanel == null)
      //      return;
      //   BringToFront();
      //   Activate();
      //   if (!Controls.Contains(pPanel))
      //      Controls.Add(pPanel);
      //   pPanel.Show();
      //   pPanel.BringToFront();
      //}

      public static void SetBottomPanelHeight(Panel pPanel) {
         int top = 1, height = 1;

         foreach (Button button in pPanel.Controls.OfType<Button>()) {
            if (button.Top > top)
               top = button.Top;
            if (button.Height > height)
               height = button.Height;
         }
         foreach (MenuStrip menu in pPanel.Controls.OfType<MenuStrip>()) {
            if (menu.Top > top)
               top = menu.Top;
            if (menu.Height > height)
               height = menu.Height;
         }
         pPanel.Height = height + (top * 2);
      }

      public static bool ColorsAreIdentical(Color pColor1, Color pColor2) {
         if ((pColor1.R == pColor2.R) && (pColor1.G == pColor2.G) && (pColor1.B == pColor2.B))
            return true;
         return false;
      }

      public static Font CreateNewFont(Font pFont) {
         return new Font(pFont.Name, pFont.SizeInPoints, pFont.Style);
      }

      public static Font CreateNewTitleFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         //DEBUG efm5 2026 03 31 for these new font creators should I use multiplication or Addition
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints + 4f, FontStyle.Bold);
      }

      public static Font CreateNewWarningFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         //DEBUG efm5 2026 03 31 for these new font creators should I use multiplication or Addition
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints + 2f, FontStyle.Bold);
      }

      internal static RECT RECTFromRectangle(Rectangle pRectangle) {
         return new RECT() {
            Left = pRectangle.Left,
            Top = pRectangle.Top,
            Right = pRectangle.Right,
            Bottom = pRectangle.Bottom
         };
      }

      internal static Rectangle RectangleFromRECT(RECT pRECT) {
         return new Rectangle() {
            X = pRECT.Left,
            Y = pRECT.Top,
            Size = new Size(pRECT.Right - pRECT.Left, pRECT.Bottom - pRECT.Top)
         };
      }

      public static void SelectPartOfText(RichTextBox pRichTextBox, float pPart = 0.5f) {
         pRichTextBox.Select(0, (int)(pRichTextBox.Text.Length * pPart));
         HomeTextBoxInsertionPoint(pRichTextBox);
         pRichTextBox.Refresh();
      }

      const int EM_LINESCROLL = 0x00B6;
      public static void HomeTextBoxInsertionPoint(RichTextBox pRichTextBox) {
         _ = SendMessage(pRichTextBox.Handle, EM_LINESCROLL, 0, 0);
      }

      public static void HomeTextBoxInsertionPoint(TextBox pTextBox) {
         _ = SendMessage(pTextBox.Handle, EM_LINESCROLL, 0, 0);
      }

      public static void SelectPartOfText(TextBox pTextBox, float pPart = 0.5f) {
         pTextBox.Select(0, (int)(pTextBox.Text.Length * pPart));
         HomeTextBoxInsertionPoint(pTextBox);
         pTextBox.Refresh();
      }

      //public void FadeIn(double pTranslucency = 1d) {
      //   if (!gFade) {
      //      Opacity = 1;
      //      return;
      //   }
      //   double translucency = 1d;

      //   if (pTranslucency > 1)
      //      translucency = 1d;
      //   else
      //      translucency = pTranslucency;
      //   double translucencySteps = translucency / sTransitionSteps;

      //   Opacity = translucencySteps;
      //   for (int i = 0; i < (sTransitionSteps - 1); i++) {
      //      Opacity += translucencySteps;
      //      Thread.Sleep(sTransitionInterval);
      //   }
      //   Opacity = translucency;
      //}

      //public void FadeOut(double pTranslucency = 0d) {
      //   if (!gFade) {
      //      Opacity = 0;
      //      return;
      //   }
      //   double translucencySteps = Opacity / sTransitionSteps;

      //   Opacity -= translucencySteps;
      //   for (int i = 0; i < (sTransitionSteps - 1); i++) {
      //      Opacity -= translucencySteps;
      //      Thread.Sleep(sTransitionInterval);
      //   }
      //   Opacity = pTranslucency;
      //}

      public static bool LocateUpDownLine(out int pONextTop, int pTop, Button? pPrefixButton, NumericUpDown? pUpDown,
         Label? pSuffixLabel = null, int pLeft = -1) {
         pONextTop = pTop;
         if ((pPrefixButton == null) || (pUpDown == null)) {
            TimedMessage("LocateUpDownLine() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         if (pLeft == -1)
            pLeft = pPrefixButton.Left;
         pPrefixButton.Location = new Point(pLeft, pTop);
         pUpDown.Location = new Point(pPrefixButton.Right + mAssociatedUpDownPostButtonHorizontalSpace,
            pPrefixButton.Top + mAssociatedUpDownPostButtonVerticalOffset);
         pONextTop = Math.Max(pPrefixButton.Bottom, pUpDown.Bottom);
         if (pSuffixLabel != null) {
            pSuffixLabel.Location = new Point(pUpDown.Right + mAssociatedLabelPostUpDownHorizontalSpace,
               pUpDown.Top + mAssociatedLabelPostUpDownVerticalOffset);
            pONextTop = Math.Max(pONextTop, pSuffixLabel.Bottom);
         }
         return true;
      }

      public static bool LocateCheckBoxLine(out int oNextTop, int pTop, CheckBox? pCheckBox, Label? pSuffixLabel = null, int pLeft = 20) {
         oNextTop = pTop;
         if (pCheckBox == null) {
            TimedMessage("LocateCheckBoxLine() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         pCheckBox.Location = new Point(pLeft, pTop);
         if (pSuffixLabel != null) {
            pSuffixLabel.Location = new Point(
               pCheckBox.Right + mAssociatedLabelPostCheckBoxHorizontalSpace,
               pCheckBox.Top + mAssociatedLabelPostCheckBoxVerticalOffset);
            oNextTop = (int)Math.Max(pCheckBox.Bottom, pSuffixLabel.Bottom);
         }
         else
            oNextTop = pCheckBox.Bottom;
         return true;
      }

      public static bool LocatePrefixedTextBox(out Point oNextLocation, int pTop, Button? pPrefixButton, TextBox? pTextBox, int pLeft = 20) {
         oNextLocation = new Point(pLeft, pTop);
         if ((pPrefixButton == null) || (pTextBox == null)) {
            TimedMessage("LocatePrefixedTextBoxLine() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         pPrefixButton.Location = new Point(pLeft, pTop);
         pTextBox.Location = new Point(
            pPrefixButton.Right + mAssociatedTextBoxPostButtonHorizontalSpace,
            pPrefixButton.Top + mAssociatedTextBoxPostButtonVerticalOffset);
         oNextLocation = new Point(pTextBox.Right, Math.Max(pPrefixButton.Bottom, pTextBox.Bottom));
         return true;
      }

      public static bool LocateControls(out Point oNextPaddedLocation, Control? pAnchorControl, List<Control>? pControlList,
         bool pHorizontal, int pPadding) {
         Control? lastControl = pAnchorControl;

         oNextPaddedLocation = new Point(0, 0);
         if ((pAnchorControl == null) || (pControlList == null) || (lastControl == null)) {
            TimedMessage("LocateControls() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         foreach (Control radioButton in pControlList.OfType<Control>()) {
            if (pHorizontal)
               radioButton.Location = new Point(lastControl.Right + pPadding, lastControl.Top);
            else
               radioButton.Location = new Point(lastControl.Left, lastControl.Bottom + pPadding);
            lastControl = radioButton;
         }
         oNextPaddedLocation = new Point(lastControl.Right, lastControl.Bottom);
         return true;
      }

      public void ToCenterOrNot(Form pForm, bool pControlBox = true) {
         bool centerHorizontal = true, centerVertical = true;
         Screen currentMonitor = Screen.FromControl(pForm),
#pragma warning disable CS8600
            primaryscreen = Screen.PrimaryScreen;
#pragma warning restore CS8600
         int ninetyHorizontal = (int)Math.Floor(currentMonitor.WorkingArea.Width * 0.9f),
           ninetyVertical = (int)Math.Floor(currentMonitor.WorkingArea.Height * 0.9f);
         EnsureWindowFitsMonitor(pForm, pControlBox);
         if (pForm.Width > ninetyHorizontal)
            centerHorizontal = false;
         if (pForm.Height > ninetyVertical)
            centerVertical = false;
         if (centerHorizontal && centerVertical) {
            pForm.Location = new Point(
               (int)(currentMonitor.Bounds.X + (currentMonitor.Bounds.Width - pForm.Size.Width) / 2),
               (int)(currentMonitor.Bounds.Y + (currentMonitor.Bounds.Height - pForm.Size.Height) / 2));
         }
         else if (centerHorizontal) {
            pForm.Left =
               (int)(currentMonitor.Bounds.X + (currentMonitor.Bounds.Width - pForm.Size.Width) / 2);
         }
         else if (centerVertical) {
            pForm.Top =
               (int)(currentMonitor.Bounds.Y + (currentMonitor.Bounds.Height - pForm.Size.Height) / 2);
         }
      }

      public static Size SizeFromSizeF(SizeF pSizeF) {
         return new Size((int)Math.Ceiling(pSizeF.Width), (int)Math.Ceiling(pSizeF.Height));
      }

      public static Size SizeFromSize(Size pSize) {
         return new Size(pSize.Width, pSize.Height);
      }

      public static Size SizeFromFloats(float pWidth, float pHeight) {
         return new Size((int)Math.Ceiling(pWidth), (int)Math.Ceiling(pHeight));
      }

      public static bool BooleanFromString(string pInput, out bool oBoolean) {
         oBoolean = false;
         if (string.Equals("True", pInput, StringComparison.OrdinalIgnoreCase)) {
            oBoolean = true;
            return true;
         }
         if (string.Equals("False", pInput, StringComparison.OrdinalIgnoreCase)) {
            oBoolean = false;
            return true;
         }
         return false;
      }

      public static void UpDownSelectAll(NumericUpDown pNumericUpDown) {
         pNumericUpDown.Focus();
         pNumericUpDown.Select(0, pNumericUpDown.Text.Length);
      }

      public static void TextBoxSelectAll(TextBox pTextBox) {
         pTextBox.Focus();
         pTextBox.SelectAll();
         //SendKeys.SendWait("{HOME}+{END}");
      }

      public static void ComboBoxSelectAll(ComboBox pComboBox) {
         pComboBox.Focus();
         pComboBox.SelectAll();
         pComboBox.DroppedDown = true;
      }

      public static bool FontsAreEquals(Font pFirst, Font pSecond) {
         if (!string.Equals(pFirst.Name, pSecond.Name, StringComparison.OrdinalIgnoreCase))
            return false;
         if (pFirst.SizeInPoints != pSecond.SizeInPoints)
            return false;
         if (pFirst.Style != pSecond.Style)
            return false;
         return true;
      }

      public static Point PlusEquals(Point pPointA, Point pPointB) {
         Point result = new Point {
            X = pPointA.X + pPointB.X,
            Y = pPointA.Y + pPointB.Y
         };
         return result;
      }

      public static Point PointFromPoint(Point pPointA) {
         Point result = new Point {
            X = pPointA.X,
            Y = pPointA.Y
         };
         return result;
      }

      private bool FontsAreEqual(Font pFirstFont, Font pSecondFont) {
         if ((pFirstFont.Name == pSecondFont.Name) && (pFirstFont.SizeInPoints == pSecondFont.SizeInPoints) &&
           (pFirstFont.Style == pSecondFont.Style))
            return true;
         return false;
      }

      public static void GetDpi(Screen pScreen, DpiType pDpiType, out uint pODpiX, out uint pODpiY) {
         Point location = new System.Drawing.Point(pScreen.Bounds.Left + 1, pScreen.Bounds.Top + 1);
         nint monitor = MonitorFromPoint(location, 2/*MONITOR_DEFAULTTONEAREST*/);
         GetDpiForMonitor(monitor, pDpiType, out pODpiX, out pODpiY);
      }
#pragma warning restore IDE1006
#pragma warning restore IDE0028
   }
}
