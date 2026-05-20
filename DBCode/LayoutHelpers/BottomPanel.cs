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
               Text = "&Help",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Top = 1
            };
            mHelpButton.Click += MainForm.Help_Click;
            mCancelButton = new Button() {
               Name = $"BottomPanelDefaultButtonsCancel{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = string.IsNullOrEmpty(pCancelText) ? "&Cancel" : pCancelText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Top = 1
            };
            Controls.AddRange([mCancelButton, mHelpButton]);
            Anchor = mAnchorBottomLeftRight;
            AutoSize = false;
         }

         public int NeededWidth {
            get {
               ThrowIfNull(mHelpButton, nameof(mHelpButton));
               ThrowIfNull(mCancelButton, nameof(mCancelButton));
               List<string> leftTexts = [];
               List<string> rightTexts = [];
               int result;
               Theme.ThemeStatusThings(mTheme, out Font poFont, out _, out _);
               Font font = CreateNewFont(poFont);
               foreach (Control c in mLeftControls)
                  leftTexts.Add(c.Text);
               foreach (Control c in mRightControls)
                  rightTexts.Add(c.Text);
               result = MeasureHelper.BottomPanelNeededWidth(
                  mHelpButton.Text, mCancelButton.Text, leftTexts, rightTexts, font);
               MainForm.DisposeFontIfOwned(font);
               return result;
            }
         }

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
            ThrowIfNull(mHelpButton, nameof(mHelpButton));
            ThrowIfNull(mCancelButton, nameof(mCancelButton));
            Control? parent = Parent;
            if (parent == null)
               return;
            SetFontAndColor();
            SetBottomPanelHeight(this);
            int parentHeight = parent.Height;
            int parentWidth = parent.Width;
            if (parent is Panel p) {
               if (p.HorizontalScroll.Visible)
                  Location = new Point(1, parentHeight - Height - 1 - SystemInformation.HorizontalScrollBarHeight);
               else
                  Location = new Point(1, parentHeight - Height - 1);
            }
            else
               Location = new Point(1, parentHeight - Height - 1);
            Width = parentWidth - 2 - SystemInformation.VerticalScrollBarWidth;
            // Left pass: Help then mLeftControls
            mHelpButton.Left = mIndent;
            int leftEdge = mHelpButton.Right + mEm2;
            foreach (Control control in mLeftControls) {
               control.Left = leftEdge;
               leftEdge += control.Width + mEm;
            }
            // Right pass: Cancel at far right, then mRightControls leftward
            PositionRightControls();
            int centerY = Height / 2;
            foreach (Control control in Controls)
               control.Top = centerY - (control.Height / 2);
         }

         public void PositionRightControls() {
            ThrowIfNull(mCancelButton, nameof(mCancelButton));
            mCancelButton.Left = Width - mCancelButton.Width - mCancelOffset;
            int rightEdge = mCancelButton.Left - mCancelOffset;
            for (int i = mRightControls.Count - 1; i >= 0; i--) {
               Control control = mRightControls[i];
               control.Left = rightEdge - control.Width;
               rightEdge = control.Left - mEm;
            }
         }

         internal void SetFontAndColor() {
            ThrowIfNull(mHelpButton, nameof(mHelpButton));
            ThrowIfNull(mCancelButton, nameof(mCancelButton));
            Theme.ThemeStatusThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            Font oldHelp = mHelpButton.Font;
            mHelpButton.Font = CreateNewFont(poFont);
            MainForm.DisposeFontIfOwned(oldHelp);
            mHelpButton.ForeColor = poForeColor;
            mHelpButton.BackColor = poBackColor;
            Font oldCancel = mCancelButton.Font;
            mCancelButton.Font = CreateNewFont(poFont);
            MainForm.DisposeFontIfOwned(oldCancel);
            mCancelButton.ForeColor = poForeColor;
            mCancelButton.BackColor = poBackColor;
            foreach (Control control in mLeftControls) {
               if (mBottomPanelExcluded != null && mBottomPanelExcluded.Contains(control))
                  continue;
               Font oldLeft = control.Font;
               control.Font = CreateNewFont(poFont);
               MainForm.DisposeFontIfOwned(oldLeft);
               control.ForeColor = poForeColor;
               control.BackColor = poBackColor;
            }
            foreach (Control control in mRightControls) {
               if (mBottomPanelExcluded != null && mBottomPanelExcluded.Contains(control))
                  continue;
               Font oldRight = control.Font;
               control.Font = CreateNewFont(poFont);
               MainForm.DisposeFontIfOwned(oldRight);
               control.ForeColor = poForeColor;
               control.BackColor = poBackColor;
            }
            BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               ThrowIfNull(mHelpButton, nameof(mHelpButton));
               ThrowIfNull(mCancelButton, nameof(mCancelButton));
               mHelpButton.Click -= MainForm.Help_Click;
               MainForm.DisposeFontIfOwned(mHelpButton.Font);
               MainForm.DisposeFontIfOwned(mCancelButton.Font);
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
