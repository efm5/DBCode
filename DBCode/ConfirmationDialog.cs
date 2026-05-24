namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ConfirmationDialog : DraggablePanel {
         private readonly BottomPanel mDialogBottomPanel;
         private readonly Button mYesButton;
         private readonly HeaderLabelCluster mTitleCluster;
         private readonly Label mMessageLabel;
         private readonly ScrollablePanel mOuterPanel;
         private readonly Panel mInnerPanel;

         [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
         public Action<bool>? OnClose { get; set; }

         private ConfirmationDialog(string pTitle, string pMessage, string pYesText, string pNoText) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mOuterPanel = new ScrollablePanel {
               Name = $"ConfirmationDialog_OuterPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEm),
               Dock = DockStyle.None
            };
            mInnerPanel = new Panel {
               Name = $"ConfirmationDialog_InnerPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEmHalf),
               AutoScroll = false
            };
            mTitleCluster = new HeaderLabelCluster(mCurrentTheme, pTitle, HeaderLabelSize.Normal);
            mMessageLabel = new Label {
               Name = $"ConfirmationDialog_MessageLabel{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pMessage,
               AutoSize = true,
               MaximumSize = new Size(mEm * 30, 0),
               Dock = DockStyle.Top
            };
            mYesButton = new Button {
               Name = $"ConfirmationDialogYesButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pYesText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mDialogBottomPanel = new BottomPanel(mCurrentTheme, pNoText) {
               Dock = DockStyle.Top
            };
            ThrowIfNull(mDialogBottomPanel.mHelpButton, nameof(mDialogBottomPanel.mHelpButton));
            ThrowIfNull(mDialogBottomPanel.mCancelButton, nameof(mDialogBottomPanel.mCancelButton));
            mDialogBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.Options);
            mDialogBottomPanel.AddRightControl(mYesButton);
            mDialogBottomPanel.mCancelButton.Click += NoButton_Click;
            mYesButton.Click += YesButton_Click;
            mInnerPanel.Controls.AddRange([mDialogBottomPanel, mMessageLabel, mTitleCluster]);
            mOuterPanel.Controls.Add(mInnerPanel);
            Controls.Add(mOuterPanel);
            LayoutClusters();
         }

         public static void ShowMe(string pTitle, string pMessage, string pYesText, string pNoText,
               Action<bool> pCallback) {
            ThrowIfNull(mForm, nameof(mForm));
            mConfirmationPanel?.Dispose();
            mConfirmationPanel = new ConfirmationDialog(pTitle, pMessage, pYesText, pNoText) {
               OnClose = pCallback
            };
            if (mForm.ClientSize.Width < mConfirmationPanel.Width + mEm2 * 2 ||
                  mForm.ClientSize.Height < mConfirmationPanel.Height + mEm2 * 2)
               mForm.ClientSize = new Size(
                  Math.Max(mForm.ClientSize.Width, mConfirmationPanel.Width + mEm2 * 2),
                  Math.Max(mForm.ClientSize.Height, mConfirmationPanel.Height + mEm2 * 2));
            mConfirmationPanel.Attach(mForm);
         }

         public static void Restore() {
            ThrowIfNull(mConfirmationPanel, nameof(mConfirmationPanel));
            mConfirmationPanel.Detach();
            mConfirmationPanel.Dispose();
            mConfirmationPanel = null;
         }

         private void LayoutClusters() {
            SuspendLayout();
            mInnerPanel.SuspendLayout();
            ApplyTheme();
            mTitleCluster.LayoutCluster();
            int wantedWidth = mTitleCluster.Width;
            wantedWidth = Math.Max(wantedWidth, mMessageLabel.Width);
            mDialogBottomPanel.LayoutControls();
            wantedWidth = Math.Max(wantedWidth, mDialogBottomPanel.NeededWidth);
            mInnerPanel.Size = new Size(wantedWidth + mEm,
               mTitleCluster.Height + mMessageLabel.Height + mDialogBottomPanel.Height);
            mDialogBottomPanel.LayoutControls();
            mOuterPanel.Size = new Size(mInnerPanel.Width + (mEmHalf * 2), mInnerPanel.Height + (mEmHalf * 2));
            Size = new Size(mOuterPanel.Width + (mEmHalf * 2), mOuterPanel.Height + mEm + mEmHalf);
            mInnerPanel.ResumeLayout(true);
            ResumeLayout(true);
         }

         protected override void ApplyDragTone(ColorTones pTone) {
            bool isDark = pTone == ColorTones.Dark || pTone == ColorTones.MediumDark;
            BackColor = isDark ? Color.White : Color.Black;
            ColorTones outerTone = ColorTone.GetTone(BackColor);
            bool isOuterDark = outerTone == ColorTones.Dark || outerTone == ColorTones.MediumDark;
            mOuterPanel.BackColor = isOuterDark ? Color.White : Color.Black;
         }

         private void ApplyTheme() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            Theme.ThemeInterfaceThings(mCurrentTheme, out Font interfaceFont, out Color interfaceForeColor,
               out Color interfaceBackColor);
            mInnerPanel.BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font previousFont = mMessageLabel.Font;
            mMessageLabel.Font = CreateNewFont(interfaceFont);
            MainForm.DisposeFontIfOwned(previousFont);
            mMessageLabel.ForeColor = interfaceForeColor;
            mMessageLabel.BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
         }

         private void CloseDialog(bool pConfirmed) {
            ThrowIfNull(OnClose, nameof(OnClose));
            OnClose.Invoke(pConfirmed);
         }

         private void YesButton_Click(object? pSender, EventArgs pEventArguments) {
            CloseDialog(true);
         }
         private void NoButton_Click(object? pSender, EventArgs pEventArguments) {
            CloseDialog(false);
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mYesButton.Click -= YesButton_Click;
               ThrowIfNull(mDialogBottomPanel.mCancelButton, nameof(mDialogBottomPanel.mCancelButton));
               mDialogBottomPanel.mCancelButton.Click -= NoButton_Click;
               MainForm.DisposeFontIfOwned(mMessageLabel.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
