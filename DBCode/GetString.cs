namespace DBCode {
   internal sealed class GetString : Panel {
      private static bool mHasControlBox;
      private readonly BottomPanel mGetStringBottomPanel;
      private readonly Button mOKButton;
      private readonly HeaderLabelCluster mTitleCluster;
      private readonly Label mPromptLabel;
      private readonly Panel mOuterPanel, mInnerPanel;
      private readonly string mInitialValue;
      private readonly TextBox mInputTextBox;

      public string? ResultValue => WasCancelled ? null : mInputTextBox.Text;
      public bool WasCancelled { get; private set; }

      [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
      public Action<string?, bool>? OnClose { get; set; }

      public GetString(string pTitle, string pPrompt, string pInitialValue = "") {
         ThrowIfNull(pTitle, nameof(pTitle));
         ThrowIfNull(pPrompt, nameof(pPrompt));
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));

         mHasControlBox = mForm.ControlBox;
         if (mHasControlBox)
            mForm.ControlBox = false;
         mInitialValue = pInitialValue;
         mOuterPanel = new Panel {
            Name = $"GetString_OuterPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            Location = new Point(mEmFifth, mEmFifth),
            BackColor = Color.White
         };
         mInnerPanel = new Panel {
            Name = $"GetString_InnerPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            Location = new Point(mEmFifth, mEmFifth),
            BackColor = Color.Black,
            AutoScroll = true
         };
         mTitleCluster = new HeaderLabelCluster(mCurrentTheme, pTitle, HeaderLabelSize.Normal);
         mPromptLabel = new Label {
            Name = $"GetString_PromptLabel{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = pPrompt,
            AutoSize = true,
            Dock = DockStyle.Top
         };
         mInputTextBox = new TextBox {
            Name = $"GetString_InputTextBox{mTabIndex}",
            TabIndex = mTabIndex++,
            AutoSize = false,
            Text = pInitialValue,
            Multiline = false,
            Dock = DockStyle.Top
         };
         mOKButton = new Button {
            Name = $"GetString_OKButton{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = "&OK",
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
         };
         mGetStringBottomPanel = new BottomPanel(mCurrentTheme!, pCancelText: "&Cancel") {
            Dock = DockStyle.Top
         };
         mGetStringBottomPanel.mHelpButton!.Tag = new HelpTag(HelpContext.Main, "GetString");
         mGetStringBottomPanel.AddRightControl(mOKButton);
         mGetStringBottomPanel.mCancelButton!.Click += CancelButton_Click;
         mOKButton.Click += OKButton_Click;
         mInputTextBox.KeyDown += InputTextBox_KeyDown;
         mInnerPanel.Controls.AddRange([mGetStringBottomPanel, mInputTextBox, mPromptLabel, mTitleCluster]);
         mOuterPanel.Controls.Add(mInnerPanel);
         Controls.Add(mOuterPanel);
         LayoutClusters();
      }

      public static void Show(string pTitle, string pPrompt, string pInitialValue, Action<string?, bool> pCallback) {
         ThrowIfNull(mForm, nameof(mForm));
         foreach (Control control in mForm.Controls.OfType<Control>())
            control.Enabled = false;
         mPreGetStringBounds = mForm.Bounds;
         mGetStringPanel = new GetString(pTitle, pPrompt, pInitialValue) {
            OnClose = pCallback
         };
         mForm.SuspendLayout();
         mForm.Controls.Add(mGetStringPanel);
         mGetStringPanel.PerformLayout();
         mForm.ResumeLayout(true);
         CenterControl(mForm, mGetStringPanel);
         EnsureWindowFitsMonitor(mForm, false);
         mGetStringPanel.Visible = true;
         mGetStringPanel.BringToFront();
         mGetStringPanel.Show();
         mGetStringPanel.FocusInputTextBox();
      }

      public static void Restore() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mGetStringPanel, nameof(mGetStringPanel));
         mForm.SuspendLayout();
         mGetStringPanel.Visible = false;
         mGetStringPanel.SendToBack();
         if (mForm.Controls.Contains(mGetStringPanel))
            mForm.Controls.Remove(mGetStringPanel);
         mGetStringPanel.Dispose();
         mGetStringPanel = null;
         mForm.Bounds = mPreGetStringBounds;
         foreach (Control control in mForm.Controls.OfType<Control>())
            control.Enabled = true;
         if (mHasControlBox)
            mForm.ControlBox = true;
         mForm.ResumeLayout(true);
      }

      private void LayoutClusters() {
         SuspendLayout();
         mInnerPanel.SuspendLayout();
         ApplyTheme();
         mTitleCluster.LayoutCluster();
         SizeF size;
         if (!string.IsNullOrEmpty(mInitialValue))
            SizeTextBoxToFitString(out size, mInputTextBox, mInitialValue);
         else {
            mInputTextBox.Width = 300;
            SizeTextBoxToFitString(out size, mInputTextBox);
         }
         mInputTextBox.Size = new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
         mInputTextBox.Left = mIndent;
         int wantedWidth = mTitleCluster.Width;
         wantedWidth = Math.Max(wantedWidth, mPromptLabel.Width);
         wantedWidth = Math.Max(wantedWidth, mInputTextBox.Width);
         wantedWidth = Math.Max(wantedWidth, mGetStringBottomPanel.NeededWidth);
         mTitleCluster.LayoutCluster();
         mGetStringBottomPanel.LayoutControls();// efm5 this is needed to get the correct sizes after the font is applied
         mInnerPanel.Size = new Size(wantedWidth + mEm, mTitleCluster.Height + mPromptLabel.Height +
            mInputTextBox.Height + mGetStringBottomPanel.Height);
         mGetStringBottomPanel.LayoutControls();// efm5 this is needed to recalculate the needed width after the inner panel is sized
         mOuterPanel.Size =
            new Size(mInnerPanel.Width + (mEmFifth * 2), mInnerPanel.Height + (mEmFifth * 2));
         Size = new Size(mOuterPanel.Width + (mEmFifth * 2), mOuterPanel.Height + (mEmFifth * 2));
         Width += SystemInformation.VerticalScrollBarWidth;
         Height += SystemInformation.HorizontalScrollBarHeight;
         mInnerPanel.ResumeLayout(true);
         ResumeLayout(true);
      }

      private void ApplyTheme() {
         Theme theme = mCurrentTheme!;
         Theme.ThemeInterfaceThings(theme, out Font interfaceFont, out Color interfaceForeColor,
            out Color interfaceBackColor);
         mInnerPanel.BackColor = theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground];
         MainForm.DisposeFontIfOwned(mPromptLabel.Font);
         mPromptLabel.Font = CreateNewFont(interfaceFont);
         mPromptLabel.ForeColor = interfaceForeColor;
         mPromptLabel.BackColor = interfaceBackColor;
         Theme.ThemeTextBoxThings(theme, out Font textBoxFont, out Color textBoxForeColor,
            out Color textBoxBackColor);
         MainForm.DisposeFontIfOwned(mInputTextBox.Font);
         mInputTextBox.Font = CreateNewFont(textBoxFont);
         mInputTextBox.ForeColor = textBoxForeColor;
         mInputTextBox.BackColor = textBoxBackColor;
      }

      internal void FocusInputTextBox() {
         mInputTextBox.Focus();
         mInputTextBox.SelectAll();
      }

      private void CloseDialog(bool pCancelled) {
         WasCancelled = pCancelled;
         OnClose?.Invoke(ResultValue, WasCancelled);
      }

      private void OKButton_Click(object? pSender, EventArgs pEventArguments) {
         CloseDialog(false);
      }

      private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
         CloseDialog(true);
      }

      private void InputTextBox_KeyDown(object? pSender, KeyEventArgs pEventArguments) {
         if (pEventArguments.KeyCode == Keys.Enter) {
            pEventArguments.Handled = true;
            pEventArguments.SuppressKeyPress = true;
            CloseDialog(false);
         }
         else if (pEventArguments.KeyCode == Keys.Escape) {
            pEventArguments.Handled = true;
            pEventArguments.SuppressKeyPress = true;
            CloseDialog(true);
         }
      }

      protected override void Dispose(bool pDisposing) {
         if (pDisposing) {
            mOKButton.Click -= OKButton_Click;
            mGetStringBottomPanel.mCancelButton!.Click -= CancelButton_Click;
            mInputTextBox.KeyDown -= InputTextBox_KeyDown;
            MainForm.DisposeFontIfOwned(mPromptLabel.Font);
            MainForm.DisposeFontIfOwned(mInputTextBox.Font);
         }
         base.Dispose(pDisposing);
      }
   }
}
