namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006
      public static void ToCenterOrNot(Form pForm, bool pControlBox = true) {
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

      public static void RightAlign(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return;
         int rightmost = Rightmost(pControls);
         foreach (Control control in pControls)
            if (control.Right < rightmost)
               control.Left += (rightmost - control.Right);
      }

      public static List<Control> ControlCollectionAsList(Control.ControlCollection pControlCollection) {
         List<Control> returnValue = new List<Control>();
         foreach (Control control in pControlCollection)
            returnValue.Add(control);
         return returnValue;
      }

      public static void PaintPanel(Panel? pPanel) {
         if (pPanel == null)
            return;
      }

      private static void SetFontComboBoxWidth(ComboBox? pComboBox) {
         if (pComboBox == null)
            return;
         float boxWidth = 30f;
         float boxHeight = 30f;
         SizeF stringSize = new SizeF();
         using (Graphics graphics = pComboBox.CreateGraphics()) {
            foreach (string fontName in pComboBox.Items)
               if (!String.IsNullOrEmpty(fontName)) {
                  stringSize = graphics.MeasureString(fontName, pComboBox.Font);
                  if (stringSize.Width > boxWidth)
                     boxWidth = stringSize.Width;
                  if (stringSize.Height > boxHeight)
                     boxHeight = stringSize.Height;
               }
         }
         pComboBox.Width = (int)(boxWidth + SystemInformation.VerticalScrollBarWidth);
         pComboBox.Height = (int)(boxHeight + mIndent);
      }

      private static void SetFontComboBoxDropDownWidth(ComboBox? pComboBox) {
         if (pComboBox == null)
            return;
         if (pComboBox.Items.Count == 0)
            return;
         float boxWidth = 30f;
         SizeF stringSize = new SizeF();
         using (Graphics graphics = pComboBox.CreateGraphics()) {
            foreach (string fontName in pComboBox.Items)
               if (!String.IsNullOrEmpty(fontName)) {
                  stringSize = graphics.MeasureString(fontName, pComboBox.Font);
                  if (stringSize.Width > boxWidth)
                     boxWidth = stringSize.Width;
               }
         }
         if (boxWidth > pComboBox.Width) {
            if (boxWidth > COMBOBOX_MAXIMUM_DROPDOWN_WIDTH)
               boxWidth = COMBOBOX_MAXIMUM_DROPDOWN_WIDTH;
            pComboBox.DropDownWidth = (int)boxWidth;
         }
      }

      public static void CenterControlHorizontally(Control? pContainer, Control? pChildControl) {
         if ((pContainer == null) || (pChildControl == null))
            return;
         if (pContainer.Width < pChildControl.Width) {
            pContainer.Width = pChildControl.Width + 2;
            pChildControl.Left = 1;
            TimedMessage(
               string.Format(
                  "CenterControlHorizontally() error:{0}container: {1} is narrower than{0}the control to be centered: {2}",
                  Environment.NewLine,
                  pContainer.Name,
                  pChildControl.Name),
               "Code VIOLATION",
               0);
         }
         else
            pChildControl.Left = (pContainer.Width - pChildControl.Width) / 2;
      }

      public static void CenterControlVertically(Control pContainer, Control pChildControl) {
         if (pContainer.Height < pChildControl.Height) {
            pChildControl.Width = pContainer.Height + 2;
            pChildControl.Top = 1;
            TimedMessage(
               string.Format(
                  "CenterControlVertically() error:{0}container: {1} is shorter than{0}the control to be centered: {2}",
                  Environment.NewLine,
                  pContainer.Name,
                  pChildControl.Name),
               "Code VIOLATION",
               0);
         }
         else
            pChildControl.Top = (pContainer.Height - pChildControl.Height) / 2;
      }

      public static void CenterControl(Control pContainer, Control pChildControl) {
         CenterControlHorizontally(pContainer, pChildControl);
         CenterControlVertically(pContainer, pChildControl);
      }

      public static bool EnsureWindowFitsMonitor(Form pForm, bool pControlBox = true) {
#pragma warning disable CS8600
         Screen formScreen = Screen.FromControl(pForm);
         Screen primaryScreen = Screen.PrimaryScreen;
#pragma warning restore CS8600
         Rectangle workingArea = formScreen.WorkingArea;
         Size size = pForm.Size;
         SizeF titleSize;
         int x = pForm.Location.X;
         int y = pForm.Location.Y;
         int wantedTitleBarWidth = 0;
         int controlBoxSpace = pControlBox ? 4 : 1;
         bool changeSize = false;
         using (Graphics graphics = pForm.CreateGraphics()) {
#pragma warning disable CS8604
            titleSize = graphics.MeasureString(pForm.Text, SystemFonts.CaptionFont);
#pragma warning restore CS8604
         }
         wantedTitleBarWidth =
            (int)((titleSize.Width * 0.86f) +
            (SystemInformation.CaptionButtonSize.Width * controlBoxSpace));
         if (size.Width < wantedTitleBarWidth) {
            size.Width = wantedTitleBarWidth;
            changeSize = true;
         }
         if (size.Width > workingArea.Width) {
            size.Width = workingArea.Width - WINDOW_REDUCER;
            changeSize = true;
         }
         if (size.Height > workingArea.Height) {
            size.Height = workingArea.Height - WINDOW_REDUCER;
            changeSize = true;
         }
         pForm.Size = size;
         if (pForm.Right > workingArea.Right)
            x = workingArea.Right - size.Width - WINDOW_REDUCER;
         if (pForm.Bottom > workingArea.Bottom)
            y = workingArea.Bottom - size.Height - WINDOW_REDUCER;
         pForm.Location = new Point(x, y);
         if (IsOffScreen(pForm))
            pForm.Location = new Point(formScreen.Bounds.Left + OFFSET,
                                       formScreen.Bounds.Top + OFFSET);
         if (IsPartiallyHidden(pForm)) {
            pForm.Location = new Point(formScreen.Bounds.Left + OFFSET,
                                       formScreen.Bounds.Top + OFFSET);
            if (pForm.Width > formScreen.WorkingArea.Width - DOUBLE_OFFSET)
               pForm.Width = formScreen.WorkingArea.Width - DOUBLE_OFFSET;
            if (pForm.Height > formScreen.WorkingArea.Height - DOUBLE_OFFSET)
               pForm.Height = formScreen.WorkingArea.Height - DOUBLE_OFFSET;
            changeSize = true;
         }
         return changeSize;
      }

      public static void CenterFormOnMonitor(Form pForm) {
         Screen screen = Screen.FromControl(pForm);
         Rectangle workingArea = screen.WorkingArea;
         pForm.Left = Math.Max(workingArea.X,
            workingArea.X + (workingArea.Width - pForm.Width) / 2);
         pForm.Top = Math.Max(workingArea.Y,
            workingArea.Y + (workingArea.Height - pForm.Height) / 2);
      }

      public static void AdjustForThemeFont(Font pFont) {
         float fontSize = pFont.SizeInPoints;
         if (fontSize < 15f)
            mFontWidthAdjustment = 0.75f;
         else if (fontSize < 20f)
            mFontWidthAdjustment = 0.8f;
         else if (fontSize < 30f)
            mFontWidthAdjustment = 0.85f;
         else if (fontSize < 50f)
            mFontWidthAdjustment = 0.9f;
         else
            mFontWidthAdjustment = 0.95f;
         RecalculateAssociatedOffsets(pFont);
      }

      public static void RecalculateAssociatedOffsets(Font pFont) {
         float fontSize = pFont.SizeInPoints;
         mGroupLeftPad = (int)Math.Ceiling(fontSize * 2.5f);
         mAssociatedButtonPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);
         mAssociatedButtonPostLabelHorizontalSpace = (int)Math.Ceiling(fontSize * 0.3f);
         mAssociatedButtonPostLabelVerticalOffset = (int)Math.Ceiling(fontSize * -0.4f);
         mAssociatedButtonPostTextBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedCheckBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedCheckBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);
         mAssociatedComboBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedComboBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);
         mAssociatedLabelPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedLabelPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.5f);
         mAssociatedLabelPostCheckBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.1f);
         mAssociatedLabelPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.1f);
         mAssociatedLabelPostPanelVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);
         mAssociatedLabelPostUpDownHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedLabelPostUpDownVerticalOffset = (int)Math.Ceiling(fontSize * 0.1f);
         mAssociatedPostVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedSliderPostUpDownHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedSliderPostUpDownVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);
         mAssociatedTextBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.5f);
         mAssociatedTextBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.2f);
         mAssociatedTextBoxPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedTextBoxPostComboBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.1f);
         mAssociatedTextBoxPostComboBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedTextPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.5f);
         mAssociatedUpDownPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedUpDownPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);
         mAssociatedUpDownPostCheckBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);
         mAssociatedUpDownPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * -0.2f);
         mIndent = (int)Math.Ceiling(fontSize * 0.5f);
         mCancelOffset = (int)Math.Ceiling(fontSize * 1.5f);
         mOkOffset = (int)Math.Ceiling(fontSize * 1.5f);
         mWidgetHorizontalSpace = (int)Math.Ceiling(fontSize * 0.3f);
         mWidgetBigHorizontalSpace = (int)Math.Ceiling(fontSize * 1.0f);
         mWidgetVerticalOffset = (int)Math.Ceiling(fontSize * 0.2f);
         mWidgetBigVerticalOffset = (int)Math.Ceiling(fontSize * 0.7f);
         mEM = (int)Math.Ceiling(fontSize);
         mMenuLeftOffset = (int)Math.Ceiling(fontSize * 2.5f);
         mGroupTopPad = (int)Math.Ceiling(fontSize * 0.2f);
         GroupBox groupBox = new GroupBox();
         groupBox.Font = CreateNewFont(pFont);
         groupBox.Text = "the quick brown fox";
         groupBox.AutoSize = true;
         Panel panel = new Panel();
         panel.Font = CreateNewFont(pFont);
         panel.Dock = DockStyle.Fill;
         Label label = new Label();
         label.Font = CreateNewFont(pFont);
         label.Text = "the quick brown fox";
         panel.Controls.Add(label);
         groupBox.Controls.Add(panel);
         mGroupRightPad =
            groupBox.Width - panel.Width + (panel.Left * 2) + mIndent;
         mGroupBottomPad =
            groupBox.Height - panel.Height - panel.Top + mIndent + 2;
         label.Dispose();
         panel.Dispose();
         groupBox.Dispose();
      }

      public static void SizeGroupBox(GroupBox pGroupBox, bool pGroupPad = true) {
         int right = 0;
         int bottom = 0;
         int rightPad = pGroupPad ? mGroupRightPad : 0;
         int bottomPad = pGroupPad ? mGroupBottomPad : 0;
         int wide = mMenuLeftOffset;
         int menuCount = 0;
         SizeF stringSize = new SizeF(0, 0);
         using (Graphics graphics = pGroupBox.CreateGraphics()) {
            if (!string.IsNullOrEmpty(pGroupBox.Text))
               stringSize = graphics.MeasureString("M" + pGroupBox.Text, pGroupBox.Font);
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

      public static void SizePanel(Panel pPanel, int pLeftPad = 10, bool pScrollbarPad = true) {
         int right = 0;
         int bottom = 0;
         int scrollbarWidth = pScrollbarPad ? SystemInformation.VerticalScrollBarWidth : 0;
         int scrollbarHeight = pScrollbarPad ? SystemInformation.HorizontalScrollBarHeight : 0;
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

      public static void SizeTextBoxToFitString(out SizeF pSize, RichTextBox pTextBox, string pExample = "",
         bool pDoWidth = true, bool pDoHeight = true, bool pPadWidth = true) {
         Font font = pTextBox.Font;
         SizeF stringSize = new SizeF(0, 0);
         using (Graphics graphics = pTextBox.CreateGraphics()) {
            if (!string.IsNullOrEmpty(pExample))
               stringSize = graphics.MeasureString(pExample, font);
            else if (!string.IsNullOrEmpty(pTextBox.Text))
               stringSize = graphics.MeasureString(pTextBox.Text, font);
            else
               stringSize = graphics.MeasureString("X", font);
         }
         float width = pDoWidth ? stringSize.Width : pTextBox.Width;
         float height = pDoHeight ? stringSize.Height : pTextBox.Height;

         if (pPadWidth)
            width += mIndent;
         pSize = new SizeF(width, height);
      }
#pragma warning restore IDE1006
   }
}
