using DBCode.Diagnostics;

namespace DBCode {
   namespace Themes {
      internal sealed class VariableWidthTabControl : TabControl {
         public readonly List<int> TabHeaderWidths = [];

         protected override void OnMouseDown(MouseEventArgs pMouseEventArgs) {
            Point mouseLocation = pMouseEventArgs.Location;
            int tabIndex = HitTestTabHeaders(mouseLocation);
            if (tabIndex >= 0)
               SelectedIndex = tabIndex;
            else
               base.OnMouseDown(pMouseEventArgs);
         }

         private int HitTestTabHeaders(Point pMouseLocation) {
            int currentTabHeaderOffset = 0;
            int tabHeaderTop = GetTabRect(0).Y;

            for (int tabIndex = 0; tabIndex < TabHeaderWidths.Count; tabIndex++) {
               int offsetBefore = currentTabHeaderOffset;
               int tabHeaderWidth = TabHeaderWidths[tabIndex];
               Rectangle tabHeaderRectangle = new Rectangle(offsetBefore, tabHeaderTop, tabHeaderWidth, ItemSize.Height);
               bool containsPoint = tabHeaderRectangle.Contains(pMouseLocation);

               if (containsPoint)
                  return tabIndex;
               int offsetAfter = offsetBefore + tabHeaderWidth;
               currentTabHeaderOffset = offsetAfter;
            }
            return -1;
         }

         private int HitTestVariableWidth(Point pPoint) {
            int x = 0;
            for (int i = 0; i < TabHeaderWidths.Count; i++) {
               Rectangle r = new Rectangle(x, 0, TabHeaderWidths[i], ItemSize.Height);
               if (r.Contains(pPoint))
                  return i;
               x += TabHeaderWidths[i];
            }
            return -1;
         }
      }

      internal sealed class ThemePanel : Panel {
         private readonly Button mApplyButton, mCancelButton, mHelpButton, mOkButton, mNewButton, mOpenButton;
         private readonly TitleLabelCluster mTitleLabel, mFirstTitleLabel, mSecondTitleLabel, mThirdTitleLabel, mFourthTitleLabel, mFifthTitleLabel;
         private readonly StatusStrip mStatusStrip;
         private readonly VariableWidthTabControl mHighlightTabControl, mPrimaryTabControl;
         private readonly ToolStripControlHost mApplyHost, mCancelHost, mHelpHost, mOkHost, mNewHost, mOpenHost;
         private readonly ToolStripStatusLabel mSpringLabel;
         private readonly LabeledButtonTextBoxCluster mFirstCluster, mSecondCluster, mThirdCluster, mFourthCluster;
         private readonly ThemeUsage mThemeUsage;
         private readonly MenuStrip mThemePanelMenuStrip;
         private Theme? mTemporaryTheme;

         public ThemePanel(ThemeUsage pThemeUsage) {
            if (mCurrentTheme == null)
               Fatal.Layout(nameof(mCurrentTheme), "Current theme is null when creating a new Theme Panel.");
            mThemeUsage = pThemeUsage;
            AutoScroll = true;
            AutoSize = false;
            BackColor = Color.Transparent;
            mApplyButton = new Button() ?? throw Fatal.Layout(nameof(mApplyButton), "Constructor failed to create control.");
            mCancelButton = new Button() ?? throw Fatal.Layout(nameof(mCancelButton), "Constructor failed to create control.");
            mHelpButton = new Button() ?? throw Fatal.Layout(nameof(mHelpButton), "Constructor failed to create control.");
            mOkButton = new Button() ?? throw Fatal.Layout(nameof(mOkButton), "Constructor failed to create control.");
            mTitleLabel = new TitleLabelCluster("Themes", 0f) ?? throw Fatal.Layout(nameof(mTitleLabel), "Constructor failed to create control.");
            mFirstTitleLabel = new TitleLabelCluster("Fonts", 1.1f) ?? throw Fatal.Layout(nameof(mFirstTitleLabel), "Constructor failed to create control.");
            mSecondTitleLabel = new TitleLabelCluster("Colors", 1.1f) ?? throw Fatal.Layout(nameof(mSecondTitleLabel), "Constructor failed to create control.");
            mThirdTitleLabel = new TitleLabelCluster("Interface", 1.1f) ?? throw Fatal.Layout(nameof(mThirdTitleLabel), "Constructor failed to create control.");
            mFourthTitleLabel = new TitleLabelCluster("C# 1", 1.1f) ?? throw Fatal.Layout(nameof(mFourthTitleLabel), "Constructor failed to create control.");
            mFifthTitleLabel = new TitleLabelCluster("C# 2", 1.1f) ?? throw Fatal.Layout(nameof(mFifthTitleLabel), "Constructor failed to create control.");
            mStatusStrip = new StatusStrip();
            mHighlightTabControl = new VariableWidthTabControl();
            mPrimaryTabControl = new VariableWidthTabControl();
            mApplyHost = new ToolStripControlHost(mApplyButton);
            mCancelHost = new ToolStripControlHost(mCancelButton);
            mHelpHost = new ToolStripControlHost(mHelpButton);
            mOkHost = new ToolStripControlHost(mOkButton);
            mSpringLabel = new ToolStripStatusLabel();
            mFirstCluster = new LabeledButtonTextBoxCluster("The Interface Font:", "Interface", LabelPosition.Left);
            mSecondCluster = new LabeledButtonTextBoxCluster("The Menu Font:", "Menu", LabelPosition.Left);
            mThirdCluster = new LabeledButtonTextBoxCluster("The Static Strip Font:", "Status Strip", LabelPosition.Left);
            mFourthCluster = new LabeledButtonTextBoxCluster("The Text Box Font:", "Box", LabelPosition.Left);
            mTemporaryTheme = mCurrentTheme?.Clone() ?? throw Fatal.Layout(nameof(mCurrentTheme), "Current theme is null when creating a new Theme Panel.");
         }

         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            CreateLayout();
         }

         private void LayoutPrimaryClusters() {
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface].Controls.Add(mFirstTitleLabel);
            int top = mFirstTitleLabel!.Bottom + mEm;
            LabeledButtonTextBoxCluster[] clusters = [mFirstCluster, mSecondCluster, mThirdCluster, mFourthCluster];
            foreach (LabeledButtonTextBoxCluster cluster in clusters) {
               cluster.Left = mEm;
               cluster.Top = top;
               mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface].Controls.Add(cluster);
               top = cluster.Bottom + mEm;
            }
         }

         private void CreateLayout() {
            SuspendLayout();
            CreateTabControls();
            CreateStatusStrip();
            LayoutPrimaryClusters();
            mApplyButton.Click += ApplyButton_Click;
            mCancelButton.Click += CancelButton_Click;
            mHelpButton.Click += MainForm.Help_Click;
            mOkButton.Click += OkButton_Click;
            Controls.AddRange(mPrimaryTabControl, mStatusStrip, mTitleLabel);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface].Controls.Add(mFirstTitleLabel);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Color].Controls.Add(mSecondTitleLabel);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Interface].Controls.Add(mThirdTitleLabel);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp1].Controls.Add(mFourthTitleLabel);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp2].Controls.Add(mFifthTitleLabel);
            ApplyThemeToThemePanel();
            ResumeLayout(true);
         }

         private void CreateTabControls() {
            mPrimaryTabControl.SuspendLayout();
            mHighlightTabControl.SuspendLayout();
            mPrimaryTabControl.TabPages.AddRange([new TabPage("Fonts"), new TabPage("Colors")]);
            foreach (TabPage tabPage in mPrimaryTabControl.TabPages.OfType<TabPage>())
               tabPage.AutoScroll = true;
            mPrimaryTabControl.Dock = DockStyle.Fill;
            mHighlightTabControl.TabPages.AddRange([new TabPage("Interface"), new TabPage("C# 1"), new TabPage("C# 2")]);
            foreach (TabPage tabPage in mHighlightTabControl.TabPages.OfType<TabPage>())
               tabPage.AutoScroll = true;
            mHighlightTabControl.Dock = DockStyle.Fill;
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Color].Controls.Add(mHighlightTabControl);
            mPrimaryTabControl.SelectedIndex = mThemePrimaryTabPageIndex;
            mHighlightTabControl.SelectedIndex = mThemeHighlightTabPageIndex;
            mPrimaryTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mPrimaryTabControl.DrawItem += new DrawItemEventHandler(PrimaryTabControl_DrawItem);
            mHighlightTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mHighlightTabControl.DrawItem += new DrawItemEventHandler(HighlightTabControl_DrawItem);
            mHighlightTabControl.ResumeLayout(false);
            mPrimaryTabControl.ResumeLayout(false);
         }

         private void CreateStatusStrip() {
            mStatusStrip.SuspendLayout();
            mStatusStrip.SizingGrip = true;
            mStatusStrip.Dock = DockStyle.Bottom;
            mSpringLabel.Spring = true;
            mHelpButton.Text = "Help";
            mApplyButton.Text = "Apply";
            mOkButton.Text = "OK";
            mCancelButton.Text = "Cancel";
            mStatusStrip.Items.Add(mHelpHost);
            mStatusStrip.Items.Add(mSpringLabel);
            mStatusStrip.Items.Add(mApplyHost);
            mStatusStrip.Items.Add(mOkHost);
            mStatusStrip.Items.Add(mCancelHost);
            mStatusStrip.ResumeLayout(false);
         }

         private void PrimaryTabControl_DrawItem(object? pSender, DrawItemEventArgs pDrawItemEventArgs) {
            TabControlDrawItem(mPrimaryTabControl, pDrawItemEventArgs);
         }

         private void HighlightTabControl_DrawItem(object? pSender, DrawItemEventArgs pDrawItemEventArgs) {
            TabControlDrawItem(mHighlightTabControl, pDrawItemEventArgs);
         }

         private void TabControlDrawItem(TabControl pTabControl, DrawItemEventArgs pDrawItemEventArgs) {
            if (mCurrentTheme == null)
               return;
            if (pTabControl is not VariableWidthTabControl variableWidthTabControl)
               return;
            TabPage page = pTabControl.TabPages[pDrawItemEventArgs.Index];
            Rectangle tabRect = pTabControl.GetTabRect(pDrawItemEventArgs.Index);
            bool isSelected = (pTabControl.SelectedIndex == pDrawItemEventArgs.Index);
            Color backgroundColor = isSelected
               ? mCurrentTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground]
               : mCurrentTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground];
            Color textColor = isSelected
               ? mCurrentTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont]
               : mCurrentTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont];
            Font font;
            if (isSelected)
               font = CreateNewBoldFont();
            else
               font = CreateNewFont();
            using (SolidBrush brush = new SolidBrush(backgroundColor)) {
               pDrawItemEventArgs.Graphics.FillRectangle(brush, tabRect);
            }
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pDrawItemEventArgs.Graphics, page.Text, font,
               tabRect, textColor, flags);
         }

         public void ApplyThemeToThemePanel() {
            if (mCurrentTheme == null)
               return;
            BackColor = mCurrentTheme.mColors[(int)ColorUsage.PanelBackground];
            foreach (ToolStripItem item in mStatusStrip.Items) {
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  control.Font = mCurrentTheme.mFonts[(int)FontUsage.Status];
                  control.ForeColor = mCurrentTheme.mColors[(int)ColorUsage.StatusFont];
                  control.BackColor = mCurrentTheme.mColors[(int)ColorUsage.StatusBackground];
               }
            }
            mPrimaryTabControl.Font = mCurrentTheme.mFonts[(int)FontUsage.Interface];
            mPrimaryTabControl.ForeColor = mCurrentTheme.mColors[(int)ColorUsage.InterfaceFont];
            mPrimaryTabControl.BackColor = mCurrentTheme.mColors[(int)ColorUsage.InterfaceBackground];
            mHighlightTabControl.Font = mPrimaryTabControl.Font;
            mHighlightTabControl.ForeColor = mPrimaryTabControl.ForeColor;
            mHighlightTabControl.BackColor = mPrimaryTabControl.BackColor;
            foreach (TabPage page in mPrimaryTabControl.TabPages) {
               page.BackColor = mCurrentTheme.mColors[(int)ColorUsage.InterfaceBackground];
               page.ForeColor = mCurrentTheme.mColors[(int)ColorUsage.InterfaceFont];
            }
            foreach (TabPage page in mHighlightTabControl.TabPages) {
               page.BackColor = mPrimaryTabControl.BackColor;
               page.ForeColor = mPrimaryTabControl.ForeColor;
            }
            foreach (TabPage page in mPrimaryTabControl.TabPages)
               ApplyThemeToControlTree(page);
            foreach (TabPage page in mHighlightTabControl.TabPages)
               ApplyThemeToControlTree(page);
            //DEBUG efm5 2026 04 6 for each Label in the theme panel, all its TabPages, and all clusters – Refresh ()
         }

         private void ApplyThemeToControlTree(Control pParent) {
            if (mCurrentTheme == null)
               return;
            foreach (Control control in pParent.Controls) {
               control.BackColor = mCurrentTheme.mColors[(int)ColorUsage.InterfaceBackground];
               control.ForeColor = mCurrentTheme.mColors[(int)ColorUsage.InterfaceFont];
               control.Font = mCurrentTheme.mFonts[(int)FontUsage.Interface];
               ApplyThemeToControlTree(control);
            }
         }

         public void SaveSettings() {
            if (mForm == null)
               return;
            Settings.Default.ThemeLocation = mForm.Location;
            Settings.Default.ThemeSize = mForm.Size;
         }

         public void UseSettings() {
            Location = Settings.Default.ThemeLocation;
            Size = Settings.Default.ThemeSize;
         }

         public void UpdateTheme() {
         }

         private void ApplyButton_Click(object? pSender, EventArgs pEventArgs) {
            SaveSettings();
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArgs) {
            CloseThemePanel();
         }

         private void OkButton_Click(object? pSender, EventArgs pEventArgs) {
            UpdateTheme();
            //DEBUG efm5 2026 04 6 if current theme has been changed, repaint main form to reflect changes immediately
            CloseThemePanel();
         }

         private void CloseThemePanel() {
            MainForm.RestoreFromThemePanel();
         }
      }
   }
}
