namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class BottomPanel : Panel {
         private readonly List<Control> mLeftControls = [];
         private readonly List<Control> mRightControls = [];
         internal Button? mHelpButton = null, mOKButton = null, mCancelButton = null;
         private Theme mTheme;

         public BottomPanel(Theme pTheme, bool pDoOkay, string pOKText, string pCancelText = "") {
            mTheme = pTheme;
            mHelpButton = new Button() {
               Name = $"BottomPanelDefaultButtonsHelp{mTabIndex}",
               TabIndex = mTabIndex++,
               Tag = new HelpTag(HelpContext.BottomPanel, "BottomPanelHelp"),
               Text = "&Help",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Top = 1
            };
            Controls.Add(mHelpButton);
            mHelpButton.Click += MainForm.Help_Click;
            if (pDoOkay) {
               mOKButton = new Button() {
                  Name = $"BottomPanelDefaultButtonsOK{mTabIndex}",
                  TabIndex = mTabIndex++,
                  Text = pOKText,
                  AutoSize = true,
                  AutoSizeMode = AutoSizeMode.GrowAndShrink,
                  Top = 1
               };
               Controls.Add(mOKButton);
            }
            mCancelButton = new Button() {
               Name = $"BottomPanelDefaultButtonsCancel{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = string.IsNullOrEmpty(pCancelText) ? "&Cancel" : pCancelText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Top = 1
            };
            Controls.Add(mCancelButton);
            Anchor = mAnchorBottomLeftRight;
            AutoSize = false;
         }

         public int NeededWidth => mHelpButton!.Width + mEm2 + mLeftControls.Sum(pC => pC.Width + mEm) + mRightControls.Sum(pC => pC.Width + mEm) + (mOKButton?.Width ?? 0) + (mCancelButton?.Width ?? 0) + mCancelOffset * 2;

         internal Control AddLeftControl(Control pControl) {
            mLeftControls.Add(pControl);
            Controls.Add(pControl);
            return pControl;
         }

         internal Control AddRightControl(Control pControl) {
            mRightControls.Add(pControl);
            Controls.Add(pControl);
            return pControl;
         }

         internal void LayoutControls() {
            Control? parent = Parent;
            if (parent == null)
               return;
            SetFontAndColor();
            SetBottomPanelHeight(this);
            int parentHeight = parent.Height;
            int parentWidth = parent.Width;
            Location = new Point(1, parentHeight - Height - 1);
            Width = parentWidth - 2 - SystemInformation.VerticalScrollBarWidth;
            // Left pass: Help then mLeftControls
            mHelpButton!.Left = mIndent;
            int leftEdge = mHelpButton.Right + mEm2;
            foreach (Control control in mLeftControls) {
               control.Left = leftEdge;
               leftEdge += control.Width + mEm;
            }
            // Right pass: Cancel (Exit) at far right, then mRightControls leftward, then OK (Revert)
            mCancelButton!.Left = Width - mCancelButton.Width - mCancelOffset;
            int rightEdge = mCancelButton.Left - mCancelOffset;
            if (mOKButton != null) {
               mOKButton.Left = rightEdge - mOKButton.Width;
               rightEdge = mOKButton.Left - mEm;
            }
            for (int i = mRightControls.Count - 1; i >= 0; i--) {
               Control control = mRightControls[i];
               control.Left = rightEdge - control.Width;
               rightEdge = control.Left - mEm;
            }
         }

         internal void SetFontAndColor() {
            Theme.ThemeStatusThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mHelpButton!.Font = CreateNewFont(poFont);
            mHelpButton.ForeColor = poForeColor;
            mHelpButton.BackColor = poBackColor;
            if (mOKButton != null) {
               mOKButton.Font = CreateNewFont(poFont);
               mOKButton.ForeColor = poForeColor;
               mOKButton.BackColor = poBackColor;
            }
            mCancelButton!.Font = CreateNewFont(poFont);
            mCancelButton.ForeColor = poForeColor;
            mCancelButton.BackColor = poBackColor;
            foreach (Control control in mLeftControls) {
               control.Font = CreateNewFont(poFont);
               control.ForeColor = poForeColor;
               control.BackColor = poBackColor;
            }
            foreach (Control control in mRightControls) {
               control.Font = CreateNewFont(poFont);
               control.ForeColor = poForeColor;
               control.BackColor = poBackColor;
            }
            BackColor = poBackColor;
         }
      }
   }
}
