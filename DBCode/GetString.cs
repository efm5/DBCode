namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class GetString : DraggablePanel {
         private bool mHasControlBox; // instance field; captured before dispose in Restore()
         private readonly BottomPanel mGetStringBottomPanel;
         private readonly Button mOKButton;
         private readonly HeaderLabelCluster mTitleCluster;
         private readonly Label mPromptLabel;
         private readonly ScrollablePanel mOuterPanel;
         private readonly Panel mInnerPanel;
         private readonly string mInitialValue;
         private readonly TextBox mInputTextBox;

         public string? ResultValue => WasCancelled ? null : mInputTextBox.Text;
         public bool WasCancelled { get; private set; } = false;

         [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
         public Action<string?, bool>? OnClose { get; set; }

         public GetString(string pTitle, string pPrompt, string pInitialValue = "") {
            ThrowIfNull(pTitle, nameof(pTitle));
            ThrowIfNull(pPrompt, nameof(pPrompt));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mHasControlBox = mForm!.ControlBox;
            if (mHasControlBox)
               mForm.ControlBox = false;
            mInitialValue = pInitialValue;
#pragma warning disable IDE0017
            mOuterPanel = new ScrollablePanel {
               Name = $"GetString_OuterPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEm)    // inset within DraggablePanel (this)
            };
#pragma warning restore IDE0017
            mOuterPanel.Dock = DockStyle.None; //Override default
            mInnerPanel = new Panel {
               Name = $"GetString_InnerPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEmHalf),
               AutoScroll = false
            };
            mTitleCluster = new HeaderLabelCluster(mCurrentTheme!, pTitle, HeaderLabelSize.Normal);
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
            // LayoutClusters() sizes mInnerPanel, mOuterPanel, and this (DraggablePanel) in order,
            // and sets all BackColors. Attach(mForm) is called from Show() after construction.
            LayoutClusters();
         }

         public static void Show(string pTitle, string pPrompt, string pInitialValue, Action<string?, bool> pCallback) {
            ThrowIfNull(mForm, nameof(mForm));
            // Invariant: Form must have exactly one direct child and it must be a ScrollablePanel.
            if (mForm!.Controls.Count != 1 || mForm.Controls[0] is not ScrollablePanel)
               throw new InvalidOperationException(
                  "GetString.Show: Form must have exactly one direct child control and it must be a ScrollablePanel.");
            mUiState.FormBounds = mForm.Bounds;
            mGetStringPanel = new GetString(pTitle, pPrompt, pInitialValue) {
               OnClose = pCallback
            };
            // Attach() uses the self-hosting overload: GetString has already sized itself in
            // LayoutClusters() and owns its content in Controls. Attach() just registers with
            // the Form, disables the ScrollablePanel, centers, and makes visible.
            mGetStringPanel.Attach(mForm);
            mGetStringPanel.FocusInputTextBox();
         }

         public static void Restore() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mGetStringPanel, nameof(mGetStringPanel));
            bool hadControlBox = mGetStringPanel!.mHasControlBox; // capture before dispose
            mGetStringPanel.Detach();
            mGetStringPanel.Dispose();
            mGetStringPanel = null;
            if (mForm!.Size != mUiState.FormBounds.Size)          // only restore if Show() enlarged the Form
               mForm.Bounds = mUiState.FormBounds;
            if (hadControlBox)
               mForm.ControlBox = true;
            mActiveLayoutable?.LayoutControls();                   // defensive: ensures layout reflects restored bounds
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
            //[is this an unnecessary duplication]            mTitleCluster.LayoutCluster();
            mGetStringBottomPanel.LayoutControls();             // get correct sizes after font is applied
            mInnerPanel.Size = new Size(
               wantedWidth + mEm,
               mTitleCluster.Height + mPromptLabel.Height + mInputTextBox.Height + mGetStringBottomPanel.Height);
            mGetStringBottomPanel.LayoutControls();             // recalculate needed width after inner panel is sized
                                                                // mOuterPanel (ScrollablePanel) wraps mInnerPanel with mEmHalf inset on all four sides.
            mOuterPanel.Size = new Size(
               mInnerPanel.Width + (mEmHalf * 2),
               mInnerPanel.Height + (mEmHalf * 2));
            // DraggablePanel (this) wraps mOuterPanel with mEm top and mEmHalf left/right/bottom.
            Size = new Size(
               mOuterPanel.Width + (mEmHalf * 2),
               mOuterPanel.Height + mEm + mEmHalf);
            mInnerPanel.ResumeLayout(true);
            ResumeLayout(true);
         }

         // ApplyDragTone() is called by DraggablePanel.AttachCore() after the ScrollablePanel has
         // been disabled and sampled. Sets contrast colors for the drag-handle ring and the
         // middle contrasting ring based on the dominant tone of the disabled background.
         protected override void ApplyDragTone(ColorTones pTone) {
            bool isDarkBackground = pTone == ColorTones.Dark || pTone == ColorTones.MediumDark;
            // DraggablePanel (this): outermost drag-handle ring — contrast against Form background.
            BackColor = isDarkBackground ? Color.White : Color.Black;
            // mOuterPanel (ScrollablePanel): middle contrasting ring — contrast against this.BackColor.
            ColorTones outerTone = ColorTone.GetTone(BackColor);
            bool isOuterDark = outerTone == ColorTones.Dark || outerTone == ColorTones.MediumDark;
            mOuterPanel.BackColor = isOuterDark ? Color.White : Color.Black;
         }

         private void ApplyTheme() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            Theme theme = mCurrentTheme!;
            Theme.ThemeInterfaceThings(theme, out Font interfaceFont, out Color interfaceForeColor,
               out Color interfaceBackColor);
            // mInnerPanel: content area — themed normally.
            // Contrast colors for this and mOuterPanel are set in ApplyDragTone(), which is called
            // by AttachCore() after the ScrollablePanel is disabled and sampled.
            mInnerPanel.BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
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

         private void OKButton_Click(object? pSender, EventArgs pEventArguments) =>
            CloseDialog(false);

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) =>
            CloseDialog(true);

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
}
