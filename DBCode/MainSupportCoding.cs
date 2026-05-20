namespace DBCode {
   public partial class MainForm : Form {
      #region C-Family coding methods
      private class Body {
         private string mFirst, mSecond;
         private readonly string mThird, mFourth;

#pragma warning disable IDE0290
         public Body(string pFirst, string pSecond, string pThird, string pFourth) {
            mFirst = pFirst.Trim();
            mSecond = pSecond.Trim();
            mThird = pThird.Trim();
            mFourth = pFourth.Trim();
         }
#pragma warning restore IDE0290

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
            mFirst = string.Concat("\t", mFirst.AsSpan(0, index - 1), " =>", Environment.NewLine);
            mSecond = "\t\t" + mSecond + Environment.NewLine;
            return true;
         }

         public string Results() {
            return mFirst + mSecond + Environment.NewLine;
         }
      }

      private static void CSharpExpressionBodiedMethod() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         string content = string.Empty;
         List<Body> bodies = [];
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

      private static void CSharpReverseEquality() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         string content = string.Empty;
         int index = 0;

         AllIfNothing();
         string[] lines = mRichTextBox.SelectedText.Split(["\r\n", "\n"], StringSplitOptions.None);

         foreach (string phrase in lines) {
            if (phrase.Contains('=') && phrase.Contains(';'))
               lines[index] = Regex.Replace(lines[index], @"^(\s*)(.*) = (.*);$", "$1$3 = $2;");
            index++;
         }
         for (int i = 0; i < lines.Length; i++)
            content += lines[i] + Environment.NewLine;
         mRichTextBox.SelectedText = content.TrimEnd();
         mRichTextBox.Refresh();
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void CSharpAddNotImplemented() {
         AssertCodingReady();
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

      private static void CFamilyBlockComment() {
         AssertCodingReady();
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

      private static void CFamilyAddDouble() {
         AssertCodingReady();
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

      private static void CSharpAddTriple(bool pSummary = false) {
         AssertCodingReady();
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

      private static void CFamilyCommentRemove() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string temporary = mRichTextBox.SelectedText;
         temporary = Regex.Replace(temporary, Regex.Escape(@"/*"), "");
         temporary = Regex.Replace(temporary, Regex.Escape(@"*/"), "");
         mRichTextBox.SelectedText = Regex.Replace(temporary, @"/", "");
      }

      private static void CFamilyCommentOut() {
         AssertCodingReady();
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

      private static void CFamilyWrapComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = string.Empty;
         string[] lines = [string.Empty];
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

      #endregion

      #region Basic coding methods
      private static void BasicAddSingleQuote() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = string.Empty;
         string[] lines = mRichTextBox.SelectedText.Split(["\r\n", "\n"], StringSplitOptions.None);
         int index = 0;

         foreach (string phrase in lines)
            lines[index] = lines[index++].TrimStart();

         for (int i = 0; i < lines.Length; i++) {
            if (!string.IsNullOrEmpty(lines[i]))
               content += @"'  " + lines[i] + Environment.NewLine;
            else
               content += lines[i] + Environment.NewLine;
         }
         mRichTextBox.SelectedText = content.Substring(0, content.Length - 2);
      }

      private static void ConvertCSharpToVBComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @"^(\s*)//", "$1'", RegexOptions.Multiline);
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void ConvertVBToCSharpComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @"^(\s*)'", "$1//", RegexOptions.Multiline);
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void ReverseEqualityVB() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         string content = string.Empty;
         int index = 0;

         AllIfNothing();
         string[] lines = mRichTextBox.SelectedText.Split(["\r\n", "\n"], StringSplitOptions.None);

         foreach (string phrase in lines) {
            if (phrase.Contains('='))
               lines[index] = Regex.Replace(lines[index], @"^(\s*)(.*) = (.*)$", "$1$3 = $2");
            index++;
         }
         for (int i = 0; i < lines.Length; i++)
            content += lines[i] + Environment.NewLine;
         mRichTextBox.SelectedText = content.TrimEnd();
         mRichTextBox.Refresh();
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void RemoveLineContinuation() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @" _\r?\n\s*", " ");
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void ConvertBooleans() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\b(True|False)\b")) {
            selected = Regex.Replace(selected, @"\bTrue\b", "true");
            selected = Regex.Replace(selected, @"\bFalse\b", "false");
         }
         else {
            selected = Regex.Replace(selected, @"\btrue\b", "True");
            selected = Regex.Replace(selected, @"\bfalse\b", "False");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void ConvertNullNothing() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\bNothing\b")) {
            selected = Regex.Replace(selected, @"\bNothing\b", "null");
         }
         else {
            selected = Regex.Replace(selected, @"\bnull\b", "Nothing");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void ConvertLogicalOperators() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\b(AndAlso|OrElse|And|Or)\b")) {
            selected = Regex.Replace(selected, @"\bAndAlso\b", "&&");
            selected = Regex.Replace(selected, @"\bOrElse\b", "||");
            selected = Regex.Replace(selected, @"\bAnd\b", "&");
            selected = Regex.Replace(selected, @"\bOr\b", "|");
         }
         else {
            selected = Regex.Replace(selected, @"&&", "AndAlso");
            selected = Regex.Replace(selected, @"\|\|", "OrElse");
            selected = Regex.Replace(selected, @"&", "And");
            selected = Regex.Replace(selected, @"\|", "Or");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region F# coding methods
      private static void FSharpBlockComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = mRichTextBox.SelectedText;

         if (string.Equals(content.Substring(content.Length - 1, 1), " ",
            StringComparison.OrdinalIgnoreCase)) {
            content = content.Substring(0, content.Length - 1);
            mRichTextBox.SelectedText = "(* " + content + " *) ";
         }
         else
            mRichTextBox.SelectedText = "(* " + mRichTextBox.SelectedText + " *)";
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void FSharpAddComment() => CFamilyAddDouble();

      private static void FSharpAddMutable() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @"^(\s*let\s+)(?!mutable\b)", "$1mutable ", RegexOptions.Multiline);
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void FSharpAddIgnore() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @"^(\s*.+)$", "$1 |> ignore", RegexOptions.Multiline);
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void FSharpConvertNullNone() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\bNone\b"))
            selected = Regex.Replace(selected, @"\bNone\b", "null");
         else
            selected = Regex.Replace(selected, @"\bnull\b", "None");
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region HTML coding methods
      private static void HTMLWrap(string pOpen, string pClose) {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;
         mRichTextBox.SelectedText = pOpen + selected + pClose;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void HTMLComment() => HTMLWrap("<!-- ", " -->");
      private static void HTMLBold() => HTMLWrap("<b>", "</b>");
      private static void HTMLItalic() => HTMLWrap("<i>", "</i>");
      private static void HTMLUnderline() => HTMLWrap("<u>", "</u>");
      private static void HTMLStrikethrough() => HTMLWrap("<s>", "</s>");
      private static void HTMLBig() => HTMLWrap("<big>", "</big>");
      private static void HTMLBigBig() => HTMLWrap("<big><big>", "</big></big>");
      private static void HTMLSmall() => HTMLWrap("<small>", "</small>");
      private static void HTMLSmallSmall() => HTMLWrap("<small><small>", "</small></small>");
      private static void HTMLMark() => HTMLWrap("<mark>", "</mark>");
      private static void HTMLSuperscript() => HTMLWrap("<sup>", "</sup>");
      private static void HTMLSubscript() => HTMLWrap("<sub>", "</sub>");
      private static void HTMLCode() => HTMLWrap("<code>", "</code>");
      private static void HTMLPreformatted() => HTMLWrap("<pre>", "</pre>");

      private static void HTMLColorize() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;
         using ColorDialog dialog = new ColorDialog { FullOpen = true };
         if (dialog.ShowDialog() != DialogResult.OK)
            return;
         Color color = dialog.Color;
         string hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
         mRichTextBox.SelectedText = $"<span style=\"color: {hex}\">{selected}</span>";
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      #endregion

      #region CSS coding methods
      private static void CSSBlockComment() => CFamilyBlockComment();
      private static void CSSCommentRemove() => CFamilyCommentRemove();

      private static void CSSToggleImportant() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"!important", RegexOptions.IgnoreCase))
            selected = Regex.Replace(selected, @"\s*!important", "", RegexOptions.IgnoreCase);
         else
            selected = Regex.Replace(selected, @"\s*;", " !important;");
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void CSSConvertColorFormat() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"#[0-9A-Fa-f]{6}\b"))
            selected = Regex.Replace(selected, @"#([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})\b",
               match => {
                  int r = Convert.ToInt32(match.Groups[1].Value, 16);
                  int g = Convert.ToInt32(match.Groups[2].Value, 16);
                  int b = Convert.ToInt32(match.Groups[3].Value, 16);
                  return $"rgb({r}, {g}, {b})";
               });
         else
            selected = Regex.Replace(selected,
               @"rgb\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*\)",
               match => {
                  int r = int.Parse(match.Groups[1].Value);
                  int g = int.Parse(match.Groups[2].Value);
                  int b = int.Parse(match.Groups[3].Value);
                  return $"#{r:X2}{g:X2}{b:X2}";
               }, RegexOptions.IgnoreCase);
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region XML coding methods
      private static void XMLComment() => HTMLWrap("<!-- ", " -->");

      private static void XMLRemoveComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string temporary = mRichTextBox.SelectedText;

         temporary = Regex.Replace(temporary, @"<!--\s?", "");
         temporary = Regex.Replace(temporary, @"\s?-->", "");
         mRichTextBox.SelectedText = temporary;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void XMLEscapeEntities() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"&(?:lt|gt|amp|quot|apos);")) {
            // unescape — &amp; must come last to avoid corrupting other entities
            selected = selected.Replace("&lt;", "<");
            selected = selected.Replace("&gt;", ">");
            selected = selected.Replace("&quot;", "\"");
            selected = selected.Replace("&apos;", "'");
            selected = selected.Replace("&amp;", "&");
         }
         else {
            // escape — & must come first to avoid double-escaping
            selected = selected.Replace("&", "&amp;");
            selected = selected.Replace("<", "&lt;");
            selected = selected.Replace(">", "&gt;");
            selected = selected.Replace("\"", "&quot;");
            selected = selected.Replace("'", "&apos;");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void XMLWrapCData() => HTMLWrap("<![CDATA[", "]]>");

      private static void XMLToggleSelfClose() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"<\w[^>]*/>")) {
            // expand: <foo/> → <foo></foo>
            selected = Regex.Replace(selected, @"<(\w[\w:.-]*)([^>]*?)/>", "<$1$2></$1>");
         }
         else {
            // collapse empty elements: <foo></foo> → <foo/>
            selected = Regex.Replace(selected, @"<(\w[\w:.-]*)([^>]*?)></\1>", "<$1$2/>");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region JSON coding methods
      private static void JSONAddComment() => CFamilyAddDouble();
      private static void JSONBlockComment() => CFamilyBlockComment();
      private static void JSONRemoveComment() => CFamilyCommentRemove();

      private static void JSONToggleQuotes() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (selected.Contains('\''))
            selected = Regex.Replace(selected, @"'([^']*)'", "\"$1\"");
         else
            selected = Regex.Replace(selected, "\"([^\"]*)\"", "'$1'");
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void JSONEscapeString() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (selected.Contains("\\\"") || selected.Contains("\\\\")) {
            // unescape — backslash must come first to avoid double-processing
            selected = selected.Replace("\\\\", "\\");
            selected = selected.Replace("\\\"", "\"");
            selected = selected.Replace("\\n", "\n");
            selected = selected.Replace("\\t", "\t");
            selected = selected.Replace("\\r", "\r");
         }
         else {
            // escape — backslash must come first to avoid double-processing
            selected = selected.Replace("\\", "\\\\");
            selected = selected.Replace("\"", "\\\"");
            selected = selected.Replace("\n", "\\n");
            selected = selected.Replace("\t", "\\t");
            selected = selected.Replace("\r", "\\r");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void JSONRemoveTrailingCommas() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @",(\s*[}\]])", "$1");
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region Power Shell coding methods
      private static void PowerShellAddComment() => AddLineComment("# ");
      private static void PowerShellBlockComment() => InlineBlockComment("<# ", " #>");

      private static void PowerShellRemoveComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string temporary = mRichTextBox.SelectedText;

         temporary = Regex.Replace(temporary, @"<#\s?", "");
         temporary = Regex.Replace(temporary, @"\s?#>", "");
         temporary = Regex.Replace(temporary, @"^#\s?", "", RegexOptions.Multiline);
         mRichTextBox.SelectedText = temporary;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void PowerShellToggleBoolean() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\$(true|false)", RegexOptions.IgnoreCase)) {
            selected = Regex.Replace(selected, @"\$true", "true", RegexOptions.IgnoreCase);
            selected = Regex.Replace(selected, @"\$false", "false", RegexOptions.IgnoreCase);
         }
         else {
            selected = Regex.Replace(selected, @"\btrue\b", "$$true");
            selected = Regex.Replace(selected, @"\bfalse\b", "$$false");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void PowerShellToggleQuotes() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (selected.Contains('\''))
            selected = Regex.Replace(selected, @"'([^']*)'", "\"$1\"");
         else
            selected = Regex.Replace(selected, "\"([^\"]*)\"", "'$1'");
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region Batch coding methods
      private static void BatchAddComment() => AddLineComment("REM ");
      private static void BatchAddColonComment() => AddLineComment(":: ");

      private static void BatchRemoveComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         mRichTextBox.SelectedText = Regex.Replace(mRichTextBox.SelectedText,
            @"^(REM\s+|::\s*)", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void BatchToggleCommentStyle() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"^::", RegexOptions.Multiline))
            selected = Regex.Replace(selected, @"^:: ?", "REM ", RegexOptions.Multiline);
         else
            selected = Regex.Replace(selected, @"^REM\s+", ":: ",
               RegexOptions.Multiline | RegexOptions.IgnoreCase);
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region SQL coding methods
      private static void SQLAddComment() => AddLineComment("--  ");
      private static void SQLBlockComment() => CFamilyBlockComment();

      private static void SQLRemoveComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string temporary = mRichTextBox.SelectedText;

         temporary = Regex.Replace(temporary, Regex.Escape("/*"), "");
         temporary = Regex.Replace(temporary, Regex.Escape("*/"), "");
         temporary = Regex.Replace(temporary, @"^--\s?", "", RegexOptions.Multiline);
         mRichTextBox.SelectedText = temporary;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void SQLToggleNullCheck() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\bIS\s+(NOT\s+)?NULL\b", RegexOptions.IgnoreCase)) {
            // IS NOT NULL must be replaced before IS NULL to avoid partial match
            selected = Regex.Replace(selected, @"\bIS\s+NOT\s+NULL\b", "<> NULL",
               RegexOptions.IgnoreCase);
            selected = Regex.Replace(selected, @"\bIS\s+NULL\b", "= NULL",
               RegexOptions.IgnoreCase);
         }
         else {
            // <>/<!/= NULL must be replaced before = NULL to avoid partial match
            selected = Regex.Replace(selected, @"<>\s*NULL\b", "IS NOT NULL",
               RegexOptions.IgnoreCase);
            selected = Regex.Replace(selected, @"!=\s*NULL\b", "IS NOT NULL",
               RegexOptions.IgnoreCase);
            selected = Regex.Replace(selected, @"=\s*NULL\b", "IS NULL",
               RegexOptions.IgnoreCase);
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void SQLToggleBoolean() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\b(TRUE|FALSE)\b", RegexOptions.IgnoreCase)) {
            selected = Regex.Replace(selected, @"\bTRUE\b", "1", RegexOptions.IgnoreCase);
            selected = Regex.Replace(selected, @"\bFALSE\b", "0", RegexOptions.IgnoreCase);
         }
         else {
            selected = Regex.Replace(selected, @"\b1\b", "TRUE");
            selected = Regex.Replace(selected, @"\b0\b", "FALSE");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region Markdown coding methods
      private static void MarkdownComment() => HTMLWrap("<!-- ", " -->");
      private static void MarkdownBold() => HTMLWrap("**", "**");
      private static void MarkdownItalic() => HTMLWrap("*", "*");
      private static void MarkdownBoldItalic() => HTMLWrap("***", "***");
      private static void MarkdownStrikethrough() => HTMLWrap("~~", "~~");
      private static void MarkdownCode() => HTMLWrap("`", "`");

      private static void MarkdownCodeBlock() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         mRichTextBox.SelectedText = "```" + Environment.NewLine + selected +
            Environment.NewLine + "```";
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      #region Python coding methods
      private static void PythonAddComment() => AddLineComment("# ");
      private static void PythonBlockComment() => InlineBlockComment("\"\"\" ", " \"\"\"");

      private static void PythonRemoveComment() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string temporary = mRichTextBox.SelectedText;

         temporary = temporary.Replace("\"\"\"", "");
         temporary = Regex.Replace(temporary, @"^#\s?", "", RegexOptions.Multiline);
         mRichTextBox.SelectedText = temporary;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void PythonToggleBoolean() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\b(True|False)\b")) {
            selected = Regex.Replace(selected, @"\bTrue\b", "true");
            selected = Regex.Replace(selected, @"\bFalse\b", "false");
         }
         else {
            selected = Regex.Replace(selected, @"\btrue\b", "True");
            selected = Regex.Replace(selected, @"\bfalse\b", "False");
         }
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void PythonConvertNullNone() {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string selected = mRichTextBox.SelectedText;

         if (Regex.IsMatch(selected, @"\bNone\b"))
            selected = Regex.Replace(selected, @"\bNone\b", "null");
         else
            selected = Regex.Replace(selected, @"\bnull\b", "None");
         mRichTextBox.SelectedText = selected;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }
      #endregion

      private static void AddLineComment(string pPrefix) {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = string.Empty;
         string[] lines = mRichTextBox.SelectedText.Split(["\r\n", "\n"], StringSplitOptions.None);
         int index = 0;

         foreach (string phrase in lines)
            lines[index] = lines[index++].TrimStart();
         for (int i = 0; i < lines.Length; i++) {
            if (!string.IsNullOrEmpty(lines[i]))
               content += pPrefix + lines[i] + Environment.NewLine;
            else
               content += lines[i] + Environment.NewLine;
         }
         mRichTextBox.SelectedText = content.Substring(0, content.Length - 2);
      }

      private static void InlineBlockComment(string pOpen, string pClose) {
         AssertCodingReady();
         if (string.IsNullOrEmpty(mRichTextBox.Text))
            return;
         AllIfNothing();
         string content = mRichTextBox.SelectedText;

         if (string.Equals(content.Substring(content.Length - 1, 1), " ",
            StringComparison.OrdinalIgnoreCase)) {
            content = content.Substring(0, content.Length - 1);
            mRichTextBox.SelectedText = pOpen + content + pClose + " ";
         }
         else
            mRichTextBox.SelectedText = pOpen + mRichTextBox.SelectedText + pClose;
         mRichTextBox.SelectAll();
         mRichTextBox.Copy();
      }

      private static void AssertCodingReady() {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
      }

      private static void AllIfNothing() {
         AssertCodingReady();
         if ((mUiState.mAllIfNothing) && string.IsNullOrEmpty(mRichTextBox.SelectedText))
            mRichTextBox.SelectAll();
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
   }
}
