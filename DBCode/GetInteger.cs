namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class GetInteger : DraggablePanel {
         private bool mHasControlBox;
         private readonly BottomPanel mGetIntegerBottomPanel;
         private readonly Button mOKButton;
         private readonly HeaderLabelCluster mTitleCluster;
         private readonly Label mPromptLabel;
         private readonly ScrollablePanel mOuterPanel;
         private readonly Panel mInnerPanel;
         private readonly NumericUpDown mUpDown;

         public int? ResultValue => WasCancelled ? null : (int)mUpDown.Value;
         public bool WasCancelled { get; private set; } = false;

         [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
         public Action<int?, bool>? OnClose { get; set; }

         public GetInteger(string pTitle, string pPrompt, int pInitialValue, int pMinValue, int pMaxValue) {
            ThrowIfNull(pTitle, nameof(pTitle));
            ThrowIfNull(pPrompt, nameof(pPrompt));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mHasControlBox = mForm.ControlBox;
            if (mHasControlBox)
               mForm.ControlBox = false;
            mOuterPanel = new ScrollablePanel {
               Name = $"GetInteger_OuterPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEm),
               Dock = DockStyle.None
            };
            mInnerPanel = new Panel {
               Name = $"GetInteger_InnerPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEmHalf),
               AutoScroll = false,
               BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
            };
            mTitleCluster = new HeaderLabelCluster(mCurrentTheme, pTitle, HeaderLabelSize.Normal);
            mPromptLabel = new Label {
               Name = $"GetInteger_PromptLabel{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pPrompt,
               AutoSize = true,
               Dock = DockStyle.Top
            };
            mUpDown = new NumericUpDown {
               Name = $"GetInteger_InputUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = pMinValue,
               Maximum = pMaxValue,
               Value = Math.Clamp(pInitialValue, pMinValue, pMaxValue),
               DecimalPlaces = 0
            };
            mOKButton = new Button {
               Name = $"GetIntegerOKButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&OK",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mGetIntegerBottomPanel = new BottomPanel(mCurrentTheme, pCancelText: "&Cancel");
            ThrowIfNull(mGetIntegerBottomPanel.mHelpButton, nameof(mGetIntegerBottomPanel.mHelpButton));
            ThrowIfNull(mGetIntegerBottomPanel.mCancelButton, nameof(mGetIntegerBottomPanel.mCancelButton));
            mGetIntegerBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.Main, "GetInteger");
            mGetIntegerBottomPanel.AddRightControl(mOKButton);
            mGetIntegerBottomPanel.mCancelButton.Click += CancelButton_Click;
            mOKButton.Click += OKButton_Click;
            mUpDown.KeyDown += InputUpDown_KeyDown;
            mInnerPanel.Controls.AddRange([mGetIntegerBottomPanel, mUpDown, mPromptLabel, mTitleCluster]);
            mOuterPanel.Controls.Add(mInnerPanel);
            Controls.Add(mOuterPanel);
            LayoutClusters();
         }

         public static void ShowMe(string pTitle, string pPrompt, int pInitialValue, int pMinValue,
            int pMaxValue, Action<int?, bool> pCallback) {
            ThrowIfNull(mForm, nameof(mForm));
            if (mForm.Controls.Count != 1 || mForm.Controls[0] is not ScrollablePanel)
               throw new InvalidOperationException(
                  "GetInteger.ShowMe: Form must have exactly one direct child control and it must be a ScrollablePanel.");
            mUiState.FormBounds = mForm.Bounds;
            mGetIntegerPanel = new GetInteger(pTitle, pPrompt, pInitialValue, pMinValue, pMaxValue) {
               OnClose = pCallback
            };
            if (mForm.ClientSize.Width < mGetIntegerPanel.Width + mEm2 * 2 || mForm.ClientSize.Height < mGetIntegerPanel.Height + mEm2 * 2)
               mForm.ClientSize = new Size(
                  Math.Max(mForm.ClientSize.Width, mGetIntegerPanel.Width + mEm2 * 2),
                  Math.Max(mForm.ClientSize.Height, mGetIntegerPanel.Height + mEm2 * 2));
            mGetIntegerPanel.Attach(mForm);
            mGetIntegerPanel.FocusInputUpDown();
         }

         public static void Restore() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mGetIntegerPanel, nameof(mGetIntegerPanel));
            ThrowIfNull(mActiveLayoutable, nameof(mActiveLayoutable));
            bool hadControlBox = mGetIntegerPanel.mHasControlBox;
            mGetIntegerPanel.Detach();
            mGetIntegerPanel.Dispose();
            mGetIntegerPanel = null;
            if (mForm.Size != mUiState.FormBounds.Size)
               mForm.Bounds = mUiState.FormBounds;
            if (hadControlBox)
               mForm.ControlBox = true;
            mActiveLayoutable.LayoutControls();
         }

         private void LayoutClusters() {
            SuspendLayout();
            mInnerPanel.SuspendLayout();
            ApplyTheme();
            mTitleCluster.LayoutCluster();
            SetUpDownBoxWidth(mUpDown);
            mPromptLabel.Left = mIndent;
            mUpDown.Left = mIndent;
            mGetIntegerBottomPanel.LayoutControls();
            int wantedWidth = mTitleCluster.Width;
            wantedWidth = Math.Max(wantedWidth, mPromptLabel.Width);
            wantedWidth = Math.Max(wantedWidth, mUpDown.Width);
            wantedWidth = Math.Max(wantedWidth, mGetIntegerBottomPanel.NeededWidth);
            mInnerPanel.Size = new Size(wantedWidth + mEm,
               mTitleCluster.Height + mPromptLabel.Height + mUpDown.Height + mGetIntegerBottomPanel.Height + mEm);
            mGetIntegerBottomPanel.LayoutControls();
            SetUpDownBoxWidth(mUpDown);
            mOuterPanel.Size = new Size(mInnerPanel.Width + mEm, mInnerPanel.Height + mEm);
            Size = new Size(mOuterPanel.Width + mEm, mOuterPanel.Height + mEm + mEmHalf);
            mPromptLabel.Top = mTitleCluster.Bottom;
            mUpDown.Top = mPromptLabel.Bottom + mEm;
            mInnerPanel.ResumeLayout(true);
            ResumeLayout(true);
         }

         protected override void ApplyDragTone(ColorTones pTone) {
            bool isDarkBackground = pTone == ColorTones.Dark || pTone == ColorTones.MediumDark;
            BackColor = isDarkBackground ? Color.White : Color.Black;
            ColorTones outerTone = ColorTone.GetTone(BackColor);
            bool isOuterDark = outerTone == ColorTones.Dark || outerTone == ColorTones.MediumDark;
            mOuterPanel.BackColor = isOuterDark ? Color.White : Color.Black;
         }

         private void ApplyTheme() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            Theme theme = mCurrentTheme;
            Theme.ThemeInterfaceThings(theme, out Font interfaceFont, out Color interfaceForeColor,
               out Color interfaceBackColor);
            mInnerPanel.BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font oldPrompt = mPromptLabel.Font;
            mPromptLabel.Font = CreateNewFont(interfaceFont);
            MainForm.DisposeFontIfOwned(oldPrompt);
            mPromptLabel.ForeColor = interfaceForeColor;
            mPromptLabel.BackColor = interfaceBackColor;
            Theme.ThemeTextBoxThings(theme, out Font textBoxFont, out Color textBoxForeColor,
               out Color textBoxBackColor);
            Font oldUpDown = mUpDown.Font;
            mUpDown.Font = CreateNewFont(textBoxFont);
            MainForm.DisposeFontIfOwned(oldUpDown);
            mUpDown.ForeColor = textBoxForeColor;
            mUpDown.BackColor = textBoxBackColor;
         }

         internal void FocusInputUpDown() {
            mUpDown.Focus();
            mUpDown.Select(0, mUpDown.Text.Length);
         }

         private void CloseDialog(bool pCancelled) {
            WasCancelled = pCancelled;
            ThrowIfNull(OnClose, nameof(OnClose));
            OnClose.Invoke(ResultValue, WasCancelled);
         }

         private void OKButton_Click(object? pSender, EventArgs pEventArguments) =>
            CloseDialog(false);

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) =>
            CloseDialog(true);

         private void InputUpDown_KeyDown(object? pSender, KeyEventArgs pEventArguments) {
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
               ThrowIfNull(mGetIntegerBottomPanel.mCancelButton, nameof(mGetIntegerBottomPanel.mCancelButton));
               mGetIntegerBottomPanel.mCancelButton.Click -= CancelButton_Click;
               mUpDown.KeyDown -= InputUpDown_KeyDown;
               MainForm.DisposeFontIfOwned(mPromptLabel.Font);
               MainForm.DisposeFontIfOwned(mUpDown.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
