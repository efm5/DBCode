namespace DBCode {
   public partial class MainForm : Form {
      #region Event Handlers
      #region coding menu
      private void CommentGetTSMI_Click(object? pSender, EventArgs pE) {
         GetComment();
      }

      private void CommentReplaceTSMI_Click(object? pSender, EventArgs pE) {
         ReplaceComment();
      }

      private void CommentAddDoubleTSMI_Click(object? pSender, EventArgs pE) {
         AddDouble();
      }

      private void CommentAddTripleTSMI_Click(object? pSender, EventArgs pE) {
         AddTriple();
      }

      private void MakeSummaryTSMI_Click(object? pSender, EventArgs pE) {
         AddTriple(true);
      }

      private void CPlusPlusTSMI_Click(object? pSender, EventArgs pE) {
         CPlusPlus();
      }

      private void CommentOutTSMI_Click(object? pSender, EventArgs pE) {
         CommentOut();
      }

      private void CommentRemoveTSMI_Click(object? pSender, EventArgs pE) {
         CommentRemove();
      }

      private void CommentWrapTSMI_Click(object? pSender, EventArgs pE) {
         WrapComment();
      }

      private void CommentUpdateTSMI_Click(object? pSender, EventArgs pE) {
         UpdateComment();
      }

      private void AddNotImplementedTSMI_Click(object? pSender, EventArgs pE) {
         AddNotImplemented();
      }

      private void ReverseEqualityTSMI_Click(object? pSender, EventArgs pE) {
         ReverseEquality();
      }

      private void ExpressionBodiedMethodTSMI_Click(object? pSender, EventArgs pE) =>
         ExpressionBodiedMethod();
      #endregion

      //#region coding preferences dialog
      //private void CommentWidthPrefixButton_Click(object? pSender, EventArgs pE) {
      //   commentWidthUpDown.Focus();
      //   commentWidthUpDown.Select(0, goUpDown.Text.Length);
      //}

      //private void SpacesPerTabPrefixButton_Click(object? pSender, EventArgs pE) {
      //   codingSpacesPerTabUpDown.Focus();
      //   codingSpacesPerTabUpDown.Select(0, goUpDown.Text.Length);
      //}

      //private void CommentWidthCancelButton_Click(object? pSender, EventArgs pE) {
      //   CloseCommentWidth();
      //   RestoreMain();
      //}

      //private void CommentWidthOKButton_Click(object? pSender, EventArgs pE) {
      //   CloseCommentWidth();
      //   Settings.Default.CommentWidth = (int)commentWidthUpDown.Value;
      //   Settings.Default.SpacesPerTab = (int)codingSpacesPerTabUpDown.Value;
      //   Settings.Default.UseThreeSpaces = codingUseThreeSpacesCheckBox.Checked;
      //   Settings.Default.CommentOutBlankLines = codingOutBlanksCheckBox.Checked;
      //   Settings.Default.ConcatenateCommentFirst = codingConcatenateCheckBox.Checked;
      //   if (codingUseTabsRadioButton.Checked)
      //      Settings.Default.UseSpacesNotTabs = false;
      //   else
      //      Settings.Default.UseSpacesNotTabs = true;
      //}
      //#endregion
      #endregion

      #region procedures
      private class Body {
         private string mFirst, mSecond;
         private readonly string mThird, mFourth;

         public Body(string pFirst, string pSecond, string pThird, string pFourth) {
            mFirst = pFirst.Trim();
            mSecond = pSecond.Trim();
            mThird = pThird.Trim();
            mFourth = pFourth.Trim();
         }

         public bool Good() {
            int index = 0;

            if (!string.Equals("{", mFirst.Last().ToString(), StringComparison.OrdinalIgnoreCase))
               return false;
            if (!string.Equals("}", mThird.Last().ToString(), StringComparison.OrdinalIgnoreCase))
               return false;
            if (!string.IsNullOrEmpty(mFourth))
               return false;
            if (!mSecond.EndsWith(';'))
               return false;
            index = mFirst.IndexOf('{');
            if (index == -1)
               return false;
            mFirst = "\t" + mFirst.Substring(0, index - 1) + " =>" + Environment.NewLine;
            mSecond = "\t\t" + mSecond + Environment.NewLine;
            return true;
         }

         public string Results() {
            return mFirst + mSecond + Environment.NewLine;
         }
      }

      private static void ExpressionBodiedMethod() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         string content = string.Empty;
         List<Body> bodies = new List<Body>();
         bool restoreTop = false;
         int index = 0;

         AllIfNothing();
         content = mRichTextBox.SelectedText.Trim();
         content += Environment.NewLine;
         if (content.StartsWith(Environment.NewLine))
            restoreTop = true;
         List<string> lines = content.Split(Environment.NewLine).ToList();

         content = string.Empty;
         foreach (string phrase in lines.OfType<string>()) {
            string thisPhrase = phrase;
            do {
               thisPhrase = thisPhrase.Replace("\t", string.Empty);
            }
            while (thisPhrase.Contains('\t'));
            thisPhrase = thisPhrase.Trim();
            thisPhrase += Environment.NewLine;
            content += thisPhrase;
         }
         do {
            content = content.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
         }
         while (content.Contains(Environment.NewLine + Environment.NewLine));
         content = content.Replace("}", "}" + Environment.NewLine);
         index = content.LastIndexOf(Environment.NewLine);
         content = content.Substring(0, index - 2);
         lines = content.Split(Environment.NewLine).ToList();
         if (string.IsNullOrEmpty(lines[0]))
            lines.RemoveAt(0);
         if (!string.IsNullOrEmpty(lines.Last()))
            lines.Add(string.Empty);
         if ((lines.Count % 4) != 0)
            return;

         for (int i = 0; i < lines.Count; i++) {
            bodies.Add(new Body(lines[i], lines[i + 1], lines[i + 2],
               lines[i + 3]));
            i += 3;
         }
         content = string.Empty;
         if (restoreTop)
            content = Environment.NewLine;
         foreach (Body body in bodies.OfType<Body>()) {
            if (!body.Good())
               return;
            else
               content += body.Results();
         }
         mRichTextBox.SelectedText = Environment.NewLine + content;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void ReverseEquality() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         string content = string.Empty;
         int index = 0;

         AllIfNothing();
         string[] lines = mRichTextBox.SelectedText.Split(Environment.NewLine);

         foreach (string phrase in lines) {
            if (phrase.Contains('=') && phrase.Contains(';'))
               lines[index] = Regex.Replace(lines[index], @"^(\s+)(.*) = (.*);$", "$1$3 = $2;");
            index++;
         }
         for (int i = 0; i < lines.Length; i++)
            content += lines[i] + Environment.NewLine;
         mRichTextBox.SelectedText = content.TrimEnd();
         mRichTextBox.Refresh();
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void AddNotImplemented() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selectedText = mRichTextBox.SelectedText;

         selectedText = Regex.Replace(selectedText, @"\{\r\n\r\n(\s+)\}",
            "{\r\nTimedMessage(\"XXX\", \"NOT YET IMPLEMENTED\");\r\n$1}");
         if (selectedText.Contains("XXX", StringComparison.Ordinal)) {
            do {
               int endProcedureName = selectedText.IndexOf("XXX") - 46;
               string temporary = selectedText.Substring(0, endProcedureName);
               int beginProcedureName = temporary.LastIndexOf(' ') + 1;
#pragma warning disable CA1514
               string procedureName = temporary.Substring(beginProcedureName, (temporary.Length - beginProcedureName));
#pragma warning restore CA1514
               Regex regex = new Regex("XXX");

               selectedText = regex.Replace(selectedText, procedureName, 1);
            }
            while (selectedText.Contains("XXX", StringComparison.Ordinal));
         }
         mRichTextBox.SelectedText = selectedText;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private void GetComment() {
         try {
            TopMost = false;
            ActivateVisualStudio();
            SendKeys.SendWait("^c");
            Thread.Sleep(300);
            if (Clipboard.ContainsText(TextDataFormat.UnicodeText)) {
               mRichTextBox.Clear();//efm5 Probably unnecessary but I've seen the next statement fail if I don't do this
               mRichTextBox.Text = Clipboard.GetText(TextDataFormat.UnicodeText);
               Thread.Sleep(300);
               mRichTextBox.Refresh();
            }
            else if (Clipboard.ContainsText(TextDataFormat.Rtf)) {
               mRichTextBox.Clear();//efm5 Probably unnecessary but I've seen the next statement fail if I don't do this
               mRichTextBox.Text = Clipboard.GetText(TextDataFormat.Rtf);
               Thread.Sleep(300);
               mRichTextBox.Refresh();
            }
            else if (Clipboard.ContainsText(TextDataFormat.Text)) {
               mRichTextBox.Clear();//efm5 Probably unnecessary but I've seen the next statement fail if I don't do this
               mRichTextBox.Text = Clipboard.GetText(TextDataFormat.Text);
               Thread.Sleep(300);
               mRichTextBox.Refresh();
            }
            else
               TimedMessage("GetComment() – There is no text on the clipboard", "WARNING", 3000);
            HardFocus();
         }
         catch (Exception pException) {
            TimedMessage("Get Comment threw an exception" + Environment.NewLine + pException.ToString(),
               "EXCEPTION CAUGHT", 0);
         }
      }

      private void ReplaceComment() {
         try {
            TopMost = false;
            Clipboard.SetText(mRichTextBox.Text, TextDataFormat.UnicodeText);
            Thread.Sleep(300);
            if (Clipboard.ContainsText(TextDataFormat.UnicodeText)) {
               ActivateVisualStudio();
               SendKeys.SendWait("^v");
               Thread.Sleep(300);
            }
            else
               TimedMessage("ReplaceComment() – There is no text on the clipboard", "WARNING", 3000);
            HardFocus();
         }
         catch (Exception pException) {
            TimedMessage("Replace Comment threw an exception" + Environment.NewLine + pException.ToString(),
               "EXCEPTION CAUGHT", 0);
         }
      }

      private static void CPlusPlus() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = mRichTextBox.SelectedText;

         if (string.Equals(content.Substring(content.Length - 1, 1), " ",
            StringComparison.OrdinalIgnoreCase)) {
            content = content.Substring(0, content.Length - 1);
            mRichTextBox.SelectedText = @"/*" + content + @"*/ ";
         }
         else
            mRichTextBox.SelectedText = @"/*" + mRichTextBox.SelectedText + @"*/";
      }

      private static void AddDouble() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = string.Empty;
         string[] lines = mRichTextBox.SelectedText.Split(Environment.NewLine);
         int index = 0;

         foreach (string phrase in lines)
            lines[index] = lines[index++].TrimStart();

         for (int i = 0; i < lines.Length; i++) {
            if (!string.IsNullOrEmpty(lines[i]))
               content += @"//  " + lines[i] + Environment.NewLine;
            else {
               //DEBUG efm5 2026 05 18 reinstate
               //if (Settings.Default.CommentOutBlankLines)
               //   content += @"//" + Environment.NewLine;
               //else
               content += lines[i] + Environment.NewLine;
            }
         }
         mRichTextBox.SelectedText = content.Substring(0, content.Length - 2);
      }

      private static void AddTriple(bool pSummary = false) {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = string.Empty;
         string[] lines = mRichTextBox.SelectedText.Split(Environment.NewLine);
         int index = 0;

         foreach (string phrase in lines)
            lines[index] = lines[index++].TrimStart();
         if (pSummary) {
            //DEBUG efm5 2026 05 18 reinstate
            //if (Settings.Default.UseThreeSpaces)
            //   content += @"///   <summary>" + Environment.NewLine + Environment.NewLine;
            //else
            content += @"///  <summary>" + Environment.NewLine + Environment.NewLine;
         }

         for (int i = 0; i < lines.Length; i++) {
            if (!string.IsNullOrEmpty(lines[i])) {
               //DEBUG efm5 2026 05 18 reinstate
               //if (Settings.Default.UseThreeSpaces)
               //   content += @"///   " + lines[i] + Environment.NewLine;
               //else
               content += @"///  " + lines[i] + Environment.NewLine;
            }
            else {
               //DEBUG efm5 2026 05 18 reinstate
               //if (Settings.Default.CommentOutBlankLines)
               //   content += @"///" + lines[i] + Environment.NewLine;
               //else
               content += lines[i] + Environment.NewLine;
            }
         }
         mRichTextBox.SelectedText = content.Substring(0, content.Length - 2);
      }

      private static void CommentRemove() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string temporary = mRichTextBox.SelectedText;
         temporary = Regex.Replace(temporary, Regex.Escape(@"/*"), "");
         temporary = Regex.Replace(temporary, Regex.Escape(@"*/"), "");
         mRichTextBox.SelectedText = Regex.Replace(temporary, @"/", "");
      }

      private static void CommentOut() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         string[] lines = new string[mRichTextBox.Lines.Length];
         int index = 0;

         foreach (string phrase in mRichTextBox.Lines) {
            lines[index] = phrase.Trim();
            lines[index] = @"//" + lines[index++];
         }
         mRichTextBox.Clear();
         for (int i = 0; i < lines.Length; i++)
            mRichTextBox.Text += lines[i] + Environment.NewLine;
         mRichTextBox.Text = mRichTextBox.Text.Substring(0, mRichTextBox.TextLength - 2);
      }

      private static void WrapComment() {
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = string.Empty;
         string[] lines = new string[] { string.Empty };
         //DEBUG efm5 2026 05 18 reinstate
         //if (Settings.Default.ConcatenateCommentFirst) {
         //   lines[0] = mRichTextBox.SelectedText.Replace("\r\n", " ");
         //   lines[0] = lines[0].Replace("\n\r", " ");
         //   lines[0] = lines[0].Replace("\r", " ");
         //   lines[0] = lines[0].Replace("\n", " ");
         //}
         //else
         lines = mRichTextBox.SelectedText.Split(Environment.NewLine);
         foreach (string phrase in lines) {
            //DEBUG efm5 2026 05 18 reinstate
            //if (phrase.Length > Settings.Default.CommentWidth)
            //   content += SplitToLines(phrase, new char[] { ' ' }, Settings.Default.CommentWidth) +
            //      Environment.NewLine;
            //else
            content += phrase + Environment.NewLine;
         }
         mRichTextBox.SelectedText = content.Substring(0, content.Length - 2);
      }

      public static string SplitToLines(string pText, char[] pSplitOnCharacters, int pMaximumStringLength) {
         StringBuilder stringBuilder = new StringBuilder();
         int index = 0;

         while (pText.Length > index) {
            if (index != 0)
               stringBuilder.AppendLine();
            int splitAt = index + pMaximumStringLength <= pText.Length
                ? pText.Substring(index, pMaximumStringLength).LastIndexOfAny(pSplitOnCharacters)
                : pText.Length - index;
            splitAt = (splitAt == -1) ? pMaximumStringLength : splitAt;
            stringBuilder.Append(pText.Substring(index, splitAt).Trim());
            index += splitAt;
         }
         return stringBuilder.ToString();
      }

      private static void UpdateComment() {
         TimedMessage("CommentUpdate probably now redundant (replaced by “Replace”)" + Environment.NewLine +
            "Left as an empty placeholder in both the menu and the toolbar", "Not yet implemented", 3000);
      }

      private void LayoutCommentPreferencesPanel() {
         //   sControlList.Clear();
         //   commentPreferencesTitleLabel.Top = 10;
         //   commentWidthGroupBox.Top = commentPreferencesTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
         //   commentWidthPrefixButton.Top = GetGroupBoxFirstLineOffset(commentWidthGroupBox);
         //   commentWidthUpDown.Location = new Point(commentWidthPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
         //      commentWidthPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
         //   commentWrapWidthLabel.Location = new Point(commentWidthUpDown.Right + sAssociatedUpDownPostButtonHorizontalSpace,
         //      commentWidthUpDown.Top + sAssociatedUpDownPostButtonVerticalOffset);
         //   sControlList.Add(commentWidthPrefixButton);
         //   sControlList.Add(commentWidthUpDown);
         //   sControlList.Add(commentWrapWidthLabel);
         //   codingConcatenateCheckBox.Top = Bottommost(sControlList) + sWidgetBigVerticalOffset;
         //   SizeGroupBox(commentWidthGroupBox);
         //   codingUseThreeSpacesCheckBox.Top = commentWidthGroupBox.Bottom + (sWidgetBigVerticalOffset * 2);
         //   codingPreserveWhiteSpaceCheckBox.Top = codingUseThreeSpacesCheckBox.Bottom + sWidgetVerticalOffset;
         //   codingOutBlanksCheckBox.Top = codingPreserveWhiteSpaceCheckBox.Bottom + sWidgetVerticalOffset;
         //   codingTabsVsSpacesGroupBox.Top = codingOutBlanksCheckBox.Bottom + sWidgetBigVerticalOffset;
         //   codingUseTabsRadioButton.Top = GetGroupBoxFirstLineOffset(codingTabsVsSpacesGroupBox);
         //   codingUseSpacesRadioButton.Location = new Point(codingUseTabsRadioButton.Right + sWidgetVerticalOffset, codingUseTabsRadioButton.Top);
         //   sControlList.Clear();
         //   sControlList.Add(codingUseTabsRadioButton);
         //   sControlList.Add(codingUseSpacesRadioButton);
         //   codingSpacesPerTabPrefixButton.Top = Bottommost(sControlList);
         //   codingSpacesPerTabUpDown.Location = new Point(codingSpacesPerTabPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
         //      codingSpacesPerTabPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
         //   codingSpacesPerTabLabel.Location = new Point(codingSpacesPerTabUpDown.Right + sAssociatedUpDownPostButtonHorizontalSpace,
         //      codingSpacesPerTabUpDown.Top + sAssociatedUpDownPostButtonVerticalOffset);
         //   SizeGroupBox(codingTabsVsSpacesGroupBox);
         //   codingOKButton.Location = new Point(sIndent, codingTabsVsSpacesGroupBox.Bottom + (sWidgetBigVerticalOffset * 2));
         //   codingCancelButton.Top = codingOKButton.Top;
         //   SizePanel(commentPreferencesPanel);
         //   codingCancelButton.Left = commentPreferencesPanel.Width - sCancelOffset - codingCancelButton.Width;
         //   CenterControlHorizontally(codingTabsVsSpacesGroupBox, commentPreferencesTitleLabel);
      }

      private void CommentPreferences() {
         //RememberWindow();
         //sSelectionStart = mRichTextBox.SelectionStart;
         //sSelectionLength = mRichTextBox.SelectionLength;
         //if (!Controls.Contains(commentPreferencesPanel))
         //   Controls.Add(commentPreferencesPanel);
         //commentWidthUpDown.Value = Settings.Default.CommentWidth;
         //codingSpacesPerTabUpDown.Value = Settings.Default.SpacesPerTab;
         //if (Settings.Default.UseSpacesNotTabs)
         //   codingUseSpacesRadioButton.Checked = true;
         //else
         //   codingUseTabsRadioButton.Checked = true;
         //codingUseThreeSpacesCheckBox.Checked = Settings.Default.UseThreeSpaces;
         //codingOutBlanksCheckBox.Checked = Settings.Default.CommentOutBlankLines;
         //codingPreserveWhiteSpaceCheckBox.Checked = Settings.Default.PreserveWhitespace;
         //codingConcatenateCheckBox.Checked = Settings.Default.ConcatenateCommentFirst;
         //DisableMain();
         //sEscapeFrom = EscapeFrom.CommentWidth;
         //LayoutCommentPreferencesPanel();
         //FitToDialog();
         //commentPreferencesPanel.Enabled = true;
         //commentPreferencesPanel.Show();
         //commentPreferencesPanel.BringToFront();
         //CenterControl(this, commentPreferencesPanel);
         //commentWidthUpDown.Focus();
         //commentWidthUpDown.Select(0, commentWidthUpDown.Text.Length);
      }

      private void CloseCommentWidth() {
         //sEscapeFrom = EscapeFrom.Main;
         //commentPreferencesPanel.Enabled = false;
         //commentPreferencesPanel.Hide();
         //if (Controls.Contains(commentPreferencesPanel))
         //   Controls.Remove(commentPreferencesPanel);
         //RestoreMain();
         //RestoreWindow();
      }
      #endregion
   }
}
