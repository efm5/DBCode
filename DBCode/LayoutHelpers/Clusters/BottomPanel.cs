namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class BottomPanel : Panel, ILayoutable {
         private readonly List<Control> mLeftControls = [];
         private readonly List<Control> mRightControls = [];
         internal Button? mHelpButton = null, mCancelButton = null;
         private Theme mTheme;

         public BottomPanel(Theme pTheme, string pCancelText = "") {
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

         public int NeededWidth => mHelpButton!.Width + mEm2 + mLeftControls.Sum(pC => pC.Width + mEm) +
            mRightControls.Sum(pC => pC.Width + mEm) + mCancelButton!.Width + mCancelOffset * 2;

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

         public void LayoutControls() {
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
            // Right pass: Cancel at far right, then mRightControls leftward
            mCancelButton!.Left = Width - mCancelButton.Width - mCancelOffset;
            int rightEdge = mCancelButton.Left - mCancelOffset;
            for (int i = mRightControls.Count - 1; i >= 0; i--) {
               Control control = mRightControls[i];
               control.Left = rightEdge - control.Width;
               rightEdge = control.Left - mEm;
            }
            int centerY = Height / 2;
            foreach (Control control in Controls)
               control.Top = centerY - (control.Height / 2);
         }

         internal void SetFontAndColor() {
            Theme.ThemeStatusThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            MainForm.DisposeFontIfOwned(mHelpButton!.Font);
            mHelpButton.Font = CreateNewFont(poFont);
            mHelpButton.ForeColor = poForeColor;
            mHelpButton.BackColor = poBackColor;
            MainForm.DisposeFontIfOwned(mCancelButton!.Font);
            mCancelButton.Font = CreateNewFont(poFont);
            mCancelButton.ForeColor = poForeColor;
            mCancelButton.BackColor = poBackColor;
            foreach (Control control in mLeftControls) {
               MainForm.DisposeFontIfOwned(control.Font);
               control.Font = CreateNewFont(poFont);
               control.ForeColor = poForeColor;
               control.BackColor = poBackColor;
            }
            foreach (Control control in mRightControls) {
               MainForm.DisposeFontIfOwned(control.Font);
               control.Font = CreateNewFont(poFont);
               control.ForeColor = poForeColor;
               control.BackColor = poBackColor;
            }
            BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mHelpButton!.Click -= MainForm.Help_Click;
               MainForm.DisposeFontIfOwned(mHelpButton!.Font);
               MainForm.DisposeFontIfOwned(mCancelButton!.Font);
               foreach (Control control in mLeftControls) {
                  MainForm.DisposeFontIfOwned(control.Font);
                  Controls.Remove(control);
               }
               foreach (Control control in mRightControls) {
                  MainForm.DisposeFontIfOwned(control.Font);
                  Controls.Remove(control);
               }
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
