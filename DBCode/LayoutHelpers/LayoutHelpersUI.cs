namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static void FlattenButton(Button? pButton, Color? pBackgroundColor, int pLeft = 0) {
         if ((pButton != null) && (pBackgroundColor != null)) {
            pButton.BackColor = Color.Transparent;
            pButton.FlatAppearance.BorderColor = (Color)pBackgroundColor;
            pButton.FlatAppearance.BorderSize = 0;
            pButton.FlatStyle = FlatStyle.Flat;
            pButton.Left = pLeft;
         }
      }

      internal static void ToCenterOrNot(Form pForm, bool pControlBox = true) {
         bool centerHorizontal = true, centerVertical = true;
         Screen currentMonitor = Screen.FromControl(pForm),
#pragma warning disable CS8600
            primaryscreen = Screen.PrimaryScreen;
#pragma warning restore CS8600
         int ninetyHorizontal = (int)Math.Floor(currentMonitor.WorkingArea.Width * 0.9f),
           ninetyVertical = (int)Math.Floor(currentMonitor.WorkingArea.Height * 0.9f);
         EnsureWindowFitsMonitor(pForm);
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

      internal static void UpDownSelectAll(NumericUpDown pNumericUpDown) {
         pNumericUpDown.Focus();
         pNumericUpDown.Select(0, pNumericUpDown.Text.Length);
      }

#pragma warning disable IDE0028
      internal static List<Control> ControlCollectionAsList(Control.ControlCollection pControlCollection) {
         List<Control> returnValue = new List<Control>();
         foreach (Control control in pControlCollection)
            returnValue.Add(control);
         return returnValue;
      }
#pragma warning restore IDE0028

      internal static void PaintPanel(Panel? pPanel) {
         if (pPanel == null)
            return;
      }

      internal static void SetFontComboBoxWidth(ComboBox? pComboBox) {
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

      internal static void SetFontComboBoxDropDownWidth(ComboBox? pComboBox) {
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

      internal static void AdjustForThemeFont(Font pFont) {
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

      internal static void RecalculateAssociatedOffsets(Font pFont) {
         float fontSize = pFont.SizeInPoints;
         mGroupLeftPad = (int)Math.Ceiling(fontSize * 1.5f);
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
         mEm = (int)Math.Ceiling(fontSize);
         mEm2 = mEm * 2;
         mEm3 = mEm * 3;
         mEmHalf = (int)Math.Ceiling(mEm * 0.5f);
         mEmFifth = Math.Clamp(mEm / 5, 3, 14);
         mBottomPad = (int)Math.Ceiling(fontSize * 0.75f);
         mRightPad = (int)Math.Ceiling(fontSize * 0.75f);
         mMenuLeftOffset = (int)Math.Ceiling(fontSize * 2.5f);
         mGroupTopPad = (int)Math.Ceiling(fontSize * 0.2f);
         GroupBox groupBox = new GroupBox {
            Font = CreateNewFont(pFont),
            Text = mUnicodeSampleString,
            AutoSize = true
         };
         Panel panel = new Panel {
            Font = CreateNewFont(pFont),
            Dock = DockStyle.Fill
         };
         Label label = new Label {
            Font = CreateNewFont(pFont),
            Text = mUnicodeSampleString,
            AutoSize = true,
         };
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

      internal static void SizeGroupBox(GroupBox pGroupBox, bool pGroupPad = true) {
         int right = 0;
         int bottom = 0;
         int rightPad = pGroupPad ? mGroupRightPad : 0;
         int bottomPad = pGroupPad ? mGroupBottomPad : 0;
         int wide = mMenuLeftOffset;
         int menuCount = 0;
         SizeF stringSize = new SizeF(0, 0);
         using (Graphics graphics = pGroupBox.CreateGraphics()) {
            if (string.IsNullOrEmpty(pGroupBox.Text))
               stringSize = graphics.MeasureString(mUnicodeSampleString, pGroupBox.Font);
            else
               stringSize = graphics.MeasureString(mUnicodeSampleString + pGroupBox.Text, pGroupBox.Font);
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

      internal static void SizePanel(Panel pPanel, int pLeftPad = 10, bool pScrollbarPad = true) {
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

      internal static void SizePanelWidthToAccommodateBottomPanel(Panel pPanel, Button pRightmostButton) {
         if (pPanel.Width < (pRightmostButton.Right + mCancelOffset))
            pPanel.Width = pRightmostButton.Right + mCancelOffset;
         if (pPanel.Width > (int)(mMonitorSize.Width * 0.95f)) {
            pPanel.Width = (int)(mMonitorSize.Width * 0.95f);
            pPanel.AutoScroll = true;
         }
      }

      internal static void SizeTextBoxToFitString(out SizeF pSize, RichTextBox pTextBox,
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
               stringSize = graphics.MeasureString(mUnicodeSampleString, font);
         }
         if (pDoWidth) {
            if (pPadWidth)
               pSize.Width = stringSize.Width + mEm;
            else
               pSize.Width = stringSize.Width;
         }
         if (pDoHeight)
            pSize.Height = (int)Math.Ceiling(stringSize.Height * 1.3f);
      }

      internal static void SizeTextBoxToFitString(out SizeF pSize, TextBox pTextBox, string pExample = "",
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
               stringSize = graphics.MeasureString(mUnicodeSampleString, font);
         }
         if (pDoWidth) {
            if (pPadWidth)
               pSize.Width = stringSize.Width + mEm;
            else
               pSize.Width = stringSize.Width;
         }
         if (pDoHeight)
            pSize.Height = stringSize.Height;
      }

      internal static void TextBoxSelectAll(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.SelectAll();
      }

      internal static void ComboBoxSelectAll(ComboBox pComboBox) {
         pComboBox.Focus();
         pComboBox.SelectAll();
         pComboBox.DroppedDown = true;
      }

      public static Point GetGroupBoxFirstLineOffset(GroupBox pGroupBox) {
         SizeF stringSize = new SizeF();

         using (Graphics graphics = pGroupBox.CreateGraphics())
            stringSize = graphics.MeasureString(pGroupBox.Text + "Ñçg", pGroupBox.Font);
         return new Point(mGroupLeftPad, (int)stringSize.Height + mGroupTopPad + mScalingGroupBoxTopLinePad);
      }
   }
}
