namespace DBCode {
   public sealed partial class MainForm : Form {
      public void FontPickerHelp() {
         //try {
         //   if (!File.Exists(sFontPickerHelp)) {
         //      _ = AskingAsync(new TM("Missing Help File",
         //         string.Format("DBCode's Font Picker help HTML file" +
         //            ":{0}{1}{0}" +
         //            "could not be found" + "!{0}{0}" +
         //            "You may locate and restore the missing" + " " +
         //            "file then click the “Retry” button below" + ".{0}{0}" +
         //            "Otherwise, click “Cancel” to abort this help request" + ".",
         //            Environment.NewLine, sFontPickerHelp),
         //          string.Empty,
         //          "Cancel", TIMED_MESSAGE_Cancel_Ask,
         //          "Retry", TIMED_MESSAGE_Retry_Font_Picker_Help,
         //          string.Empty, TIMED_MESSAGE_Ignore,
         //          TIMED_MESSAGE_Cancel_Ask, 0));
         //      return;
         //   }
         //   Process process = new Process();
         //   process.StartInfo = new ProcessStartInfo(sFontPickerHelp) {
         //      UseShellExecute = true
         //   };
         //   process.Start();
         //   Thread.Sleep(100);
         //}
         //catch (Exception pException) {
         //   _ = AskingAsync(new TM("FontPickerHelp", pException));
         //}
      }

      private void LayoutFontPicker() {
         //if (mCurrentTheme == null)
         //   return;
         //Color interfaceBackgroundColor = mCurrentTheme.mGroupBoxBackgroundColor,
         //   interfaceFontColor = mCurrentTheme.mGroupBoxFontColor;
         //Font interfaceTextFont = mCurrentTheme.mInterfaceFont, usageFont = CreateNewFont();
         //float interfaceTextFontSize = mCurrentTheme.mInterfaceFont.SizeInPoints, usageFontSize = 14f;
         //FontFamily usageFontFamily;
         //FontStyle usageFontStyle = FontStyle.Regular;
         //List<string> fontNames = new List<string>();
         //int index = 0;
         //List<Control> controlList = new List<Control>();

         //RemoveFontPickerHandlers();
         //#region assignments
         //mPickFontUsageLabel?.Text = "Use this font for the " + string.Format("{0}", ToDescription((FontUsage)mFontUsage));
         //mEscapeFrom = EscapeFrom.FontPicker;
         ////DEBUG efm5 2026 03 30 implement
         //switch (mFontUsage) {
         //   case FontUsage.InterfaceFont:
         //      break;
         //   case FontUsage.MenuFont:
         //      break;
         //   case FontUsage.StatusBarFont:
         //      break;
         //   case FontUsage.TextBoxFont:
         //      //usageFont = sOptionsTheme.mStatusBarFont;
         //      //usageFontFamily = sOptionsTheme.mStatusBarFont.FontFamily;
         //      //usageFontSize = sOptionsTheme.mStatusBarFont.SizeInPoints;
         //      //usageFontStyle = sOptionsTheme.mStatusBarFont.Style;
         //      break;
         //   default:
         //      TimedMessage("The “FontUsage” was not recognized", "Coding ERROR");
         //      return;
         //}
         //foreach (FontFamily fontFamily in FontFamily.Families.ToList())
         //   fontNames.Add(fontFamily.Name);
         //mFontFamilyComboBox?.Items.AddRange(fontNames.ToArray());
         //mFontFamilyNameTextBox?.Text = usageFont.Name;
         //mFontSizeTextBox?.Text = string.Format("{0}", usageFontSize);
         //mFontSizeComboBox?.Text = string.Format("{0}", usageFontSize);
         //if ((usageFontStyle & FontStyle.Regular) == FontStyle.Regular) {
         //   mNormalStyleCheckBox?.Checked = true;
         //   mBoldStyleCheckBox?.Checked = false;
         //   mItalicsStyleCheckBox?.Checked = false;
         //   mUnderlineStyleCheckBox?.Checked = false;
         //   mStrikethroughStyleCheckBox?.Checked = false;
         //}
         //else if ((usageFontStyle & FontStyle.Bold) == FontStyle.Bold) {
         //   mNormalStyleCheckBox?.Checked = false;
         //   mBoldStyleCheckBox?.Checked = true;
         //}
         //else if ((usageFontStyle & FontStyle.Italic) == FontStyle.Italic) {
         //   mNormalStyleCheckBox?.Checked = false;
         //   mItalicsStyleCheckBox?.Checked = true;
         //}
         //else if ((usageFontStyle & FontStyle.Underline) == FontStyle.Underline) {
         //   mNormalStyleCheckBox?.Checked = false;
         //   mUnderlineStyleCheckBox?.Checked = true;
         //}
         //else if ((usageFontStyle & FontStyle.Strikeout) == FontStyle.Strikeout) {
         //   mNormalStyleCheckBox?.Checked = false;
         //   mStrikethroughStyleCheckBox?.Checked = true;
         //}
         //mFontDescriptionLabel?.Text = string.Format("Selected font: {0}; {1}pt; {2}",
         //         usageFont.Name, (int)Math.Ceiling(usageFontSize), usageFontStyle);
         //#endregion

         //#region fonts and colors
         //mPickFontPanel?.BackColor = interfaceBackgroundColor;
         //mPickFontTitleLabel?.Font = CreateNewTitleFont();
         //mPickFontUsageLabel?.Font = CreateNewFont(interfaceTextFont);
         //mFontDescriptionLabel?.Font = CreateNewFont(interfaceTextFont);
         //mFontFamilyNameTextBox?.Font = CreateNewFont(interfaceTextFont);
         //mFontSizeTextBox?.Font = CreateNewFont(interfaceTextFont);
         //mFontFamilyComboBox?.Font = CreateNewFont(interfaceTextFont);
         //mFontSizeComboBox?.Font = CreateNewFont(interfaceTextFont);
         //mFontStyleGroupBox?.Font = CreateNewFont(interfaceTextFont);
         //mFontStyleGroupBox?.ForeColor = mCurrentTheme.mGroupBoxFontColor;
         //mFontStyleGroupBox?.BackColor = mCurrentTheme.mGroupBoxBackgroundColor;
         //mFontSizePanel?.BackColor = mCurrentTheme.mGroupBoxBackgroundColor;
         //mFontSizePrefixButton?.Font = CreateNewFont(interfaceTextFont);
         //FlattenButton(mFontSizePrefixButton ?, mCurrentTheme.mGroupBoxBackgroundColor);
         //mFontSizeTextBox?.BackColor = mCurrentTheme.mInterfaceBackgroundColor;
         //mFontSizeDropDownPanel?.BackColor = mCurrentTheme.mGroupBoxBackgroundColor;
         //mFontSizeDropDownPrefixButton?.Font = CreateNewFont(interfaceTextFont);
         //FlattenButton(fontSizeDropDownPrefixButton,
         //   mCurrentTheme.mGroupBoxBackgroundColor);
         //mFontSizeComboBox?.BackColor = mCurrentTheme.mInterfaceBackgroundColor;

         //foreach (CheckBox checkbox in mFontStyleGroupBox?.Controls.OfType<CheckBox>()) {
         //   checkbox.Font = CreateNewFont(interfaceTextFont);
         //   checkbox.ForeColor = interfaceFontColor;
         //   checkbox.BackColor = Color.Transparent;
         //}
         //foreach (Button button in mPickFontPanel?.Controls.OfType<Button>()) {
         //   button.Font = CreateNewFont(interfaceTextFont);
         //   button.Font = CreateNewFont(interfaceTextFont);
         //   FlattenButton(button, interfaceBackgroundColor);
         //}
         //foreach (Label label in mPickFontPanel?.Controls.OfType<Label>()) {
         //   label.ForeColor = interfaceFontColor;
         //   label.BackColor = Color.Transparent;
         //}
         //foreach (Button button in mFontPickerBottomPanel?.Controls.OfType<Button>()) {
         //   button.Font = CreateNewFont(interfaceTextFont);
         //   button.ForeColor = mCurrentTheme.mInterfaceFontColor;
         //   button.BackColor = Color.Transparent;
         //}
         ////DEBUG efm5 2026 03 30 do not use a bottom panel?
         ////mFontPickerBottomPanel?.BackColor = mCurrentTheme.mBottomPanelBackgroundColor;
         //#endregion

         //#region location and size
         //foreach (string fontFamilyName in fontNames) {
         //   if (string.Equals(fontFamilyName, usageFont.Name,
         //      StringComparison.OrdinalIgnoreCase)) {
         //      break;
         //   }
         //   index++;
         //}
         //SetFontComboBoxWidth(fontFamilyComboBox);
         //SetFontComboBoxDropDownWidth(fontFamilyComboBox);
         //fontFamilyComboBox.SelectedIndex = index;
         //fontFamilyComboBox.Text = usageFont.Name;
         //fontFamilyComboBox.DisplayMember = usageFont.Name;
         //fontFamilyComboBox.DrawItem += FontFamilyComboBox_DrawItem;
         //fontFamilyComboBox.DropDownHeight = sComboBoxMaxDropdownHeight;
         //SizeTextBoxToFitString(out SizeF oSize, fontFamilyNameTextBox);
         //fontFamilyNameTextBox.Height = (int)oSize.Height;
         //SizeTextBoxToFitString(out oSize, fontSizeTextBox, "00000");
         //fontSizeTextBox.Size = SizeFromSizeF(oSize);
         //fontSizeComboBox.Size = fontSizeTextBox.Size;
         //fontSizeComboBox.Width += SystemInformation.VerticalScrollBarWidth;
         //SetBottomPanelHeight(fontPickerBottomPanel);
         //pickFontUsageLabel.Top = pickFontTitleLabel.Bottom + sWidgetBigVerticalOffset;
         //fontFamilyTextBoxPrefixButton.Top = pickFontUsageLabel.Bottom + sWidgetBigVerticalOffset;
         //fontFamilyNameTextBox.Location =
         //   new Point(fontFamilyTextBoxPrefixButton.Right + sAssociatedTextBoxPostButtonHorizontalSpace,
         //     fontFamilyTextBoxPrefixButton.Top + sAssociatedTextBoxPostButtonVerticalOffset);
         //fontFamilyDropDownPrefixButton.Top = Math.Max(fontFamilyTextBoxPrefixButton.Bottom,
         //   fontFamilyNameTextBox.Bottom);
         //fontFamilyComboBox.Location =
         //   new Point(fontFamilyDropDownPrefixButton.Right + sAssociatedTextBoxPostButtonHorizontalSpace,
         //   fontFamilyDropDownPrefixButton.Top + sAssociatedTextBoxPostButtonVerticalOffset);
         //controlList.Clear();
         //controlList.Add(pickFontUsageLabel);
         //controlList.Add(fontFamilyNameTextBox);
         //controlList.Add(fontFamilyComboBox);
         //fontStyleGroupBox.Location = new Point(Rightmost(controlList) + sWidgetBigHorizontalSpace,
         //   pickFontTitleLabel.Bottom + sWidgetBigVerticalOffset);
         //normalStyleCheckBox.Top = GetGroupBoxFirstLineOffset(fontStyleGroupBox);
         //boldStyleCheckBox.Top = normalStyleCheckBox.Bottom + sWidgetBigVerticalOffset;
         //italicsStyleCheckBox.Top = boldStyleCheckBox.Bottom + sWidgetVerticalOffset;
         //underlineStyleCheckBox.Top = italicsStyleCheckBox.Bottom + sWidgetVerticalOffset;
         //strikethroughStyleCheckBox.Top = underlineStyleCheckBox.Bottom + sWidgetVerticalOffset;
         //SizeGroupBox(fontStyleGroupBox);
         //fontSizePanel.Location = new Point(fontStyleGroupBox.Right + sWidgetBigHorizontalSpace,
         //   fontStyleGroupBox.Top);
         //fontSizeTextBox.Top = fontSizePrefixButton.Bottom + sWidgetVerticalOffset;
         //SizePanel(fontSizePanel);
         //fontSizeDropDownPanel.Location = new Point(fontSizePanel.Left,
         //   fontSizePanel.Bottom + sWidgetBigVerticalOffset);
         //fontSizeComboBox.Top = fontSizeDropDownPrefixButton.Bottom + sWidgetVerticalOffset;
         //SizePanel(fontSizeDropDownPanel);
         //controlList.Clear();
         //controlList.Add(fontFamilyDropDownPrefixButton);
         //controlList.Add(fontFamilyComboBox);
         //controlList.Add(fontStyleGroupBox);
         //controlList.Add(fontSizeDropDownPanel);
         //fontDescriptionLabel.Top = Bottommost(controlList) + sWidgetBigVerticalOffset;
         //fontPickerBottomPanel.Top = fontDescriptionLabel.Bottom + sWidgetBigVerticalOffset;
         //controlList.Clear();
         //controlList.Add(fontSizePanel);
         //controlList.Add(fontSizeDropDownPanel);
         //pickFontPanel.Size =
         //   new Size(Rightmost(controlList) + SystemInformation.VerticalScrollBarWidth,
         //     fontPickerBottomPanel.Bottom);
         //#endregion
         //CenterDialogPanel(pickFontPanel, sFontPickerBorderPanel);
         //RestoreFontPickerHandlers();
      }

      //private void ShowFontPicker(FontUsage pFontUsage) {
      //   //FadeOut();
      //   //fontFamilyComboBox.SelectedIndexChanged -= FontFamilyComboBox_SelectedIndexChanged;
      //   //fontSizeComboBox.SelectedIndexChanged -= FontSizeComboBox_SelectedIndexChanged;
      //   //sFontUsage = pFontUsage;
      //   //NewFontPickerBorderPanel();
      //   //PrepareOptionsDialog(EscapeFrom.FontPicker);
      //   //sFromFontPicker = true;
      //   //pickFontPanel.Size = SizeFromFloats(Size.Width * 0.8f, (Size.Height * 0.8f) - sTitleBarHeight);
      //   //pickFontTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      //   //fontPickerBottomPanel.Dock = DockStyle.None;
      //   //LayoutFontPicker();
      //   //ShowPanel(sFontPickerBorderPanel);
      //   //fontPickerCancelButton.Left = fontPickerBottomPanel.Width - fontPickerCancelButton.Width - sCancelOffset;
      //   //fontPickerOkButton.Left = fontPickerCancelButton.Left - fontPickerOkButton.Width - sOkOffset;
      //   //fontPickerCancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      //   //fontPickerOkButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      //   //fontPickerBottomPanel.Dock = DockStyle.Bottom;
      //   //CenterControlHorizontally(pickFontPanel, pickFontTitleLabel);
      //   //TextBoxSelectAll(fontFamilyNameTextBox);
      //   //fontSizeComboBox.SelectedIndexChanged += FontSizeComboBox_SelectedIndexChanged;
      //   //fontFamilyComboBox.SelectedIndexChanged += FontFamilyComboBox_SelectedIndexChanged;
      //   //FadeIn();
      //}

      //private void HideFontPicker(bool pFadeIn = true) {
      //   //FadeOut();
      //   //HidePanel(sFontPickerBorderPanel);
      //   //RestoreOptions();
      //   //sFromFontPicker = false;
      //   //if (pFadeIn)
      //   //   FadeIn();
      //}

      //private void NewFontPickerBorderPanel() {
      //   //sFontPickerBorderPanel ??= new BorderFrame(pickFontPanel, sForm) {
      //   //   Name = "sFontPickerBorderPanel",
      //   //   TabIndex = sTabIndex++
      //   //};
      //}

      //private static void SetFontComboBoxWidth(ComboBox pComboBox) {
      //   //float boxWidth = 30f, boxHeight = 30f;
      //   //SizeF stringSize = new SizeF();

      //   //using (Graphics graphics = pComboBox.CreateGraphics()) {
      //   //   foreach (string fontName in pComboBox.Items) {
      //   //      //string fontName = ((FontFamily)item).Name;
      //   //      if (!String.IsNullOrEmpty(fontName)) {
      //   //         stringSize = graphics.MeasureString(fontName, pComboBox.Font);
      //   //         if (stringSize.Width > boxWidth)
      //   //            boxWidth = stringSize.Width;
      //   //         if (stringSize.Height > boxHeight)
      //   //            boxHeight = stringSize.Height;
      //   //      }
      //   //   }
      //   //}
      //   //pComboBox.Width = (int)(boxWidth + SystemInformation.VerticalScrollBarWidth);
      //   //pComboBox.Height = (int)(boxHeight + sIndent);
      //}

      //private static void SetFontComboBoxDropDownWidth(ComboBox pComboBox) {
      //if (pComboBox.Items.Count == 0)
      //   return;//don't change the width
      //float boxWidth = 30f;
      //SizeF stringSize = new SizeF();

      //using (Graphics graphics = pComboBox.CreateGraphics()) {
      //   foreach (string fontName in pComboBox.Items) {
      //      //string fontName = ((FontFamily)item).Name;
      //      if (!String.IsNullOrEmpty(fontName)) {
      //         stringSize = graphics.MeasureString(fontName, pComboBox.Font);
      //         if (stringSize.Width > boxWidth)
      //            boxWidth = stringSize.Width;
      //      }
      //   }
      //}
      //if (boxWidth > pComboBox.Width) {
      //   if (boxWidth > sComboBoxMaxDropdownWidth)
      //      boxWidth = sComboBoxMaxDropdownWidth;
      //   pComboBox.DropDownWidth = (int)boxWidth;
      //}
      //}

      private FontStyle GetFontStyle() {
         return FontStyle.Regular;//DEBUG efm5 2026 03 30 temporary
         //FontStyle style = FontStyle.Regular;

         //if (normalStyleCheckBox.Checked)
         //   return style;
         //else {
         //   if (boldStyleCheckBox.Checked)
         //      style |= FontStyle.Bold;
         //   if (italicsStyleCheckBox.Checked)
         //      style |= FontStyle.Italic;
         //   if (underlineStyleCheckBox.Checked)
         //      style |= FontStyle.Underline;
         //   if (strikethroughStyleCheckBox.Checked)
         //      style |= FontStyle.Strikeout;
         //   return style;
         //}
      }

      private void StyleHandlersOn() {
         //normalStyleCheckBox.Click += NormalStyleCheckBox_Click;
         //boldStyleCheckBox.Click += BoldStyleCheckBox_CheckedChanged;
         //italicsStyleCheckBox.Click += ItalicsStyleCheckBox_Click;
         //underlineStyleCheckBox.Click += UnderlineStyleCheckBox_Click;
         //strikethroughStyleCheckBox.Click += StrikethroughStyleCheckBox_Click;
      }

      private void StyleHandlersOff() {
         //normalStyleCheckBox.Click -= NormalStyleCheckBox_Click;
         //boldStyleCheckBox.Click -= BoldStyleCheckBox_CheckedChanged;
         //italicsStyleCheckBox.Click -= ItalicsStyleCheckBox_Click;
         //underlineStyleCheckBox.Click -= UnderlineStyleCheckBox_Click;
         //strikethroughStyleCheckBox.Click -= StrikethroughStyleCheckBox_Click;
      }

      private void MaybeRegularStyle() {
         //if ((boldStyleCheckBox.Checked == false) &&
         //(italicsStyleCheckBox.Checked == false) &&
         //(underlineStyleCheckBox.Checked == false) &&
         //(strikethroughStyleCheckBox.Checked == false))
         //   normalStyleCheckBox.Checked = true;
      }

      private void RemoveFontPickerHandlers() {
         //fontFamilyNameTextBox.TextChanged -= (FontFamilyNameTextBox_TextChanged);
         //fontFamilyComboBox.SelectedIndexChanged -= (FontFamilyComboBox_SelectedIndexChanged);
         //fontSizeTextBox.TextChanged -= (FontSizeTextBox_TextChanged);
         //fontSizeComboBox.SelectedIndexChanged -= (FontSizeComboBox_SelectedIndexChanged);
         //normalStyleCheckBox.Click -= (NormalStyleCheckBox_Click);
         //boldStyleCheckBox.CheckedChanged -= (BoldStyleCheckBox_CheckedChanged);
         //underlineStyleCheckBox.Click -= (UnderlineStyleCheckBox_Click);
         //italicsStyleCheckBox.Click -= (ItalicsStyleCheckBox_Click);
         //strikethroughStyleCheckBox.Click -= (StrikethroughStyleCheckBox_Click);
      }

      private void RestoreFontPickerHandlers() {
         //fontFamilyNameTextBox.TextChanged += (FontFamilyNameTextBox_TextChanged);
         //fontFamilyComboBox.SelectedIndexChanged += (FontFamilyComboBox_SelectedIndexChanged);
         //fontSizeTextBox.TextChanged += (FontSizeTextBox_TextChanged);
         //fontSizeComboBox.SelectedIndexChanged += (FontSizeComboBox_SelectedIndexChanged);
         //normalStyleCheckBox.Click += (NormalStyleCheckBox_Click);
         //boldStyleCheckBox.CheckedChanged += (BoldStyleCheckBox_CheckedChanged);
         //underlineStyleCheckBox.Click += (UnderlineStyleCheckBox_Click);
         //italicsStyleCheckBox.Click += (ItalicsStyleCheckBox_Click);
         //strikethroughStyleCheckBox.Click += (StrikethroughStyleCheckBox_Click);
      }
   }
}
