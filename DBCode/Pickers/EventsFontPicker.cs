namespace DBCode {
   public sealed partial class MainForm : Form {
      private void FontFamilyTextBoxPrefixButton_Click(object? pSender, EventArgs pE) {
         //TextBoxSelectAll(mFontFamilyNameTextBox);
      }

      private void FontFamilyDropDownPrefixButton_Click(object? pSender, EventArgs pE) {
         //fontFamilyComboBox.Focus();
         //fontFamilyComboBox.DroppedDown = true;
      }

      private void FontSizePrefixButton_Click(object? pSender, EventArgs pE) {
         //TextBoxSelectAll(fontSizeTextBox);
      }

      private void FontSizeDropDownPrefixButton_Click(object? pSender, EventArgs pE) {
         //fontSizeComboBox.Focus();
         //fontSizeComboBox.DroppedDown = true;
      }

      private void NormalStyleCheckBox_Click(object? pSender, EventArgs pE) {
         //if (normalStyleCheckBox.Checked) {
         //   StyleHandlersOff();
         //   boldStyleCheckBox.Checked = false;
         //   italicsStyleCheckBox.Checked = false;
         //   underlineStyleCheckBox.Checked = false;
         //   strikethroughStyleCheckBox.Checked = false;
         //   StyleHandlersOn();
         //}
      }

      private void BoldStyleCheckBox_CheckedChanged(object? pSender, EventArgs pE) {
         //StyleHandlersOff();
         //if (boldStyleCheckBox.Checked)
         //   normalStyleCheckBox.Checked = false;
         //else
         //   MaybeRegularStyle();
         //StyleHandlersOn();
      }

      private void ItalicsStyleCheckBox_Click(object? pSender, EventArgs pE) {
         //StyleHandlersOff();
         //if (italicsStyleCheckBox.Checked)
         //   normalStyleCheckBox.Checked = false;
         //else
         //   MaybeRegularStyle();
         //StyleHandlersOn();
      }

      private void UnderlineStyleCheckBox_Click(object? pSender, EventArgs pE) {
         //StyleHandlersOff();
         //if (underlineStyleCheckBox.Checked)
         //   normalStyleCheckBox.Checked = false;
         //else
         //   MaybeRegularStyle();
         //StyleHandlersOn();
      }

      private void StrikethroughStyleCheckBox_Click(object? pSender, EventArgs pE) {
         //StyleHandlersOff();
         //if (strikethroughStyleCheckBox.Checked)
         //   normalStyleCheckBox.Checked = false;
         //else
         //   MaybeRegularStyle();
         //StyleHandlersOn();
      }

      private void FontFamilyNameTextBox_TextChanged(object? pSender, EventArgs pE) {
         //fontFamilyNameTextBox.Text = fontFamilyNameTextBox.Text.Trim();
         //if (fontFamilyComboBox.Items.Contains(fontFamilyNameTextBox.Text)) {
         //   fontFamilyComboBox.Text = fontFamilyNameTextBox.Text;
         //}
         //else {
         //   _ = AskingAsync(new TM("That family name is not an installed font name.", "Unrecognized Font Family"));
         //   TextBoxSelectAll(fontFamilyNameTextBox);
         //}
      }

      private void FontFamilyComboBox_SelectedIndexChanged(object? pSender, EventArgs pE) {
         //if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
         //   string family = fontFamilyComboBox.SelectedItem.ToString();

         //   family = family.Replace("[FontFamily: Name=", string.Empty);
         //   family = family.Replace("]", string.Empty);
         //   fontDescriptionLabel.Text = string.Format("Selected font: {0}; {1}pt; {2}", family, oSize, GetFontStyle());
         //   fontFamilyNameTextBox.Text = family;
         //   fontFamilyNameTextBox.Refresh();
         //}
         //else
         //   fontDescriptionLabel.Text = string.Format("Selected font: {0} (size undetermined) {1}",
         //      fontFamilyComboBox.DisplayMember, GetFontStyle());
         //fontDescriptionLabel.Refresh();
      }

      private void FontSizeComboBox_SelectedIndexChanged(object? pSender, EventArgs pE) {
         //string originalFontSize = fontSizeTextBox.Text;

         //fontSizeTextBox.Text = fontSizeComboBox.Text;
         //if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
         //   string family = fontFamilyComboBox.SelectedItem.ToString();

         //   family = family.Replace("[FontFamily: Name=", string.Empty);
         //   family = family.Replace("]", string.Empty);
         //   fontDescriptionLabel.Text = string.Format("Selected font: {0}; {1}pt; {2}", family, oSize, GetFontStyle());
         //}
         //else {
         //   fontSizeTextBox.Text = originalFontSize;
         //   fontDescriptionLabel.Text = string.Format("Selected font: {0} (size undetermined) {1}",
         //      fontFamilyComboBox.DisplayMember, GetFontStyle());
         //   _ = AskingAsync(new TM("The font size could not be parsed.", "Parse ERROR"));
         //   ComboBoxSelectAll(fontSizeComboBox);
         //}
         //fontDescriptionLabel.Refresh();
         //fontSizeTextBox.Refresh();
      }

      private void FontSizeTextBox_TextChanged(object? pSender, EventArgs pE) {
         //string originalFontSize = fontFamilyComboBox.Text;

         //if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
         //   string family = fontFamilyComboBox.SelectedItem.ToString();

         //   family = family.Replace("[FontFamily: Name=", string.Empty);
         //   family = family.Replace("]", string.Empty);
         //   fontDescriptionLabel.Text = string.Format("Selected font: {0}; {1}pt; {2}", family, oSize, GetFontStyle());
         //   if (!fontSizeComboBox.Items.Contains(fontSizeTextBox.Text))
         //      fontSizeComboBox.Items.Add(fontSizeTextBox.Text);
         //   fontSizeComboBox.Text = fontSizeTextBox.Text;
         //}
         //else {
         //   fontSizeTextBox.Text = originalFontSize;
         //   fontDescriptionLabel.Text = string.Format("Selected font: {0} (size undetermined) {1}",
         //      fontFamilyComboBox.DisplayMember, GetFontStyle());
         //   _ = AskingAsync(new TM("The font size could not be parsed.", "Parse ERROR"));
         //   TextBoxSelectAll(fontSizeTextBox);
         //}
         //fontDescriptionLabel.Refresh();
         //fontSizeTextBox.Refresh();
      }

      private void FontFamilyComboBox_DrawItem(object? pSender, DrawItemEventArgs pE) {
         //ComboBox comboBox = (ComboBox)sender;
         //string fontFamily = (string)comboBox.Items[e.Index];
         //Font font = new Font(fontFamily, comboBox.Font.SizeInPoints);

         //e.DrawBackground();
         //e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
      }

      private void FontPickerHelpButton_Click(object? pSender, EventArgs pE) {
         FontPickerHelp();
      }

      private void FontPickerCancelButton_Click(object? pSender, EventArgs pE) {
         HideFontPicker();
      }

      private void FontPickerOkButton_Click(object? pSender, EventArgs pE) {
         //if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
         //   string family = fontFamilyNameTextBox.Text;
         //   Font font = new Font(family, oSize, GetFontStyle());

         //   switch (sFontUsage) {
         //      case FontUsage.AddressTextBoxFont:
         //         sOptionsTheme.mAddressTextBoxFont = CreateNewFont(font);
         //         optionsAddressFontLabel.Text = string.Format("Address font is: {0} {1}pt {2}",
         //            sOptionsTheme.mAddressTextBoxFont.Name, (int)sOptionsTheme.mAddressTextBoxFont.SizeInPoints,
         //            sOptionsTheme.mAddressTextBoxFont.Style);
         //         break;
         //      case FontUsage.AddressTextBoxFontThemeEditor:
         //         sEditingTheme.mAddressTextBoxFont = CreateNewFont(font);
         //         themeEditorAddressTextBoxFontLabel.Text = string.Format("Address font is: {0} {1}pt {2}",
         //            sEditingTheme.mAddressTextBoxFont.Name, (int)sEditingTheme.mAddressTextBoxFont.SizeInPoints,
         //            sEditingTheme.mAddressTextBoxFont.Style);
         //         break;
         //      case FontUsage.DataGridCellFont:
         //         sOptionsTheme.mDataGridCellFont = CreateNewFont(font);
         //         optionsDetailCellFontLabel.Text = string.Format("Data Grid Cell font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDataGridCellFont.Name, (int)sOptionsTheme.mDataGridCellFont.SizeInPoints,
         //            sOptionsTheme.mDataGridCellFont.Style);
         //         break;
         //      case FontUsage.DataGridCellFontThemeEditor:
         //         sEditingTheme.mDataGridCellFont = CreateNewFont(font);
         //         themeEditorDataGridCellFontLabel.Text = string.Format("Data Grid Cell font is: {0} {1}pt {2}",
         //            sEditingTheme.mDataGridCellFont.Name, (int)sEditingTheme.mDataGridCellFont.SizeInPoints,
         //            sEditingTheme.mDataGridCellFont.Style);
         //         break;
         //      case FontUsage.DataGridHeaderFont:
         //         sOptionsTheme.mDataGridHeaderFont = CreateNewFont(font);
         //         optionsDataGridHeaderFontLabel.Text = string.Format("Data Grid Header font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDataGridHeaderFont.Name, (int)sOptionsTheme.mDataGridHeaderFont.SizeInPoints,
         //            sOptionsTheme.mDataGridHeaderFont.Style);
         //         break;
         //      case FontUsage.DataGridHeaderFontThemeEditor:
         //         sEditingTheme.mDataGridHeaderFont = CreateNewFont(font);
         //         themeEditorDataGridHeaderFontLabel.Text = string.Format("Data Grid Header font is: {0} {1}pt {2}",
         //            sEditingTheme.mDataGridHeaderFont.Name, (int)sEditingTheme.mDataGridHeaderFont.SizeInPoints,
         //            sEditingTheme.mDataGridHeaderFont.Style);
         //         break;
         //      case FontUsage.DeskFont:
         //         sOptionsTheme.mDeskFont = CreateNewFont(font);
         //         optionsDeskFontLabel.Text = string.Format("Desk View font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDeskFont.Name, (int)sOptionsTheme.mDeskFont.SizeInPoints,
         //            sOptionsTheme.mDeskFont.Style);
         //         break;
         //      case FontUsage.DeskFontThemeEditor:
         //         sEditingTheme.mDeskFont = CreateNewFont(font);
         //         themeEditorDeskFontLabel.Text = string.Format("Desk View font is: {0} {1}pt {2}",
         //            sEditingTheme.mDeskFont.Name, (int)sEditingTheme.mDeskFont.SizeInPoints,
         //            sEditingTheme.mDeskFont.Style);
         //         break;
         //      case FontUsage.DisplayPanelFont:
         //         sOptionsTheme.mDisplayPanelFont = CreateNewFont(font);
         //         optionsDisplayPanelFontLabel.Text = string.Format("Display Panel font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDisplayPanelFont.Name, (int)sOptionsTheme.mDisplayPanelFont.SizeInPoints,
         //            sOptionsTheme.mDisplayPanelFont.Style);
         //         break;
         //      case FontUsage.DisplayPanelFontThemeEditor:
         //         sEditingTheme.mDisplayPanelFont = CreateNewFont(font);
         //         themeEditorDisplayPanelFontLabel.Text = string.Format("Display Panel font is: {0} {1}pt {2}",
         //            sEditingTheme.mDisplayPanelFont.Name, (int)sEditingTheme.mDisplayPanelFont.SizeInPoints,
         //            sEditingTheme.mDisplayPanelFont.Style);
         //         break;
         //      case FontUsage.IconPanelFont:
         //         sOptionsTheme.mIconFont = CreateNewFont(font);
         //         optionsIconFontLabel.Text = string.Format("Icon View font is: {0} {1}pt {2}",
         //            sOptionsTheme.mIconFont.Name, (int)sOptionsTheme.mIconFont.SizeInPoints,
         //            sOptionsTheme.mIconFont.Style);
         //         break;
         //      case FontUsage.IconPanelFontThemeEditor:
         //         sEditingTheme.mIconFont = CreateNewFont(font);
         //         themeEditorIconPanelFontLabel.Text = string.Format("Icon View font is: {0} {1}pt {2}",
         //            sEditingTheme.mIconFont.Name, (int)sEditingTheme.mIconFont.SizeInPoints,
         //            sEditingTheme.mIconFont.Style);
         //         break;
         //      case FontUsage.InterfaceFont:
         //         sOptionsTheme.mInterfaceFont = CreateNewFont(font);
         //         optionsInterfaceFontLabel.Text = string.Format("Interface font is: {0} {1}pt {2}",
         //            sOptionsTheme.mInterfaceFont.Name, (int)sOptionsTheme.mInterfaceFont.SizeInPoints,
         //            sOptionsTheme.mInterfaceFont.Style);
         //         RecalculateAssociatedOffsets(sEditingTheme.mInterfaceFont);
         //         break;
         //      case FontUsage.InterfaceFontThemeEditor:
         //         sEditingTheme.mInterfaceFont = CreateNewFont(font);
         //         themeEditorInterfaceFontLabel.Text = string.Format("Interface font is: {0} {1}pt {2}",
         //            sEditingTheme.mInterfaceFont.Name, (int)sEditingTheme.mInterfaceFont.SizeInPoints,
         //            sEditingTheme.mInterfaceFont.Style);
         //         RecalculateAssociatedOffsets(sEditingTheme.mInterfaceFont);
         //         break;
         //      case FontUsage.MenuFont:
         //         sOptionsTheme.mMenuFont = CreateNewFont(font);
         //         optionsMenuFontLabel.Text = string.Format("Menu font is: {0} {1}pt {2}",
         //            sOptionsTheme.mMenuFont.Name, (int)sOptionsTheme.mMenuFont.SizeInPoints,
         //            sOptionsTheme.mMenuFont.Style);
         //         break;
         //      case FontUsage.MenuFontThemeEditor:
         //         sEditingTheme.mMenuFont = CreateNewFont(font);
         //         themeEditorMenuFontLabel.Text = string.Format("Menu font is: {0} {1}pt {2}",
         //            sEditingTheme.mMenuFont.Name, (int)sEditingTheme.mMenuFont.SizeInPoints,
         //            sEditingTheme.mMenuFont.Style);
         //         break;
         //      case FontUsage.PreviewTextFont:
         //         sOptionsTheme.mPreviewFont = CreateNewFont(font);
         //         optionsPreviewFontLabel.Text = string.Format("Preview font is: {0} {1}pt {2}",
         //            sOptionsTheme.mPreviewFont.Name, (int)sOptionsTheme.mPreviewFont.SizeInPoints,
         //            sOptionsTheme.mPreviewFont.Style);
         //         break;
         //      case FontUsage.PreviewTextFontThemeEditor:
         //         sEditingTheme.mPreviewFont = CreateNewFont(font);
         //         themeEditorPreviewFontLabel.Text = string.Format("Preview font is: {0} {1}pt {2}",
         //            sEditingTheme.mPreviewFont.Name, (int)sEditingTheme.mPreviewFont.SizeInPoints,
         //            sEditingTheme.mPreviewFont.Style);
         //         break;
         //      case FontUsage.SidebarFont:
         //         sOptionsTheme.mSidebarFont = CreateNewFont(font);
         //         optionsSidebarFontLabel.Text = string.Format("Sidebar font is: {0} {1}pt {2}",
         //            sOptionsTheme.mSidebarFont.Name, (int)sOptionsTheme.mSidebarFont.SizeInPoints,
         //            sOptionsTheme.mSidebarFont.Style);
         //         break;
         //      case FontUsage.SidebarFontThemeEditor:
         //         sEditingTheme.mSidebarFont = CreateNewFont(font);
         //         themeEditorSidebarFontLabel.Text = string.Format("Sidebar font is: {0} {1}pt {2}",
         //            sEditingTheme.mSidebarFont.Name, (int)sEditingTheme.mSidebarFont.SizeInPoints,
         //            sEditingTheme.mSidebarFont.Style);
         //         break;
         //      case FontUsage.StatusBarFont:
         //         sOptionsTheme.mStatusBarFont = CreateNewFont(font);
         //         themeEditorStatusBarFontLabel.Text = string.Format("Status Bar font is: {0} {1}pt {2}",
         //            sEditingTheme.mStatusBarFont.Name, (int)sEditingTheme.mStatusBarFont.SizeInPoints,
         //            sEditingTheme.mStatusBarFont.Style);
         //         break;
         //      case FontUsage.StatusBarFontThemeEditor:
         //         sEditingTheme.mStatusBarFont = CreateNewFont(font);
         //         themeEditorStatusBarFontLabel.Text = string.Format("Status Bar font is: {0} {1}pt {2}",
         //            sEditingTheme.mStatusBarFont.Name, (int)sEditingTheme.mStatusBarFont.SizeInPoints,
         //            sEditingTheme.mStatusBarFont.Style);
         //         break;
         //      case FontUsage.ToolbarFont:
         //         sOptionsTheme.mToolbarFont = CreateNewFont(font);
         //         optionsToolbarFontLabel.Text = string.Format("Toolbar font is: {0} {1}pt {2}",
         //            sOptionsTheme.mToolbarFont.Name, (int)sOptionsTheme.mToolbarFont.SizeInPoints,
         //            sOptionsTheme.mToolbarFont.Style);
         //         break;
         //      case FontUsage.ToolbarFontThemeEditor:
         //         sEditingTheme.mToolbarFont = CreateNewFont(font);
         //         themeEditorToolbarFontLabel.Text = string.Format("Toolbar font is: {0} {1}pt {2}",
         //            sEditingTheme.mToolbarFont.Name, (int)sEditingTheme.mToolbarFont.SizeInPoints,
         //            sEditingTheme.mToolbarFont.Style);
         //         break;
         //      case FontUsage.EverythingFont:
         //         sOptionsTheme.mAddressTextBoxFont = CreateNewFont(font);
         //         optionsAddressFontLabel.Text = string.Format("Address font is: {0} {1}pt {2}",
         //            sOptionsTheme.mAddressTextBoxFont.Name, (int)sOptionsTheme.mAddressTextBoxFont.SizeInPoints,
         //            sOptionsTheme.mAddressTextBoxFont.Style);
         //         sOptionsTheme.mDataGridCellFont = CreateNewFont(font);
         //         optionsDetailCellFontLabel.Text = string.Format("Data Grid Cell font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDataGridCellFont.Name, (int)sOptionsTheme.mDataGridCellFont.SizeInPoints,
         //            sOptionsTheme.mDataGridCellFont.Style);
         //         sOptionsTheme.mDataGridHeaderFont = CreateNewFont(font);
         //         optionsDataGridHeaderFontLabel.Text = string.Format("Data Grid Header font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDataGridHeaderFont.Name, (int)sOptionsTheme.mDataGridHeaderFont.SizeInPoints,
         //            sOptionsTheme.mDataGridHeaderFont.Style);
         //         sOptionsTheme.mDeskFont = CreateNewFont(font);
         //         optionsDeskFontLabel.Text = string.Format("Desk View font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDeskFont.Name, (int)sOptionsTheme.mDeskFont.SizeInPoints,
         //            sOptionsTheme.mDeskFont.Style);
         //         sOptionsTheme.mDisplayPanelFont = CreateNewFont(font);
         //         optionsDisplayPanelFontLabel.Text = string.Format("Display Panel font is: {0} {1}pt {2}",
         //            sOptionsTheme.mDisplayPanelFont.Name, (int)sOptionsTheme.mDisplayPanelFont.SizeInPoints,
         //            sOptionsTheme.mDisplayPanelFont.Style);
         //         sOptionsTheme.mIconFont = CreateNewFont(font);
         //         optionsIconFontLabel.Text = string.Format("Icon View font is: {0} {1}pt {2}",
         //            sOptionsTheme.mIconFont.Name, (int)sOptionsTheme.mIconFont.SizeInPoints,
         //            sOptionsTheme.mIconFont.Style);
         //         sOptionsTheme.mInterfaceFont = CreateNewFont(font);
         //         optionsInterfaceFontLabel.Text = string.Format("Interface font is: {0} {1}pt {2}",
         //            sOptionsTheme.mInterfaceFont.Name, (int)sOptionsTheme.mInterfaceFont.SizeInPoints,
         //            sOptionsTheme.mInterfaceFont.Style);
         //         RecalculateAssociatedOffsets(sOptionsTheme.mInterfaceFont);
         //         sOptionsTheme.mMenuFont = CreateNewFont(font);
         //         optionsMenuFontLabel.Text = string.Format("Menu font is: {0} {1}pt {2}",
         //            sOptionsTheme.mMenuFont.Name, (int)sOptionsTheme.mMenuFont.SizeInPoints,
         //            sOptionsTheme.mMenuFont.Style);
         //         //sOptionsTheme.mPreviewFont = CreateNewFont(font);
         //         //optionsPreviewFontLabel.Text = string.Format("Preview font is: {0} {1}pt {2}",
         //         //   sOptionsTheme.mPreviewFont.Name, (int)sOptionsTheme.mPreviewFont.SizeInPoints,
         //         //   sOptionsTheme.mPreviewFont.Style);
         //         sOptionsTheme.mSidebarFont = CreateNewFont(font);
         //         optionsSidebarFontLabel.Text = string.Format("Sidebar font is: {0} {1}pt {2}",
         //            sOptionsTheme.mSidebarFont.Name, (int)sOptionsTheme.mSidebarFont.SizeInPoints,
         //            sOptionsTheme.mSidebarFont.Style);
         //         sOptionsTheme.mStatusBarFont = CreateNewFont(font);
         //         themeEditorStatusBarFontLabel.Text = string.Format("Status Bar font is: {0} {1}pt {2}",
         //            sEditingTheme.mStatusBarFont.Name, (int)sEditingTheme.mStatusBarFont.SizeInPoints,
         //            sEditingTheme.mStatusBarFont.Style);
         //         sOptionsTheme.mToolbarFont = CreateNewFont(font);
         //         optionsToolbarFontLabel.Text = string.Format("Toolbar font is: {0} {1}pt {2}",
         //            sOptionsTheme.mToolbarFont.Name, (int)sOptionsTheme.mToolbarFont.SizeInPoints,
         //            sOptionsTheme.mToolbarFont.Style);
         //         break;
         //      case FontUsage.EverythingFontThemeEditor:
         //         sEditingTheme.mAddressTextBoxFont = CreateNewFont(font);
         //         themeEditorAddressTextBoxFontLabel.Text = string.Format("Address font is: {0} {1}pt {2}",
         //            sEditingTheme.mAddressTextBoxFont.Name, (int)sEditingTheme.mAddressTextBoxFont.SizeInPoints,
         //            sEditingTheme.mAddressTextBoxFont.Style);
         //         sEditingTheme.mDataGridCellFont = CreateNewFont(font);
         //         themeEditorDataGridCellFontLabel.Text = string.Format("Data Grid Cell font is: {0} {1}pt {2}",
         //            sEditingTheme.mDataGridCellFont.Name, (int)sEditingTheme.mDataGridCellFont.SizeInPoints,
         //            sEditingTheme.mDataGridCellFont.Style);
         //         sEditingTheme.mDataGridHeaderFont = CreateNewFont(font);
         //         themeEditorDataGridHeaderFontLabel.Text = string.Format("Data Grid Header font is: {0} {1}pt {2}",
         //            sEditingTheme.mDataGridHeaderFont.Name, (int)sEditingTheme.mDataGridHeaderFont.SizeInPoints,
         //            sEditingTheme.mDataGridHeaderFont.Style);
         //         sEditingTheme.mDeskFont = CreateNewFont(font);
         //         themeEditorDeskFontLabel.Text = string.Format("Desk View font is: {0} {1}pt {2}",
         //            sEditingTheme.mDeskFont.Name, (int)sEditingTheme.mDeskFont.SizeInPoints,
         //            sEditingTheme.mDeskFont.Style);
         //         sEditingTheme.mDisplayPanelFont = CreateNewFont(font);
         //         themeEditorDisplayPanelFontLabel.Text = string.Format("Display Panel font is: {0} {1}pt {2}",
         //            sEditingTheme.mDisplayPanelFont.Name, (int)sEditingTheme.mDisplayPanelFont.SizeInPoints,
         //            sEditingTheme.mDisplayPanelFont.Style);
         //         sEditingTheme.mIconFont = CreateNewFont(font);
         //         themeEditorIconPanelFontLabel.Text = string.Format("Icon View font is: {0} {1}pt {2}",
         //            sEditingTheme.mIconFont.Name, (int)sEditingTheme.mIconFont.SizeInPoints,
         //            sEditingTheme.mIconFont.Style);
         //         sEditingTheme.mInterfaceFont = CreateNewFont(font);
         //         themeEditorInterfaceFontLabel.Text = string.Format("Interface font is: {0} {1}pt {2}",
         //            sEditingTheme.mInterfaceFont.Name, (int)sEditingTheme.mInterfaceFont.SizeInPoints,
         //            sEditingTheme.mInterfaceFont.Style);
         //         RecalculateAssociatedOffsets(sEditingTheme.mInterfaceFont);
         //         sEditingTheme.mMenuFont = CreateNewFont(font);
         //         themeEditorMenuFontLabel.Text = string.Format("Menu font is: {0} {1}pt {2}",
         //            sEditingTheme.mMenuFont.Name, (int)sEditingTheme.mMenuFont.SizeInPoints,
         //            sEditingTheme.mMenuFont.Style);
         //         //sEditingTheme.mPreviewFont = CreateNewFont(font);
         //         //themeEditorPreviewFontLabel.Text = string.Format("Preview font is: {0} {1}pt {2}",
         //         //   sEditingTheme.mPreviewFont.Name, (int)sEditingTheme.mPreviewFont.SizeInPoints,
         //         //   sEditingTheme.mPreviewFont.Style);
         //         sEditingTheme.mSidebarFont = CreateNewFont(font);
         //         themeEditorSidebarFontLabel.Text = string.Format("Sidebar font is: {0} {1}pt {2}",
         //            sEditingTheme.mSidebarFont.Name, (int)sEditingTheme.mSidebarFont.SizeInPoints,
         //            sEditingTheme.mSidebarFont.Style);
         //         sEditingTheme.mStatusBarFont = CreateNewFont(font);
         //         themeEditorStatusBarFontLabel.Text = string.Format("Status Bar font is: {0} {1}pt {2}",
         //            sEditingTheme.mStatusBarFont.Name, (int)sEditingTheme.mStatusBarFont.SizeInPoints,
         //            sEditingTheme.mStatusBarFont.Style);
         //         sEditingTheme.mToolbarFont = CreateNewFont(font);
         //         themeEditorToolbarFontLabel.Text = string.Format("Toolbar font is: {0} {1}pt {2}",
         //            sEditingTheme.mToolbarFont.Name, (int)sEditingTheme.mToolbarFont.SizeInPoints,
         //            sEditingTheme.mToolbarFont.Style);
         //         break;
         //   }
         //}
         //HideFontPicker();
      }
   }
}