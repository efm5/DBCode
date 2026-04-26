using DBCode.Themes;

namespace DBCode {
   internal sealed class GetString : Panel {
      private readonly Button mOKButton, mCancelButton, mHelpButton;
      private readonly HeaderLabelCluster mTitleCluster;
      private readonly Label mPromptLabel;
      private readonly StatusStrip mStatusStrip;
      private readonly string mInitialValue;
      private readonly TextBox mInputTextBox;
      private readonly ToolStripControlHost mOKHost, mCancelHost, mHelpHost;
      private readonly ToolStripStatusLabel mSpringLabel;

      public string? ResultValue => WasCancelled ? null : mInputTextBox.Text;
      public bool WasCancelled { get; private set; }

      [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
      public Action<string?, bool>? OnClose { get; set; }

      public GetString(string pTitle, string pPrompt, string pInitialValue = "") {
         ThrowIfNull(pTitle, nameof(pTitle));
         ThrowIfNull(pPrompt, nameof(pPrompt));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));

         mInitialValue = pInitialValue;
         AutoScroll = true;
         AutoSize = false;
         AutoSizeMode = AutoSizeMode.GrowAndShrink;
         mTitleCluster = new HeaderLabelCluster(mCurrentTheme, pTitle, HeaderLabelSize.Normal);
         mPromptLabel = new Label {
            Name = $"GetString_PromptLabel{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = pPrompt,
            AutoSize = true,
            MaximumSize = new Size(600, 0),
            TextAlign = ContentAlignment.TopLeft,
            Dock = DockStyle.Top
         };
         mInputTextBox = new TextBox {
            Name = $"GetString_InputTextBox{mTabIndex}",
            TabIndex = mTabIndex++,
            AutoSize = false,
            Text = pInitialValue,
            Multiline = false
         };
         mStatusStrip = new StatusStrip {
            SizingGrip = false,
            Dock = DockStyle.Bottom
         };
         mHelpButton = new Button {
            Text = "&Help",
            AutoSize = true
         };
         mOKButton = new Button {
            Text = "&OK",
            AutoSize = true
         };
         mCancelButton = new Button {
            Text = "&Cancel",
            AutoSize = true
         };
         mHelpHost = new ToolStripControlHost(mHelpButton);
         mOKHost = new ToolStripControlHost(mOKButton);
         mCancelHost = new ToolStripControlHost(mCancelButton);
         mSpringLabel = new ToolStripStatusLabel {
            Spring = true
         };
         mStatusStrip.Items.AddRange(mHelpHost, mSpringLabel, mOKHost, mCancelHost);
         mOKButton.Click += OKButton_Click;
         mCancelButton.Click += CancelButton_Click;
         mHelpButton.Click += HelpButton_Click;
         mInputTextBox.KeyDown += InputTextBox_KeyDown;
         CreateLayout();
      }

      public static void Show(string pTitle, string pPrompt, string pInitialValue, Action<string?, bool> pCallback) {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         mPreGetStringBounds = mForm.Bounds;
         mGetStringPanel = new GetString(pTitle, pPrompt, pInitialValue) {
            OnClose = pCallback
         };
         mForm.SuspendLayout();
         mForm.Controls.Add(mGetStringPanel);
         mGetStringPanel.PerformLayout();
         mForm.ResumeLayout(true);
         CenterControl(mForm, mGetStringPanel);
         Size requiredSize = new Size(mGetStringPanel.Width + 20, mGetStringPanel.Height + 40);
         if (mForm.ClientSize.Width < requiredSize.Width || mForm.ClientSize.Height < requiredSize.Height) {
            mForm.ClientSize = requiredSize;
            CenterControl(mForm, mGetStringPanel);
         }
         EnsureWindowFitsMonitor(mForm, false);
         mGetStringPanel.Visible = true;
         mGetStringPanel.BringToFront();
         mGetStringPanel.Show();
         mGetStringPanel.FocusInputTextBox();
      }

      public static void Restore() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mGetStringPanel, nameof(mGetStringPanel));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         mForm.SuspendLayout();
         mGetStringPanel.Visible = false;
         mGetStringPanel.SendToBack();
         if (mForm.Controls.Contains(mGetStringPanel))
            mForm.Controls.Remove(mGetStringPanel);
         mGetStringPanel.Dispose();
         mGetStringPanel = null;
         mForm.Bounds = mPreGetStringBounds;
         mForm.ResumeLayout(true);
      }

      private void CreateLayout() {
         SuspendLayout();
         ApplyTheme();
         Controls.AddRange(mTitleCluster, mPromptLabel, mStatusStrip, mInputTextBox);
         mTitleCluster.LayoutCluster();
         if (!string.IsNullOrEmpty(mInitialValue)) {
            SizeTextBoxToFitString(out SizeF size, mInputTextBox, mInitialValue);
            mInputTextBox.Width = (int)Math.Max(300, size.Width);
            mInputTextBox.Height = (int)size.Height;
         }
         else {
            mInputTextBox.Width = 300;
            SizeTextBoxToFitString(out SizeF size, mInputTextBox);
            mInputTextBox.Height = (int)Math.Ceiling(size.Height);
         }
         mInputTextBox.Top = mTitleCluster.Height + mPromptLabel.Height + mEm;
         mInputTextBox.Left = mEm;
         SizePanel(this, mEm);
         Width += SystemInformation.VerticalScrollBarWidth;
         Height = mTitleCluster.Height + mPromptLabel.Height + mInputTextBox.Height + mStatusStrip.Height + mEm3 +
            SystemInformation.HorizontalScrollBarHeight;
         mInputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
         Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
         ResumeLayout(true);
      }

      private void ApplyTheme() {
         Theme theme = mCurrentTheme!;
         Theme.ThemeInterfaceThings(theme, out Font interfaceFont, out Color interfaceForeColor,
            out Color interfaceBackColor);
         BackColor = theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground];
         mPromptLabel.Font = interfaceFont;
         mPromptLabel.ForeColor = interfaceForeColor;
         mPromptLabel.BackColor = interfaceBackColor;
         Theme.ThemeTextBoxThings(theme, out Font textBoxFont, out Color textBoxForeColor,
            out Color textBoxBackColor);
         mInputTextBox.Font = textBoxFont;
         mInputTextBox.ForeColor = textBoxForeColor;
         mInputTextBox.BackColor = textBoxBackColor;
         Theme.ThemeStatusThings(theme, out Font statusFont, out Color statusForeColor, out Color statusBackColor);
         mStatusStrip.Renderer = new ToolStripProfessionalRenderer();
         mStatusStrip.BackColor = statusBackColor;
         foreach (ToolStripItem item in mStatusStrip.Items) {
            if (item is ToolStripControlHost host) {
               Control control = host.Control;
               control.Font = statusFont;
               control.ForeColor = statusForeColor;
               control.BackColor = statusBackColor;
               control.Invalidate();
               control.Update();
            }
         }
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

      private void HelpButton_Click(object? pSender, EventArgs pEventArguments) {
         MainForm.Help_Click(pSender, pEventArguments);
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
            mCancelButton.Click -= CancelButton_Click;
            mHelpButton.Click -= HelpButton_Click;
            mInputTextBox.KeyDown -= InputTextBox_KeyDown;
         }
         base.Dispose(pDisposing);
      }
   }
}
